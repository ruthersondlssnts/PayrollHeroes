using Ganss.Excel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TPS.Infrastructure;
using TPS.Infrastructure.DTO;
using TPS.Infrastructure.Enums;
using TPS.Infrastructure.Models;
using TPS.Services.Interfaces;

namespace TPS.RabbitMq.Services
{
    public class DtrProcessorService : IDtrProcessorService
    {
        private readonly IDBService<EmployeeTimesheet> _data;
        private readonly IDBService<Employee> _dataEmployee;
        private readonly IDBService<RefPayrollCutoff> _dataPayrollCutoff;
        private readonly IDBService<BiometricsData> _dataBiometrics;
        private readonly IDBService<RequestDtr> _dataRequestDtr;
        private readonly IDBService<RequestOvertime> _dataRequestOt;
        private readonly IDBService<RequestLeave> _dataRequestLeave;
        private readonly IDBService<RequestChangeShift> _dataRequestChangeShift;
        private readonly IDBService<RefCalendarActivities> _dataCalendarActivities;
        public DtrProcessorService(
            IDBService<EmployeeTimesheet> data,
            IDBService<Employee> dataEmployee,
            IDBService<RefPayrollCutoff> dataPayrollCutoff,
            IDBService<BiometricsData> dataBiometrics,
            IDBService<RequestOvertime> dataRequestOt,
            IDBService<RequestLeave> dataRequestLeave,
            IDBService<RequestDtr> dataRequestDtr,
            IDBService<RefCalendarActivities> dataCalendarActivities,
            IDBService<RequestChangeShift> dataRequestChangeShift
            )
        {
            _data = data;
            _dataEmployee = dataEmployee;
            _dataPayrollCutoff = dataPayrollCutoff;
            _dataBiometrics = dataBiometrics;
            _dataRequestDtr = dataRequestDtr;
            _dataRequestOt = dataRequestOt;
            _dataRequestLeave = dataRequestLeave;
            _dataRequestChangeShift = dataRequestChangeShift;
            _dataCalendarActivities = dataCalendarActivities;
        }

        public void GetApprovedChangeShift(DTOPayload request)
        {
            var requestchangeShift = _dataRequestChangeShift.FilterBy(x => x.EmployeeId == request.Employee.Id.ToString() && x.ShiftDate >= request.RefPayrollCutoff.CutoffStartDate && x.ShiftDate <= request.RefPayrollCutoff.CutoffEndDate && x.ApprovalStatusId == (int)ApprovalStatus.APPROVED).ToList();
            List<EmployeeTimesheet> timesheets = new List<EmployeeTimesheet>();
            foreach (var row in requestchangeShift)
            {
                var timesheet = _data.FindOne(x => x.EmployeeId == request.Employee.Id.ToString() && x.ShiftDate == row.ShiftDate);
                if (timesheet == null) continue;

                //Fillup DTR information
                timesheet.ShiftIn = row.ShiftIn;
                timesheet.ShiftOut = row.ShiftOut;
                timesheet.RequiredHour = row.RequiredHour;
                timesheet.GracePeriod = row.GracePeriod;
                timesheet.IsNightShift = row.IsNightShift;
                _data.ReplaceOneAsync(timesheet);
            }
        }

        public void GetApprovedDtr(DTOPayload request)
        {
            var requestDtr = _dataRequestDtr.FilterBy(x => x.EmployeeId == request.Employee.Id.ToString() && x.ShiftDate >= request.RefPayrollCutoff.CutoffStartDate && x.ShiftDate <= request.RefPayrollCutoff.CutoffEndDate && x.ApprovalStatusId == (int)ApprovalStatus.APPROVED).ToList();

            foreach (var row in requestDtr)
            {
                var timesheet = _data.FindOne(x => x.EmployeeId == request.Employee.Id.ToString() && x.ShiftDate == row.ShiftDate);
                if (timesheet == null) continue;

                //Fillup DTR information
                timesheet.DtrIn = row.ActualIn;
                timesheet.DtrOut = row.ActualOut;

                TimeSpan span = timesheet.DtrOut.Value - timesheet.DtrIn.Value;
                timesheet.DtrRenderedHour = Convert.ToDecimal(span.TotalMilliseconds / 60 / 60 / 1000);

                //Fillup In/Out from DTR
                timesheet.ActualIn = row.ActualIn;
                timesheet.ActualOut = row.ActualOut;
                timesheet.RenderedHour = Convert.ToDecimal(span.TotalMilliseconds / 60 / 60 / 1000);
                //In case of change shift. To be handled by Change Shift
                //timesheet.ShiftIn = row.ShiftIn;
                //timesheet.ShiftOut = row.ShiftOut;
                _data.ReplaceOneAsync(timesheet);
            }
        }

        public void GetApprovedOvertime(DTOPayload request)
        {
            var requestOt = _dataRequestOt.FilterBy(x => x.EmployeeId == request.Employee.Id.ToString() && x.OvertimeDate >= request.RefPayrollCutoff.CutoffStartDate && x.OvertimeDate <= request.RefPayrollCutoff.CutoffEndDate && x.ApprovalStatusId == (int)ApprovalStatus.APPROVED).ToList();

            foreach (var row in requestOt)
            {
                var timesheet = _data.FindOne(x => x.EmployeeId == request.Employee.Id.ToString() && x.ShiftDate == row.OvertimeDate);
                if (timesheet == null) continue;

                //Fillup OT information
                timesheet.OvertimeTimeIn = row.OvertimeTimeIn;
                timesheet.OvertimeTimeOut = row.OvertimeTimeOut;
                timesheet.Overtime = 0;
                timesheet.Overtime8 = 0;
                _data.ReplaceOneAsync(timesheet);
            }

        }

        public void GetHolidayFromCalendar(DTOPayload request)
        {

            var holiday = _dataCalendarActivities.FilterBy(x => x.DateDeleted == null).ToList()
               .Where(x => x.WorkDate.Year == request.RefPayrollCutoff.CutoffStartDate.Year).ToList();

            if (holiday == null) return;

            foreach (var row in holiday)
            {
                var timesheets = _data.FilterBy(x => x.DateDeleted == null).ToList()
                    .Where(x => DateTime.Compare(x.ShiftDate.Value.Date, row.WorkDate.Date) == 0).ToList();
                if (timesheets.Count == 0) return;

                foreach (var timesheet in timesheets)
                {
                    timesheet.HolidayTypeId = row.HolidayTypeId;
                    timesheet.HolidayTypeName = row.HolidayTypeName;
                    _data.ReplaceOneAsync(timesheet);
                }
            }
        }

        public void CheckAbsent(DTOPayload request)
        {
            //Conditions to be abset
            //No Shift In/Out
            //NoActual In/Out
            //Not DTR in/out
            //No Approved Leave
            //Regular working day

            var absent = _data.FilterBy(x => x.EmployeeId == request.Employee.Id.ToString() &&
           x.PayrollCutoffId == request.RefPayrollCutoff.Id.ToString()).ToList();

            if (absent == null) return;

            foreach (var row in absent)
            {
                if (row.RenderedHour == 0 && row.DtrRenderedHour == 0 && row.ApproveLeave == 0 && row.DayTypeName == DayType.RWD.ToString() && row.HolidayTypeId == 0)
                {
                    row.Absent = 1;
                }
                else
                {
                    row.Absent = 0;
                }
                    

                _data.ReplaceOneAsync(row);
            }
        }

        public void CheckLeave(DTOPayload request)
        {
            var requestLeave = _dataRequestLeave.FilterBy(x => x.EmployeeId == request.Employee.Id.ToString() && x.ApprovalStatusId == (int)ApprovalStatus.APPROVED && !x.IsTimesheetProcessed).ToList();

            foreach (var row in requestLeave)
            {
                decimal remaining = row.LeaveDay;
                for (DateTime dtstart = row.DateStart; dtstart <= row.DateEnd; dtstart = dtstart.AddDays(1))
                {
                    var timesheet = _data.FindOne(x => x.EmployeeId == request.Employee.Id.ToString() && x.ShiftDate == dtstart);
                    if (timesheet == null) continue;
                    timesheet.LeaveTypeId = row.LeaveTypeId;
                    timesheet.LeaveTypeName = row.LeaveTypeName;

                    if (remaining >= 1.5m)
                    {
                        timesheet.ApproveLeave = 1;
                        remaining = remaining - 1;
                    }
                    else
                    {
                        timesheet.ApproveLeave = remaining;
                    }

                    _data.ReplaceOneAsync(timesheet);
                }

                //update requestLeave data
                row.IsTimesheetProcessed = true;
                _dataRequestLeave.ReplaceOneAsync(row);
            }
        }

        public void ComputeApprovedDtrHourByShift(DTOPayload request)
        {
            var computedIn = new DateTime();
            var computedOut = new DateTime();

            //Check if already exist
            var timesheets = _data.FilterBy(x => x.EmployeeId == request.Employee.Id.ToString()
            && x.PayrollCutoffId == request.RefPayrollCutoff.Id.ToString()
            && x.DtrIn != null && x.DtrOut != null).ToList();
            if (timesheets.Count == 0) return;

            foreach (var timesheet in timesheets)
            {
                if (timesheet.DtrIn == null || timesheet.DtrOut == null)
                {
                    timesheet.DtrRenderedHourByShift = 0;
                    _data.ReplaceOneAsync(timesheet);
                    continue;
                }
                if (string.IsNullOrEmpty(timesheet.ShiftIn) || string.IsNullOrEmpty(timesheet.ShiftOut))
                {
                    //MUST FILE DTR correction or changeshift
                    timesheet.DtrRenderedHourByShift = 0;
                    _data.ReplaceOneAsync(timesheet);
                    continue;
                }

                var shiftIn = DateTime.Parse(timesheet.ShiftDate.Value.ToShortDateString() + " " + timesheet.ShiftIn);
                var shiftOut = timesheet.IsNightShift == true ? DateTime.Parse(timesheet.ShiftDate.Value.AddDays(1).ToShortDateString() + " " + timesheet.ShiftOut) : DateTime.Parse(timesheet.ShiftDate.Value.ToShortDateString() + " " + timesheet.ShiftOut);

                //Actual IN
                if (shiftIn.AddMinutes((double)timesheet.GracePeriod) >= timesheet.DtrIn)
                {
                    computedIn = shiftIn;
                }
                else
                {
                    computedIn = timesheet.ActualIn.Value;
                }

                //Actual Out
                if (timesheet.DtrOut >= shiftOut)
                {
                    computedOut = shiftOut;
                }
                else
                {
                    computedOut = timesheet.ActualOut.Value;
                }

                TimeSpan span = computedOut - computedIn;
                timesheet.DtrRenderedHourByShift = Convert.ToDecimal(span.TotalMilliseconds / 60 / 60 / 1000);
                _data.ReplaceOneAsync(timesheet);
            }
        }

        public void ComputeApprovedOvertimeHr(DTOPayload request)
        {

            var timesheetOt = _data.FilterBy(x =>
            x.EmployeeId == request.Employee.Id.ToString() &&
            x.PayrollCutoffId == request.RefPayrollCutoff.Id.ToString() &&
            x.OvertimeTimeIn != null).ToList();
            if (timesheetOt == null) return;

            foreach (var timesheet in timesheetOt)
            {
                var actualIn = new DateTime();
                var actualOut = new DateTime();

                var computedOtIn = new DateTime();
                var computedOtOut = new DateTime();

                actualIn = timesheet.ActualIn.Value;
                actualOut = timesheet.ActualOut.Value;

                if (timesheet.DtrIn != null && timesheet.DtrOut != null)
                {
                    actualIn = timesheet.DtrIn.Value;
                    actualOut = timesheet.DtrOut.Value;
                }

                //Holiday or restday just count filed OT In and Out
                if (timesheet.HolidayTypeId > 0 || timesheet.DayTypeId == (int)DayType.RD)
                {
                    computedOtIn = timesheet.OvertimeTimeIn.Value;
                    computedOtOut = timesheet.OvertimeTimeOut.Value;
                }
                else
                {
                    //Check if actual out is > filed OTIn
                    // ShiftOut 5PM, ActaulOut 11PM. OtOut 10PM
                    if (actualOut > timesheet.OvertimeTimeOut.Value)
                    {
                        computedOtOut = timesheet.OvertimeTimeOut.Value;
                    }

                    // ShiftOut 5PM, ActaulOut 9PM. OtOut 10PM
                    if (DateTime.Parse(timesheet.ShiftDate.Value.ToShortDateString() + " " + timesheet.ShiftOut)
                        <= timesheet.OvertimeTimeIn.Value)
                    {
                        computedOtIn = timesheet.OvertimeTimeIn.Value;
                    }
                    else
                    {
                        computedOtIn = DateTime.Parse(timesheet.ShiftDate.Value.ToShortDateString() + " " + timesheet.ShiftOut);
                    }

                }
                TimeSpan span = computedOtOut - computedOtIn;
                timesheet.Overtime = Convert.ToDecimal(span.TotalMilliseconds / 60 / 60 / 1000);
                _data.ReplaceOneAsync(timesheet);
            }
        }

        public void ComputeLate(DTOPayload request)
        {
            var timesheets = _data.FilterBy(x => x.EmployeeId == request.Employee.Id.ToString() && x.PayrollCutoffId == request.RefPayrollCutoff.Id.ToString()).ToList();

            if (timesheets.Count > 0) return;

            foreach (var timesheet in timesheets)
            {
                //Check if already exist
                //var timesheet = _data.FindOne(x => x.EmployeeId == request.Employee.Id.ToString() && x.ShiftDate == dtstart);
                //if (timesheet == null) continue;

                //Since it is restday or holiday
                if (timesheet.DayTypeName == DayType.RD.ToString() || timesheet.HolidayTypeId > 0)
                {
                    timesheet.Undertime = 0;
                    _data.ReplaceOneAsync(timesheet);
                    continue;
                }

                if ((timesheet.ActualOut == null || timesheet.ActualIn == null) ||
                    (string.IsNullOrEmpty(timesheet.ShiftIn) || string.IsNullOrEmpty(timesheet.ShiftOut)))
                {
                    timesheet.Late = 0;
                    _data.ReplaceOneAsync(timesheet);
                    continue;
                }

                var shiftIn = DateTime.Parse(timesheet.ShiftDate.Value.ToShortDateString() + " " + timesheet.ShiftIn);
                var shiftOut = timesheet.IsNightShift == true ? DateTime.Parse(timesheet.ShiftDate.Value.AddDays(1).ToShortDateString() + " " + timesheet.ShiftOut) : DateTime.Parse(timesheet.ShiftDate.Value.ToShortDateString() + " " + timesheet.ShiftOut);

                var computedIn = new DateTime();

                computedIn = timesheet.ActualIn.Value;

                if (timesheet.DtrIn != null && timesheet.DtrOut != null)
                {
                    computedIn = timesheet.DtrIn.Value;
                }

                //Actual IN
                if (shiftIn.AddMinutes((double)timesheet.GracePeriod) >= computedIn)
                {
                    computedIn = shiftIn;
                }

                TimeSpan span = computedIn - shiftIn;
                timesheet.Late = Convert.ToDecimal(span.TotalMilliseconds / 60 / 60 / 1000);
                _data.ReplaceOneAsync(timesheet);
            }


        }

        public void ComputeNightShiftHours(DTOPayload request)
        {
            if (request.Employee.RefShift == null) return;

            //Get all shift that is ND
            var timesheetND = _data.FilterBy(x =>
            x.EmployeeId == request.Employee.Id.ToString() &&
            x.PayrollCutoffId == request.RefPayrollCutoff.Id.ToString() &&
            x.IsNightShift).ToList();
            if (timesheetND == null) return;

            foreach (var timesheet in timesheetND)
            {

                if (timesheet.ActualIn == null || timesheet.ActualOut == null)
                    continue;
                if (timesheet.ShiftDate == null)
                    continue;


                var actualIn = new DateTime();
                var actualOut = new DateTime();

                var NdHourIn = new DateTime();
                var NdHourOut = new DateTime();

                NdHourIn = DateTime.Parse(timesheet.ShiftDate.Value.ToShortDateString() + " " + request.Employee.RefShift.NDStart);
                NdHourOut = DateTime.Parse(timesheet.ShiftDate.Value.AddDays(1).ToShortDateString() + " " + request.Employee.RefShift.NDEnd);

                actualIn = timesheet.ActualIn.Value;
                actualOut = timesheet.ActualOut.Value;

                if (timesheet.DtrIn != null && timesheet.DtrOut != null)
                {
                    actualIn = timesheet.DtrIn.Value;
                    actualOut = timesheet.DtrOut.Value;
                }

                //ND Hour In 10PM <= Actual In 11PM
                if (NdHourIn <= actualIn)
                {
                    NdHourIn = actualIn;
                }

                //ND HOUR OUT 6AM >= Actual OUT 5AM
                if (NdHourOut >= actualOut)
                {
                    NdHourOut = actualOut;
                }

                TimeSpan span = NdHourOut - NdHourIn;
                timesheet.NightDiff = Convert.ToDecimal(span.TotalMilliseconds / 60 / 60 / 1000);
                _data.ReplaceOneAsync(timesheet);
            }
        }

        public void ComputeUnderTime(DTOPayload request)
        {
            var timesheets = _data.FilterBy(x => x.PayrollCutoffId == request.RefPayrollCutoff.Id.ToString() && x.EmployeeId == request.Employee.Id.ToString()).ToList();
            foreach (var timesheet in timesheets)
            {
                //Check if already exist
                //var timesheet = _data.FindOne(x => x.EmployeeId == request.Employee.Id.ToString() && x.ShiftDate == dtstart);
                //if (timesheet == null) continue;

                if (timesheet.ActualOut == null || timesheet.ActualIn == null)
                {
                    timesheet.Undertime = 0;
                    _data.ReplaceOneAsync(timesheet);
                    continue;
                }

                //Since it is restday no 
                if (timesheet.DayTypeName == DayType.RD.ToString() || timesheet.HolidayTypeId > 0)
                {
                    timesheet.Undertime = 0;
                    _data.ReplaceOneAsync(timesheet);
                    continue;
                }


                if (string.IsNullOrEmpty(timesheet.ShiftIn) || string.IsNullOrEmpty(timesheet.ShiftOut))
                {
                    timesheet.Undertime = 0;
                    _data.ReplaceOneAsync(timesheet);
                    continue;
                }

                var shiftIn = DateTime.Parse(timesheet.ShiftDate.Value.ToShortDateString() + " " + timesheet.ShiftIn);
                var shiftOut = timesheet.IsNightShift == true ? DateTime.Parse(timesheet.ShiftDate.Value.AddDays(1).ToShortDateString() + " " + timesheet.ShiftOut) : DateTime.Parse(timesheet.ShiftDate.Value.ToShortDateString() + " " + timesheet.ShiftOut);

                var computedOut = new DateTime();

                computedOut = timesheet.ActualOut.Value;

                if (timesheet.DtrIn != null && timesheet.DtrOut != null)
                {
                    computedOut = timesheet.DtrIn.Value;
                }

                //Actual IN
                if (shiftOut < computedOut)
                {
                    timesheet.Undertime = 0;
                    _data.ReplaceOneAsync(timesheet);
                }
                else
                {
                    TimeSpan span = shiftOut - computedOut;
                    timesheet.Undertime = Convert.ToDecimal(span.TotalMilliseconds / 60 / 60 / 1000);
                    _data.ReplaceOneAsync(timesheet);
                }
            }
        }

        public void ComputeWorkingHourByShift(DTOPayload request)
        {
            var timesheets = _data.FilterBy(x => x.PayrollCutoffId == request.RefPayrollCutoff.Id.ToString() && x.EmployeeId == request.Employee.Id.ToString()).ToList();
            foreach (var timesheet in timesheets)
            {
                //Check if already exist
                // var timesheet = _data.FindOne(x => x.EmployeeId == request.Employee.Id.ToString() && x.ShiftDate == dtstart);
                //if (timesheet == null) continue;

                if (timesheet.ActualOut == null || timesheet.ActualIn == null)
                {
                    timesheet.RenderedHourByShift = 0;
                    _data.ReplaceOneAsync(timesheet);
                    continue;
                }
                if (string.IsNullOrEmpty(timesheet.ShiftIn) || string.IsNullOrEmpty(timesheet.ShiftOut))
                {
                    timesheet.RenderedHourByShift = 0;
                    _data.ReplaceOneAsync(timesheet);
                    return;
                }

                var shiftIn = DateTime.Parse(timesheet.ShiftDate.Value.ToShortDateString() + " " + timesheet.ShiftIn);
                var shiftOut = timesheet.IsNightShift == true ? DateTime.Parse(timesheet.ShiftDate.Value.AddDays(1).ToShortDateString() + " " + timesheet.ShiftOut) : DateTime.Parse(timesheet.ShiftDate.Value.ToShortDateString() + " " + timesheet.ShiftOut);

                var computedIn = new DateTime();
                var computedOut = new DateTime();

                //Actual IN
                if (shiftIn.AddMinutes((double)timesheet.GracePeriod) >= timesheet.ActualIn)
                {
                    computedIn = shiftIn;
                }
                else
                {
                    computedIn = timesheet.ActualIn.Value;
                }

                //Actual Out
                if (timesheet.ActualOut >= shiftOut)
                {
                    computedOut = shiftOut;
                }
                else
                {
                    computedOut = timesheet.ActualOut.Value;
                }

                TimeSpan span = computedOut - computedIn;
                timesheet.RenderedHourByShift = Math.Round((Convert.ToDecimal(span.TotalMilliseconds / 60 / 60 / 1000)), 2);
                _data.ReplaceOneAsync(timesheet);
            }
        }

        public void GenerateTimesheet(DTOPayload request)
        {
            List<EmployeeTimesheet> timesheets = new List<EmployeeTimesheet>();

            for (DateTime dtstart = request.RefPayrollCutoff.CutoffStartDate; dtstart <= request.RefPayrollCutoff.CutoffEndDate; dtstart = dtstart.AddDays(1))
            {
                //Check if already exist
                var timesheet = _data.FindOne(x => x.EmployeeId == request.Employee.Id.ToString() && x.ShiftDate == dtstart);
                if (timesheet != null) continue;


                string dayOfWeek = dtstart.DayOfWeek.ToString().Substring(0, 3).ToUpper();
                if (request.Employee.RefShift.RefShiftDetails == null) continue;
                var shiftdetail = request.Employee.RefShift.RefShiftDetails.FirstOrDefault(x => x.Day == dayOfWeek);

                if (shiftdetail == null) continue;

                //Populate Timesheet
                timesheet = new EmployeeTimesheet();
                timesheet.EmployeeId = request.Employee.Id.ToString();
                timesheet.PayrollCutoffId = request.RefPayrollCutoff.Id.ToString();
                timesheet.LastName = request.Employee.LastName;
                timesheet.FirstName = request.Employee.FirstName;
                timesheet.ShiftDate = dtstart;
                timesheet.ShiftIn = shiftdetail.IsRestDay == true ? "" : request.Employee.RefShift.ShiftIn;
                timesheet.ShiftOut = shiftdetail.IsRestDay == true ? "" : request.Employee.RefShift.ShiftOut;
                timesheet.RequiredHour = shiftdetail.RequiredHour;
                timesheet.DayTypeId = shiftdetail.IsRestDay == true ? (int)DayType.RD : (int)DayType.RWD;
                timesheet.DayTypeName = shiftdetail.IsRestDay == true ? DayType.RD.ToString() : DayType.RWD.ToString();
                timesheet.IsNightShift = request.Employee.RefShift.IsNightDiff;
                timesheet.GracePeriod = request.Employee.RefShift.GracePeriod;
                timesheet.IsProcessedDTR = false;
                timesheet.IsProcessedPayroll = false;

                timesheet.ActualIn = null;
                timesheet.ActualOut = null;
                timesheet.DtrIn = null;
                timesheet.DtrOut = null;
                timesheet.OvertimeTimeIn = null;
                timesheet.OvertimeTimeOut = null;
                timesheet.LeaveTypeName = "";
                timesheet.HolidayTypeName = "";
                timesheets.Add(timesheet);
            }

            if (timesheets.Count > 0)
                _data.InsertManyAsync(timesheets);
        }

        public void GetActualInOut(DTOPayload request)
        {
            var timesheets = _data.FilterBy(x => x.PayrollCutoffId == request.RefPayrollCutoff.Id.ToString() && x.EmployeeId == request.Employee.Id.ToString()).ToList();
            foreach (var timesheet in timesheets)
            {
                //Check if already exist
                //var timesheet = _data.FindOne(x => x.EmployeeId == request.Employee.Id.ToString() && x.ShiftDate == dtstart);
                //if (timesheet == null) continue;

                //Since filter by DATE only is not possible in LINQ. Need to execute ToList then query again

                var biometrics = new BiometricsData();
                if (!timesheet.IsNightShift)
                {
                    biometrics = _dataBiometrics.FilterBy(x => x.ElectronicId == request.Employee.ElectronicId).ToList()
                        .Where(x => DateTime.Compare(x.LogDate.Date, timesheet.ShiftDate.Value) == 0).FirstOrDefault();

                    if (biometrics == null)
                    {
                        timesheet.ActualIn = null;
                        timesheet.ActualOut = null;
                        timesheet.RenderedHour = 0;
                        _data.ReplaceOneAsync(timesheet);
                        continue;
                    }

                    if (string.IsNullOrEmpty(biometrics.ClockIn) || string.IsNullOrEmpty(biometrics.ClockOut))
                    {
                        if (!string.IsNullOrEmpty(biometrics.ClockIn))
                            timesheet.ActualIn = DateTime.Parse(biometrics.LogDate.ToShortDateString() + " " + biometrics.ClockIn);

                        if (!string.IsNullOrEmpty(biometrics.ClockOut))
                            timesheet.ActualOut = DateTime.Parse(biometrics.LogDate.ToShortDateString() + " " + biometrics.ClockOut);
                        timesheet.RenderedHour = 0;
                        _data.ReplaceOneAsync(timesheet);
                        continue;
                    }

                    timesheet.ActualIn = DateTime.Parse(biometrics.LogDate.ToShortDateString() + " " + biometrics.ClockIn);
                    timesheet.ActualOut = DateTime.Parse(biometrics.LogDate.ToShortDateString() + " " + biometrics.ClockOut);
                }
                else
                {
                    //To Do. need to check also for nightShift
                    biometrics = _dataBiometrics.FilterBy(x => x.ElectronicId == request.Employee.ElectronicId).ToList()
                        .Where(x => DateTime.Compare(x.LogDate.Date, timesheet.ShiftDate.Value) == 0).FirstOrDefault();

                    if (biometrics == null)
                    {
                        timesheet.ActualIn = null;
                        timesheet.ActualOut = null;
                        timesheet.RenderedHour = 0;
                        _data.ReplaceOneAsync(timesheet);
                        continue;
                    }

                    if (string.IsNullOrEmpty(biometrics.ClockIn) || string.IsNullOrEmpty(biometrics.ClockOut))
                    {
                        if (!string.IsNullOrEmpty(biometrics.ClockIn))
                            timesheet.ActualIn = DateTime.Parse(biometrics.LogDate.ToShortDateString() + " " + biometrics.ClockIn);

                        if (!string.IsNullOrEmpty(biometrics.ClockOut))
                            timesheet.ActualOut = DateTime.Parse(biometrics.LogDate.AddDays(1).ToShortDateString() + " " + biometrics.ClockOut);
                        timesheet.RenderedHour = 0;
                        _data.ReplaceOneAsync(timesheet);
                        continue;
                    }

                    timesheet.ActualIn = DateTime.Parse(biometrics.LogDate.ToShortDateString() + " " + biometrics.ClockIn);

                    if (DateTime.Parse(biometrics.LogDate.ToShortDateString() + " " + biometrics.ClockIn) > DateTime.Parse(biometrics.LogDate.ToShortDateString() + " " + biometrics.ClockOut))
                    {
                        timesheet.ActualOut = DateTime.Parse(biometrics.LogDate.AddDays(1).ToShortDateString() + " " + biometrics.ClockOut);
                    }
                }

                if (timesheet.ActualOut == null || timesheet.ActualIn == null) continue;

                TimeSpan span = timesheet.ActualOut.Value - timesheet.ActualIn.Value;
                timesheet.RenderedHour = Math.Round((Convert.ToDecimal(span.TotalMilliseconds / 60 / 60 / 1000)), 2);
                _data.ReplaceOneAsync(timesheet);
            }
        }

        public void ProcessedDTRFlag(DTOPayload request)
        {
            var timesheets = _data.FilterBy(x => x.PayrollCutoffId == request.RefPayrollCutoff.Id.ToString() && x.EmployeeId == request.Employee.Id.ToString()).ToList();
            foreach (var timesheet in timesheets)
            {
                //Check if already exist
                //var timesheet = _data.FindOne(x => x.EmployeeId == request.Employee.Id.ToString() && x.ShiftDate == dtstart);
                //if (timesheet == null) continue;

                timesheet.IsProcessedDTR = true;

                _data.ReplaceOneAsync(timesheet);
            }
        }

        public void UploadFile(DTOBiometricUpload dTOBiometricUpload)
        {
            string filename = ConfigurationManager.AppSettings["FileUploadInProgress"] + dTOBiometricUpload.Filename;
            string doneFolder = ConfigurationManager.AppSettings["FileUploadDone"] + dTOBiometricUpload.Filename;
            bool fileExist = FileExists(false, 1, filename);

            if (fileExist)
            {
                var biometricdatas = new ExcelMapper(filename).Fetch<BiometricsData>().ToList(); //get excel path

                try
                {
                    foreach (var item in biometricdatas)
                    {
                        var a = _dataBiometrics.FindOne(x => x.ElectronicId == item.ElectronicId && x.LogDate == item.LogDate);
                        if (a == null)
                        {
                            _dataBiometrics.InsertOne(item);
                        }
                    }

                    System.IO.File.Move(filename, doneFolder);
                    Console.WriteLine("Finished uploading biometrics collection");

                }
                catch (Exception e)
                {
                    Console.WriteLine("Error writing to MongoDB: " + e.Message);
                }
            }
        }

        static bool FileExists(bool _fileexists, int Count, string filename)
        {
            if (_fileexists)
                return true;

            Count++;
            Thread.Sleep(2000);

            if (!File.Exists(filename))
            {
                if (Count <= 3)
                {
                    FileExists(_fileexists, Count, filename);
                }
                return false;
            }
            return true;
        }
    }

}
