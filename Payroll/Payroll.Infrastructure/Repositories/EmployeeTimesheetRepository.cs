using AutoMapper;
using Payroll.Core.Interface;
using Payroll.Core.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using Payroll.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Payroll.Infrastructure.Repositories
{
    public class EmployeeTimesheetRepository : IRepository<EmployeeTimesheetEntity>, IEmployeeTimesheet
    {
        private PayrollDBContext db = new PayrollDBContext();
        private IMapper _mapper;
        private RequestOvertimeRepository RequestOvertimeRepo;
        private RequestLeaveRepository RequestLeaveRepo;
        private RequestDTRRepository RequestDTRRepo;
        private RefConfigurationRepository RefConfigurationRepo;

        public string sql = @"with main as (
                Select group_id,name
                ,[Level].GetAncestor(1).ToString()  as [ancestor1]
                ,[Level].GetAncestor(2).ToString()  as [ancestor2]
                ,is_enable
                ,date_deleted
                from ref_group), 
				
				main2 as (
                SELECT group_id,name,
                ISNULL((SELECT group_id FROM ref_group WHERE level.ToString()=main.[ancestor1]),0) AS  [ancestor1],
                ISNULL((SELECT group_id FROM ref_group WHERE level.ToString()=main.[ancestor2]),0) AS  [ancestor2]
                FROM main), 
				
				main3 as (
				SELECT group_id,name,[ancestor1],[ancestor2] FROM main2 WHERE group_id = {0}
				UNION ALL
				SELECT e.group_id,e.name,e.[ancestor1],e.[ancestor2]
				FROM main2 e
				INNER JOIN main3 ecte ON ecte.group_id = e.[ancestor1]
				)

SELECT employee_timesheet.[employee_timesheet_id]
      ,employee_timesheet.[employee_id]
      ,employee_timesheet.[shift_date]
      ,employee_timesheet.[ref_shift_id]
      ,employee_timesheet.[ref_day_type_id]
      ,employee_timesheet.[holiday_day_type_id]
      ,employee_timesheet.[required_hour]
      ,employee_timesheet.[rendered_hour]
      ,employee_timesheet.[time_in1]
      ,employee_timesheet.[time_out1]
      ,employee_timesheet.[time_in2]
      ,employee_timesheet.[time_out2]
      ,employee_timesheet.[ot_in]
      ,employee_timesheet.[ot_out]
      ,employee_timesheet.[ot]
      ,employee_timesheet.[ot8]
      ,employee_timesheet.[night_dif]
      ,employee_timesheet.[night_dif8]
      ,employee_timesheet.[absent]
      ,employee_timesheet.[late]
      ,employee_timesheet.[undertime]
      ,employee_timesheet.[approve_leave]
      ,employee_timesheet.[ref_leave_type_id]
      ,employee_timesheet.[payroll_process],ref_day_type.date_type_code,ref_shift.shift_name,employee.first_name,employee.last_name FROM [dbo].[employee_timesheet] 
INNER JOIN ref_day_type ON employee_timesheet.ref_day_type_id=ref_day_type.ref_day_type_id
INNER JOIN ref_shift ON employee_timesheet.ref_shift_id=ref_shift.ref_shift_id
INNER JOIN employee ON employee.employee_id=employee_timesheet.employee_id

WHERE employee_timesheet.employee_id IN 
(
  SELECT employee_id FROM employee_group WHERE group_id IN(SELECT group_id FROM main3)
)";
        public EmployeeTimesheetRepository()
        {
            RequestOvertimeRepo = new RequestOvertimeRepository();
            RequestLeaveRepo = new RequestLeaveRepository();
            RequestDTRRepo = new RequestDTRRepository();
            RefConfigurationRepo = new RefConfigurationRepository();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EmployeeTimesheetEntity, employee_timesheet>();
                cfg.CreateMap<employee_timesheet, EmployeeTimesheetEntity>();

                cfg.CreateMap<EmployeeEntity, employee>();
                cfg.CreateMap<employee, EmployeeEntity>();

                cfg.CreateMap<RefDayTypeEntity, ref_day_type>();
                cfg.CreateMap<ref_day_type, RefDayTypeEntity>();


                cfg.CreateMap<RefShiftEntity, ref_shift>();
                cfg.CreateMap<ref_shift, RefShiftEntity>();
            });

            _mapper = config.CreateMapper();
        }
        public EmployeeTimesheetEntity GetByID(int id)
        {
            var data = db.employee_timesheet.Where(a => a.employee_timesheet_id == id).FirstOrDefault();
            var record = _mapper.Map<employee_timesheet, EmployeeTimesheetEntity>(data);

            return record;
        }
        public List<EmployeeTimesheetEntity> GenerateNewTimesheet(int employeeId, DateTime dtCutoffStart, DateTime dtCutoffEnd, int refShiftId)
        {
            List<EmployeeTimesheetEntity> TimesheetList = new List<EmployeeTimesheetEntity>();
            var shiftDetails = db.ref_shift_detail.Where(a => a.ref_shift_id == refShiftId).ToList();
            try
            {

                for (DateTime dtstart = dtCutoffStart; dtstart <= dtCutoffEnd; dtstart = dtstart.AddDays(1))
                {
                    if (!IsTimesheetExist(employeeId, dtstart))
                    {
                        EmployeeTimesheetEntity newRecord = new EmployeeTimesheetEntity();
                        newRecord.employee_id = employeeId;
                        newRecord.shift_date = dtstart;
                        newRecord.ref_shift_id = refShiftId;


                        //SHIFT Details
                        string dayOfWeek = dtstart.DayOfWeek.ToString().Substring(0, 3).ToUpper();
                        var shiftdetails = shiftDetails.FirstOrDefault(a => a.day == dayOfWeek);
                        newRecord.required_hour = shiftdetails.required_hour;

                        newRecord.ref_day_type_id = shiftdetails.rest_day == false ? 1 : 2;


                        TimesheetList.Add(newRecord);
                    }
                }

                return TimesheetList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<EmployeeTimesheetEntity>();
            }
        }

        public List<EmployeeTimesheetEntity> GenerateNewTimesheet(int employeeId, int payroll_cutoff_id, int refShiftId)
        {
            List<EmployeeTimesheetEntity> TimesheetList = new List<EmployeeTimesheetEntity>();
            var shiftDetails = db.ref_shift_detail.Where(a => a.ref_shift_id == refShiftId).ToList();
            var cutoff = db.ref_payroll_cutoff.Where(a => a.ref_payroll_cutoff_id == payroll_cutoff_id).FirstOrDefault();
            try
            {

                for (DateTime dtstart = cutoff.cutoff_date_start; dtstart <= cutoff.cutoff_date_end; dtstart = dtstart.AddDays(1))
                {
                    if (!IsTimesheetExist(employeeId, dtstart))
                    {
                        EmployeeTimesheetEntity newRecord = new EmployeeTimesheetEntity();
                        newRecord.employee_id = employeeId;
                        newRecord.shift_date = dtstart;
                        newRecord.ref_shift_id = refShiftId;


                        //SHIFT Details
                        string dayOfWeek = dtstart.DayOfWeek.ToString().Substring(0, 3).ToUpper();
                        var shiftdetails = shiftDetails.FirstOrDefault(a => a.day == dayOfWeek);
                        newRecord.required_hour = shiftdetails.required_hour;

                        newRecord.ref_day_type_id = shiftdetails.rest_day == false ? 1 : 2;
                        TimesheetList.Add(newRecord);
                    }
                    else
                    {
                        //Execute timesheet update
                    }
                }

                return TimesheetList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<EmployeeTimesheetEntity>();
            }
        }

        public bool GenerateNewTimesheet(int payroll_cutoff_id)
        {
            bool blnTrue = true;
            var cutoff = db.ref_payroll_cutoff.Where(a => a.ref_payroll_cutoff_id == payroll_cutoff_id).FirstOrDefault();
            DateTime dtCutoffStart = cutoff.cutoff_date_start;
            DateTime dtCutoffEnd = cutoff.cutoff_date_end;
            var employee = db.employee.Where(a => a.date_deleted == null).ToList();
            foreach (var row in employee)
            {
                var timesheet = GenerateNewTimesheet(row.employee_id, dtCutoffStart, dtCutoffEnd, row.ref_shift_id);
                if (timesheet.Count() > 0)
                {
                    blnTrue = InsertGeneratedtimesheet(timesheet);
                    if (!blnTrue) break;
                }

            }
            return blnTrue;
        }

        public bool InsertGeneratedtimesheet(List<EmployeeTimesheetEntity> record)
        {
            try
            {
                foreach (var row in record)
                {
                    if (!IsTimesheetExist(row.employee_id, row.shift_date))
                    {
                        Add(row);
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool IsTimesheetExist(int employeeId, DateTime? dtShiftDate)
        {

            var data = db.employee_timesheet
                .FirstOrDefault(a => a.employee_id == employeeId && a.shift_date == dtShiftDate);
            if (data != null)
            {
                return true;
            }

            return false;
        }

        //Maybe used for OT approval
        public bool IsOTVerified(int employeeId, DateTime? dtShiftDate, string otIn, string otOut)
        {

            var data = db.employee_timesheet
                .FirstOrDefault(a => a.employee_id == employeeId && a.shift_date == dtShiftDate);


            if (data != null)
            {


                var shift = db.ref_shift.FirstOrDefault(a => a.ref_shift_id == data.ref_shift_id);
                if (shift != null)
                {
                    TimeSpan timeIn = new TimeSpan();
                    TimeSpan timeOut = new TimeSpan();

                    if (GetInOut(data, shift, out timeIn, out timeOut))
                    {

                    }
                }

                return true;
            }

            return false;
        }

        public bool UpdateActualIn(int employeeId, DateTime dtShiftDate, string tsIn)
        {
            if (IsTimesheetExist(employeeId, dtShiftDate))
            {
                var record = db.employee_timesheet
                    .Where(a => a.employee_id == employeeId && a.shift_date == dtShiftDate).FirstOrDefault();
                record.time_in1 = tsIn;
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateActualOut(int employeeId, DateTime dtShiftDate, string tsOut)
        {
            if (IsTimesheetExist(employeeId, dtShiftDate))
            {
                var record = db.employee_timesheet.FirstOrDefault(a => a.employee_id == employeeId && a.shift_date == dtShiftDate);
                record.time_out1 = tsOut;
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateDTRIn(int employeeId, DateTime dtShiftDate, string tsIn)
        {
            if (IsTimesheetExist(employeeId, dtShiftDate))
            {
                var record = db.employee_timesheet
                    .Where(a => a.employee_id == employeeId && a.shift_date == dtShiftDate).FirstOrDefault();
                record.time_in2 = tsIn;
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateDTR(int employeeId, DateTime dtShiftDate, string tsIn, string tsOut, int ref_shift_id)
        {
            if (IsTimesheetExist(employeeId, dtShiftDate))
            {
                var record = db.employee_timesheet
                    .Where(a => a.employee_id == employeeId && a.shift_date == dtShiftDate).FirstOrDefault();
                record.time_in2 = tsIn;
                record.time_out2 = tsOut;
                record.ref_shift_id = ref_shift_id;
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool UpdateDTROut(int employeeId, DateTime dtShiftDate, string tsOut)
        {
            if (IsTimesheetExist(employeeId, dtShiftDate))
            {
                var record = db.employee_timesheet.FirstOrDefault(a => a.employee_id == employeeId && a.shift_date == dtShiftDate);
                record.time_out2 = tsOut;
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public decimal ComputeRenderHours(int employeeId, DateTime? dtShiftDate, int graceperiod)
        {
            decimal returnValue = 0;
            var record = db.employee_timesheet.FirstOrDefault(a => a.employee_id == employeeId && a.shift_date == dtShiftDate);
            var shift = db.ref_shift.FirstOrDefault(a => a.ref_shift_id == record.ref_shift_id);
            if (record != null)
            {
                TimeSpan timeIn = new TimeSpan(00, 00, 00);
                TimeSpan timeOut = new TimeSpan(00, 00, 00);

                TimeSpan shiftIn = TimeSpanConverter(shift.shift_in);
                TimeSpan shiftOut = TimeSpanConverter(shift.shift_out);

                if (GetInOut(record, shift, out timeIn, out timeOut))
                {
                    //TimeSpan result = timeOut.Subtract(timeIn);
                    TimeSpan result = ComputeWorkingHours(TimeSpanConverter(shift.shift_in), TimeSpanConverter(shift.shift_out), timeIn,
                        timeOut);

                    //Just compute Late and Tardi
                    decimal late = ComputeLate(employeeId, dtShiftDate, graceperiod);
                    decimal undertime = ComputeUndertime(employeeId, dtShiftDate);
                    decimal rendered = (Decimal)record.required_hour - (late + undertime);
                    return rendered;
                    //return Convert.ToDecimal((result.TotalHours).ToString("#.00")) >= (decimal)record.required_hour ? (decimal)record.required_hour : Convert.ToDecimal((result.TotalHours).ToString("#.00"));
                }
            }

            return returnValue;
        }

        public bool UpdateRenderHour(int employeeId, DateTime dtShiftDate, decimal renderHour)
        {
            if (IsTimesheetExist(employeeId, dtShiftDate))
            {
                var record = db.employee_timesheet.FirstOrDefault(a => a.employee_id == employeeId && a.shift_date == dtShiftDate);
                record.rendered_hour = renderHour;
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public TimeSpan TimeSpanConverter(string time)
        {
            string[] str = (time).Split(':');
            return new TimeSpan(int.Parse(str[0]), int.Parse(str[1]), 00);
        }

        public bool UpdateOt(int employeeId, DateTime dtShiftDate, string otIn, string otOut)
        {
            //CHECK if Login/logout exist
            //Check if OT range is outside shift hours
            //Check if OT range is inside shift hours
            try
            {
                if (IsTimesheetExist(employeeId, dtShiftDate))
                {
                    var record = db.employee_timesheet.FirstOrDefault(a => a.employee_id == employeeId && a.shift_date == dtShiftDate);
                    record.ot_in = otIn;
                    record.ot_out = otOut;
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool UpdateOt(int employeeId, DateTime dtShiftDate, decimal ot)
        {
            try
            {
                if (IsTimesheetExist(employeeId, dtShiftDate))
                {
                    var record = db.employee_timesheet.FirstOrDefault(a => a.employee_id == employeeId && a.shift_date == dtShiftDate);
                    record.ot = ot;
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool UpdateOt8(int employeeId, DateTime dtShiftDate, decimal ot)
        {
            try
            {
                if (IsTimesheetExist(employeeId, dtShiftDate))
                {
                    var record = db.employee_timesheet.FirstOrDefault(a => a.employee_id == employeeId && a.shift_date == dtShiftDate);
                    record.ot8 = ot;
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool UpdateUndertime(int employeeId, DateTime dtShiftDate, decimal countUnderTime)
        {
            try
            {
                if (IsTimesheetExist(employeeId, dtShiftDate))
                {
                    var record = db.employee_timesheet.FirstOrDefault(a => a.employee_id == employeeId && a.shift_date == dtShiftDate);
                    record.undertime = countUnderTime;
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool UpdateLate(int employeeId, DateTime dtShiftDate, decimal late)
        {
            try
            {
                if (IsTimesheetExist(employeeId, dtShiftDate))
                {
                    var record = db.employee_timesheet.FirstOrDefault(a => a.employee_id == employeeId && a.shift_date == dtShiftDate);
                    record.late = late;
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool UpdateLeave(int employeeId, DateTime dtShiftDate, decimal leave, int ref_leave_type_id)
        {
            try
            {
                if (IsTimesheetExist(employeeId, dtShiftDate))
                {
                    var record = db.employee_timesheet.FirstOrDefault(a => a.employee_id == employeeId && a.shift_date == dtShiftDate);
                    record.approve_leave = leave;
                    record.ref_leave_type_id = ref_leave_type_id;
                    record.absent = (leave * record.required_hour) == record.required_hour ? 0 : (leave * record.required_hour);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool UpdateAbsent(int employeeId, DateTime dtShiftDate, decimal absent)
        {
            try
            {
                if (IsTimesheetExist(employeeId, dtShiftDate))
                {
                    var record = db.employee_timesheet.FirstOrDefault(a => a.employee_id == employeeId && a.shift_date == dtShiftDate);
                    record.absent = absent;
                    record.rendered_hour = 0;
                    record.ot = 0;
                    record.ot8 = 0;
                    record.night_dif = 0;
                    record.night_dif8 = 0;
                    record.late = 0;
                    record.undertime = 0;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool UpdateAbsentToZero(int employeeId, DateTime dtShiftDate)
        {
            try
            {
                if (IsTimesheetExist(employeeId, dtShiftDate))
                {
                    var record = db.employee_timesheet.FirstOrDefault(a => a.employee_id == employeeId && a.shift_date == dtShiftDate);
                    record.absent = 0;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool IsAbsent(int employeeId, DateTime dtShiftDate)
        {
            try
            {
                var data = db.employee_timesheet
                .FirstOrDefault(a => a.employee_id == employeeId && a.shift_date == dtShiftDate);


                if (data != null)
                {
                    var shift = db.ref_shift.FirstOrDefault(a => a.ref_shift_id == data.ref_shift_id);
                    TimeSpan timeIn = new TimeSpan();
                    TimeSpan timeOut = new TimeSpan();

                    if (!GetInOut(data, shift, out timeIn, out timeOut))
                    {
                        if (RequestLeaveRepo.IsExist(employeeId, dtShiftDate))
                        {
                            return false;
                        }

                        if (RequestDTRRepo.IsExist(employeeId, dtShiftDate))
                        {
                            return false;
                        }
                        return true;
                    }
                    else
                    { return false; }
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        public bool UpdateNightDiffHours(int employeeId, DateTime dtShiftDate, decimal nd)
        {
            try
            {
                if (IsTimesheetExist(employeeId, dtShiftDate))
                {
                    var record = db.employee_timesheet.FirstOrDefault(a => a.employee_id == employeeId && a.shift_date == dtShiftDate);
                    record.night_dif = nd;
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        public bool UpdateNightDiffHours8(int employeeId, DateTime dtShiftDate, decimal nd8)
        {
            try
            {
                if (IsTimesheetExist(employeeId, dtShiftDate))
                {
                    var record = db.employee_timesheet.FirstOrDefault(a => a.employee_id == employeeId && a.shift_date == dtShiftDate);
                    record.night_dif8 = nd8;
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        public decimal ComputeUndertime(int employeeId, DateTime? dtShiftDate)
        {
            decimal returnValue = 0;
            var record = db.employee_timesheet.FirstOrDefault(a => a.employee_id == employeeId && a.shift_date == dtShiftDate);

            //RESTDAY
            if (record.ref_day_type_id == 2) return 0;

            var shift = db.ref_shift.FirstOrDefault(a => a.ref_shift_id == record.ref_shift_id);
            if (record != null)
            {
                TimeSpan timeIn = new TimeSpan(00, 00, 00);
                TimeSpan timeOut = new TimeSpan(00, 00, 00);

                if (GetInOut(record, shift, out timeIn, out timeOut))
                {
                    TimeSpan result = TimeSpanConverter(shift.shift_out).Subtract(timeOut);
                    return Convert.ToDecimal((result.TotalHours).ToString("#.00")) > 0 ? Convert.ToDecimal((result.TotalHours).ToString("#.00")) : 0;
                }
            }

            return returnValue;
        }

        public decimal ComputeLate(int employeeId, DateTime? dtShiftDate, int graceperiodMin)
        {
            decimal returnValue = 0;
            bool IncludeGracePeriod = RefConfigurationRepo.Includeraceperiod();
            var record = db.employee_timesheet.FirstOrDefault(a => a.employee_id == employeeId && a.shift_date == dtShiftDate);

            //RESTDAY
            if (record.ref_day_type_id == 2) return 0;

            var shift = db.ref_shift.FirstOrDefault(a => a.ref_shift_id == record.ref_shift_id);
            if (record != null)
            {
                TimeSpan timeIn = new TimeSpan(00, 00, 00);
                TimeSpan timeOut = new TimeSpan(00, 00, 00);

                if (GetInOut(record, shift, out timeIn, out timeOut))
                {
                    TimeSpan graceperiod = TimeSpan.FromMinutes((double)shift.grace_period);
                    var grace = TimeSpanConverter(shift.shift_in).Add(graceperiod);
                    if (timeIn <= TimeSpanConverter(shift.shift_in).Add(graceperiod))
                    {
                        timeIn = TimeSpanConverter(shift.shift_in);
                    }
                    TimeSpan result = timeIn.Subtract(TimeSpanConverter(shift.shift_in));

                    //Exclude the grace period (graceperiod) in the total late
                    if (!shift.include_grace_period)
                    {
                        result = result.Subtract(graceperiod);
                    }
                    return Convert.ToDecimal((result.TotalHours).ToString("#.00")) > 0 ? Convert.ToDecimal((result.TotalHours).ToString("#.00")) : 0;
                }
            }

            return returnValue;
        }

        //HR PROCESSING
        public bool ProcessTimesheet(int employeeId, DateTime dtCutoffStart, DateTime dtCutoffEnd)
        {
            //GET employee timesheet
            int gracePeriodMin = RefConfigurationRepo.GetGracePeriod();

            var data = db.employee_timesheet.Include(a => a.ref_shift_).Where(a =>
                  a.employee_id == employeeId && a.shift_date >= dtCutoffStart && a.shift_date <= dtCutoffEnd);

            foreach (var row in data)
            {
                //Check if Regular Working Day. If RD skip


                //Check if has In/Out
                //Check Leave
                //Check DTR
                if (IsAbsent(employeeId, (DateTime)row.shift_date))
                {
                    UpdateAbsent(employeeId, (DateTime)row.shift_date, (Decimal)row.required_hour);
                }
                else
                {
                    //SEEMS PRESENT SET ABSENT field to ZERO
                    UpdateAbsentToZero(employeeId, (DateTime)row.shift_date);

                    //Check DTR Correction
                    //UNIT Tested
                    string dtr_in = "";
                    string dtr_out = "";
                    int ref_shift_id = 0;
                    if (RequestDTRRepo.IsApproved(employeeId, (DateTime)row.shift_date, out dtr_in, out dtr_out, out ref_shift_id))
                    {
                        UpdateDTR(employeeId, (DateTime)row.shift_date, dtr_in, dtr_out, ref_shift_id);
                    }
                    //Check Leave
                    //UNIT Tested
                    decimal leave = 0;
                    int ref_leave_type = 0;
                    if (RequestLeaveRepo.GetApprovedLeave(employeeId, (DateTime)row.shift_date, out leave, out ref_leave_type))
                    {
                        UpdateLeave(employeeId, (DateTime)row.shift_date, leave, ref_leave_type);
                    }

                    //Check For Late
                    decimal late = ComputeLate(employeeId, (DateTime)row.shift_date, gracePeriodMin);
                    UpdateLate(employeeId, (DateTime)row.shift_date, late);

                    //Check for UnderTime
                    decimal undertime = ComputeUndertime(employeeId, (DateTime)row.shift_date);
                    UpdateUndertime(employeeId, (DateTime)row.shift_date, undertime);

                    //Check for filed OT
                    string ot_in = null;
                    string ot_out = null;
                    if (RequestOvertimeRepo.IsExist(employeeId, (DateTime)row.shift_date))
                    {
                        var ot = RequestOvertimeRepo.IsApproved(employeeId, (DateTime)row.shift_date, out ot_in, out ot_out);
                        if (ot)
                        {
                            row.ot_in = ot_in;
                            row.ot_out = ot_out;
                        }
                        UpdateOt(employeeId, (DateTime)row.shift_date, ot_in, ot_out);
                        decimal ot8 = 0;
                        decimal othr = ComputeOTHours(employeeId, (DateTime)row.shift_date, false, out ot8);
                        UpdateOt(employeeId, (DateTime)row.shift_date, othr);
                        UpdateOt8(employeeId, (DateTime)row.shift_date, ot8);
                    }
                    else
                    {
                        UpdateOt(employeeId, (DateTime)row.shift_date, 0);
                        UpdateOt8(employeeId, (DateTime)row.shift_date, 0);
                    }


                    //CHECK NIGHT DIFF
                    decimal nd8 = 0;
                    var nd = ND(employeeId, (DateTime)row.shift_date, out nd8);
                    UpdateNightDiffHours(employeeId, (DateTime)row.shift_date, nd);
                    UpdateNightDiffHours8(employeeId, (DateTime)row.shift_date, nd8);

                    //Rendered Hour
                    var renderedhour = ComputeRenderHours(employeeId, (DateTime)row.shift_date, gracePeriodMin);
                    UpdateRenderHour(employeeId, (DateTime)row.shift_date, renderedhour);

                    //Update Holiday
                    UpdateHoliday(employeeId, (DateTime)row.shift_date);
                }
            }

            return true;
        }

        public bool UpdateHoliday(int employeeId, DateTime dtShiftDate)
        {
            try
            {
                if (IsTimesheetExist(employeeId, dtShiftDate))
                {

                    var calendar = db.ref_calendar_activity.Include(a => a.ref_day_type_).Where(a => a.work_date == dtShiftDate).FirstOrDefault();

                    if (calendar != null)
                    {
                        var record = db.employee_timesheet.Include(a => a.ref_day_type_).FirstOrDefault(a => a.employee_id == employeeId && a.shift_date == dtShiftDate);

                        var holidayString = calendar.ref_day_type_.date_type_code;

                        //CHECK RWD
                        if (record.ref_day_type_.date_type_code == "RWD")
                        {
                            record.holiday_day_type_id = calendar.ref_day_type_.ref_day_type_id;
                            //record.ref_day_type_id = calendar.ref_day_type_.ref_day_type_id;
                        }
                        else
                        //CHECK RD
                        {
                            string searchKey = holidayString + "RD";
                            var refDayType = db.ref_day_type.Where(a => a.date_type_code == searchKey).FirstOrDefault();
                            record.holiday_day_type_id = refDayType.ref_day_type_id;
                        }

                        db.SaveChanges();
                    }

                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

        }
        public bool ProcessTimesheet(int employeeId, int payrollCutoff)
        {
            var cutoff = db.ref_payroll_cutoff.Where(a => a.ref_payroll_cutoff_id == payrollCutoff).FirstOrDefault();
            DateTime dtCutoffStart = cutoff.cutoff_date_start;
            DateTime dtCutoffEnd = cutoff.cutoff_date_end;
            return ProcessTimesheet(employeeId, dtCutoffStart, dtCutoffEnd);
        }

        public bool ProcessTimesheet(int payrollCutoff)
        {
            bool blnTrue = true;
            var cutoff = db.ref_payroll_cutoff.Where(a => a.ref_payroll_cutoff_id == payrollCutoff).FirstOrDefault();
            DateTime dtCutoffStart = cutoff.cutoff_date_start;
            DateTime dtCutoffEnd = cutoff.cutoff_date_end;
            var employee = db.employee.Where(a => a.date_deleted == null).ToList();
            foreach (var row in employee)
            {
                blnTrue = ProcessTimesheet(row.employee_id, dtCutoffStart, dtCutoffEnd);
                if (!blnTrue) break;
            }
            return blnTrue;
        }
        public EmployeeTimesheetEntity Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EmployeeTimesheetEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EmployeeTimesheetEntity> Find(Expression<Func<EmployeeTimesheetEntity, bool>> predecate)
        {
            throw new NotImplementedException();
        }

        public void Add(EmployeeTimesheetEntity entity)
        {
            var dataEntity = _mapper.Map<EmployeeTimesheetEntity, employee_timesheet>(entity);
            db.employee_timesheet.Add(dataEntity);
            db.SaveChanges();
        }

        public void AddRange(IEnumerable<EmployeeTimesheetEntity> entities)
        {
            throw new NotImplementedException();
        }

        public void Remove()
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<EmployeeTimesheetEntity> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(EmployeeTimesheetEntity entity)
        {
            throw new NotImplementedException();
        }

        public bool GetOTInOut(employee_timesheet record, out TimeSpan OTtimeIn, out TimeSpan OTtimeOut)
        {
            OTtimeIn = new TimeSpan(00, 00, 00);
            OTtimeOut = new TimeSpan(00, 00, 00);

            if (record.ot_in != null && record.ot_out != null)
            {
                OTtimeIn = TimeSpanConverter(record.ot_in);
                OTtimeOut = TimeSpanConverter(record.ot_out);
                return true;
            }
            return false;
        }
        public bool GetInOut(employee_timesheet record, ref_shift shift, out TimeSpan timeIn, out TimeSpan timeOut)
        {
            TimeSpan shiftIn = TimeSpanConverter(shift.shift_in);
            TimeSpan shiftOut = TimeSpanConverter(shift.shift_out);

            timeIn = new TimeSpan(00, 00, 00);
            timeOut = new TimeSpan(00, 00, 00);

            //IN
            if (record.time_in1 == null && record.time_in2 != null)
            {
                timeIn = TimeSpanConverter(record.time_in2);
            }
            else if (record.time_in1 != null && record.time_in2 == null)
            {
                timeIn = TimeSpanConverter(record.time_in1);
            }
            else if (record.time_in1 != null && record.time_in2 != null)
            {
                timeIn = TimeSpanConverter(record.time_in2);
            }
            else
            {
                return false;
            }


            //OUT
            if (record.time_out1 == null && record.time_out2 != null)
            {
                timeOut = TimeSpanConverter(record.time_out2);
            }
            else if (record.time_out1 != null && record.time_out2 == null)
            {
                timeOut = TimeSpanConverter(record.time_out1);
            }
            else if (record.time_out1 != null && record.time_out2 != null)
            {
                timeOut = TimeSpanConverter(record.time_out2);
            }
            else
            {
                return false;
            }
            //11:30 <= 12:00
            //if (timeIn <= shiftIn)
            //{
            //    timeIn = shiftIn;
            //}

            ////20:00 >= 22:00
            //if (timeOut >= shiftOut)
            //{
            //    timeOut = shiftOut;
            //}

            return true;
        }

        public decimal ND(int employeeId, DateTime? dtShiftDate, out decimal nd8)
        {
            nd8 = 0;
            var record = db.employee_timesheet.FirstOrDefault(a => a.employee_id == employeeId && a.shift_date == dtShiftDate);

            if (record == null)
            {
                return 0;
            }
            var current_shift = db.ref_shift.Where(a => a.ref_shift_id == record.ref_shift_id).FirstOrDefault();

            if (!current_shift.is_nd)
            {
                return 0;
            }

            if (current_shift.nd_start == null || current_shift.nd_end == null)
            {
                return 0;
            }

            if (current_shift.nd_start.Length == 0 || current_shift.nd_end.Length == 0)
            {
                return 0;
            }
            TimeSpan timeIn = new TimeSpan(00, 00, 00);
            TimeSpan timeOut = new TimeSpan(00, 00, 00);
            TimeSpan shift_in = TimeSpanConverter(current_shift.shift_in);
            TimeSpan shift_out = TimeSpanConverter(current_shift.shift_out);

            TimeSpan night_In = TimeSpanConverter(current_shift.nd_start);
            TimeSpan night_Out = TimeSpanConverter(current_shift.nd_end);
            GetInOut(record, current_shift, out timeIn, out timeOut);

            //Check overtime
            TimeSpan OTtimeIn = new TimeSpan(00, 00, 00);
            TimeSpan OTtimeOut = new TimeSpan(00, 00, 00);
            bool hasoT = GetOTInOut(record, out OTtimeIn, out OTtimeOut);

            var shift = TimeUtils.Intersect(
                shift_in, shift_out,
                night_In, night_Out
            );

            var actual = TimeUtils.Intersect(
                timeIn, timeOut,
                night_In, night_Out
            );

            //Check if there is an OT and actual IN/OUT intersect
            if (hasoT && actual.TotalHours > 0 && shift.TotalHours== 0)
            {
                var ot_nd = TimeUtils.Intersect(
                    OTtimeIn, OTtimeOut,
                    night_In, night_Out
                );

                var ot_actual = TimeUtils.Intersect(
                    OTtimeIn, OTtimeOut,
                    timeIn, timeOut
                );

                if (ot_actual >= ot_nd)
                {
                    
                    if ((decimal)ot_nd.TotalHours > 8)
                    {
                        nd8 = (decimal)ot_nd.TotalHours > 8 ? (decimal)ot_nd.TotalHours - 8 : 0;
                        return 8;
                    }
                    return (decimal)ot_nd.TotalHours;
                }
            }

            //Since shift did not intersect the nighshift. Return 0
            if (shift.TotalHours == 0)
            {
                return 0;
            }

            //Both shift and actual intersect the night shift hours
            if (shift.TotalHours > 0 && actual.TotalHours > 0)
            {
                if (shift.TotalHours > actual.TotalHours)
                {
                    if ((decimal)actual.TotalHours > 8)
                    {
                        nd8 = (decimal)actual.TotalHours > 8 ? (decimal)actual.TotalHours - 8 : 0;
                        return 8;
                    }
                    return (decimal)actual.TotalHours;
                }
                else if (actual.TotalHours > shift.TotalHours)
                {
                    if ((decimal)shift.TotalHours > 8)
                    {
                        nd8 = (decimal)shift.TotalHours > 8 ? (decimal)shift.TotalHours - 8 : 0;
                        return 8;
                    }
                    return (decimal)shift.TotalHours;
                }
                else
                {
                    if ((decimal)shift.TotalHours > 8)
                    {
                        nd8 = (decimal)shift.TotalHours > 8 ? (decimal)shift.TotalHours - 8 : 0;
                        return 8;
                    }
                    return (decimal)shift.TotalHours;
                }
                
            }

            string test = "";
            //if (record != null)
            //{
            //    string strnight_In = employeeshift.nd_start;
            //    string strnight_Out = employeeshift.nd_end;
            //    string strnight_In2 = "";
            //    string strnight_Out2 = "";

            //    //RefConfigurationRepo.GetNighDiffHr1(out strnight_In, out strnight_Out);
            //    // RefConfigurationRepo.GetNighDiffHr1(out strnight_In2, out strnight_Out2);

            //    TimeSpan night_In = TimeSpanConverter(strnight_In);
            //    TimeSpan night_Out = TimeSpanConverter(strnight_Out);

            //    TimeSpan night_In2 = TimeSpanConverter(strnight_In2);
            //    TimeSpan night_Out2 = TimeSpanConverter(strnight_Out2);



            //    TimeSpan timeIn = new TimeSpan(00, 00, 00);
            //    TimeSpan timeOut = new TimeSpan(00, 00, 00);

            //    TimeSpan OTtimeIn = new TimeSpan(00, 00, 00);
            //    TimeSpan OTtimeOut = new TimeSpan(00, 00, 00);

            //    var current_shift = db.ref_shift.FirstOrDefault(a => a.ref_shift_id == record.ref_shift_id);
            //    TimeSpan shift_in = TimeSpanConverter(current_shift.shift_in);
            //    TimeSpan shift_out = TimeSpanConverter(current_shift.shift_out);
            //    GetInOut(record, current_shift, out timeIn, out timeOut);
            //    bool hasoT = GetOTInOut(record, out OTtimeIn, out OTtimeOut);


            //    //SHIFT
            //    var shift = TimeUtils.Intersect(
            //        shift_in, shift_out,
            //        night_In, night_Out
            //    );
            //    var shift1 = TimeUtils.Intersect(
            //        shift_in, shift_out,
            //        night_In, night_Out
            //    );
            //    var shift2 = TimeUtils.Intersect(
            //        shift_in, shift_out,
            //        night_In2, night_Out2
            //    );

            //    if (shift.TotalHours > 0 || shift2.TotalHours > 0)
            //    {
            //        decimal shifthr = 0;
            //        decimal actualhr = 0;
            //        decimal othr = 0;

            //        //MEANS SHIFT is inside ND(Check actual in and ot)
            //        var actual = new TimeSpan(00, 00, 00);
            //        //OT

            //        var otss = TimeUtils.Intersect(
            //            OTtimeIn, OTtimeOut, //OT
            //            night_In, night_Out  //ND
            //        );

            //        if (shift.TotalHours > 0)
            //        {
            //            shifthr = Math.Floor(Convert.ToDecimal((shift.TotalHours).ToString("#.00")));
            //            actual = TimeUtils.Intersect(
            //               timeIn, timeOut, //ACTUAL
            //               night_In, night_Out //ND
            //             );
            //        }
            //        if (shift2.TotalHours > 0)
            //        {
            //            shifthr = Math.Floor(Convert.ToDecimal((shift2.TotalHours).ToString("#.00")));
            //            actual = TimeUtils.Intersect(
            //               timeIn, timeOut, //ACTUAL
            //               night_In2, night_Out2 //ND
            //             );
            //        }

            //        actualhr = Math.Floor(Convert.ToDecimal((actual.TotalHours).ToString("#.00")));

            //        if (hasoT)
            //        {
            //            othr = Math.Floor(Convert.ToDecimal((otss.TotalHours).ToString("#.00")));
            //        }

            //        if (actualhr >= (shifthr + othr))
            //        {
            //            return (shifthr + othr);
            //        }
            //        else
            //        {
            //            return actualhr;
            //        }
            //    }
            //    else
            //    {
            //        //MEANS OT ONLY
            //        //OT
            //        if (hasoT)
            //        {
            //            var ott = TimeUtils.Intersect(
            //               OTtimeIn, OTtimeOut, //OT
            //               night_In, night_Out  //ND
            //            );

            //            var actual = TimeUtils.Intersect(
            //               timeIn, timeOut, //ACTUAL
            //               night_In, night_Out //ND
            //            );

            //            if (actual >= ott)
            //            {
            //                decimal nd = Math.Floor(Convert.ToDecimal((ott.TotalHours).ToString("#.00")));
            //                if (nd > 0)
            //                {
            //                    nd8 = nd > 8 ? nd - 8 : 0;
            //                    return nd;
            //                }
            //            }
            //            else
            //            {
            //                decimal nd = Math.Floor(Convert.ToDecimal((actual.TotalHours).ToString("#.00")));
            //                if (nd > 0)
            //                {
            //                    nd8 = nd > 8 ? nd - 8 : 0;
            //                    return nd;
            //                }
            //            }
            //        }

            //    }

            //}

            return 0;
        }

        public decimal ComputeNightDiffHours(int employeeId, DateTime? dtShiftDate, out decimal nd8)
        {
            nd8 = 0;


            var record = db.employee_timesheet.FirstOrDefault(a => a.employee_id == employeeId && a.shift_date == dtShiftDate);
            if (record != null)
            {
                RefConfigurationRepo.GetNighDiffHr1(out string ns_in, out string ns_out);
                TimeSpan night_In = TimeSpanConverter(ns_in);
                TimeSpan night_Out = TimeSpanConverter(ns_out);

                TimeSpan timeIn = new TimeSpan(00, 00, 00);
                TimeSpan timeOut = new TimeSpan(00, 00, 00);

                var shift = db.ref_shift.FirstOrDefault(a => a.ref_shift_id == record.ref_shift_id);

                if (GetInOut(record, shift, out timeIn, out timeOut))
                {
                    TimeSpan shift_in = TimeSpanConverter(shift.shift_in);
                    TimeSpan shift_out = TimeSpanConverter(shift.shift_out);

                    //Check if Shift is between 10PM-6AM
                    TimeSpan checkNightDif = shift_out.Subtract(night_In);
                    decimal checknd = Math.Floor(Convert.ToDecimal((checkNightDif.TotalHours).ToString("#.00")));
                    if (checknd > 0)
                    {
                        TimeSpan result = new TimeSpan(00, 00, 00);
                        result = timeOut.Subtract(night_In);
                        decimal nd = Math.Floor(Convert.ToDecimal((result.TotalHours).ToString("#.00")));
                        nd8 = nd > 8 ? nd - 8 : 0;
                        return nd;
                    }

                    //Check if Just filed OT between 10PM-6AM
                    //IN
                    if (record.ot_in != null && record.ot_out != null)
                    {
                        TimeSpan ot_in = TimeSpanConverter(record.ot_in);
                        TimeSpan ot_out = TimeSpanConverter(record.ot_out);

                        if (ot_in <= night_In)
                        {
                            ot_in = night_In;
                        }

                        if (ot_out >= night_Out)
                        {
                            ot_out = night_Out;
                        }

                        //Check if ot_out is after 10PM
                        if (ot_out > night_In && ot_out < night_Out)
                        {
                            checkNightDif = ot_out.Subtract(ot_in);
                        }
                        else if (ot_out > night_In && ot_out >= night_Out)
                        {
                            checkNightDif = night_Out.Subtract(ot_in);
                        }
                        else if (ot_out == night_In && ot_out < night_Out)
                        {
                            checkNightDif = ot_out.Subtract(night_In);
                        }
                        else if (ot_out == night_In && ot_out >= night_Out)
                        {
                            checkNightDif = night_Out.Subtract(night_In);
                        }

                        checknd = Math.Floor(Convert.ToDecimal((checkNightDif.TotalHours).ToString("#.00")));
                        if (checknd > 0)
                        {
                            TimeSpan result = timeOut.Subtract(night_In);
                            decimal nd = 0;
                            nd8 = checknd > 8 ? checknd - 8 : 0;
                            return checknd;
                        }
                    }

                    //if (timeOut > night_In)
                    //{
                    //    TimeSpan result = timeOut.Subtract(night_In);
                    //    decimal nd = Math.Floor(Convert.ToDecimal((result.TotalHours).ToString("#.00")));
                    //    nd8 = nd > 8 ? nd - 8 : 0;
                    //    return nd;
                    //}
                }
            }

            return 0;
        }

        public decimal ComputeOTHours(int employeeId, DateTime? dtShiftDate, bool AutoOT, out decimal ot8)
        {
            ot8 = 0;
            decimal otFinal = 0;
            decimal returnValue = 0;
            var record = db.employee_timesheet.Include(a => a.ref_day_type_).FirstOrDefault(a => a.employee_id == employeeId && a.shift_date == dtShiftDate);
            if (record != null)
            {
                //GET SHIFT
                var shift = db.ref_shift.Where(a => a.ref_shift_id == record.ref_shift_id).FirstOrDefault();
                TimeSpan NullEmptyValue = new TimeSpan(00, 00, 00);
                TimeSpan shift_in = new TimeSpan(00, 00, 00);
                TimeSpan shift_out = new TimeSpan(00, 00, 00);
                TimeSpan actual_in = new TimeSpan(00, 00, 00);
                TimeSpan actual_out = new TimeSpan(00, 00, 00);


                shift_in = TimeSpanConverter(shift.shift_in);
                shift_out = TimeSpanConverter(shift.shift_out);
                GetInOut(record, shift, out actual_in, out actual_out);
                if (actual_in == NullEmptyValue || actual_out == NullEmptyValue)
                    return Convert.ToDecimal((0).ToString("#.00"));

                TimeSpan ot_in = new TimeSpan(00, 00, 00);
                TimeSpan ot_out = new TimeSpan(00, 00, 00);

                if (AutoOT)
                {
                    //CHECKING SHIFT. IF Undertime no OT
                    //Considered undertime
                    if (actual_out < shift_out)
                    { return 0; }

                    if (actual_out > shift_out)
                    {
                        ot_in = shift_out;
                        ot_out = actual_out;
                    }

                    TimeSpan resultOt = ot_out.Subtract(ot_in);
                    decimal otRD = Convert.ToDecimal((resultOt.TotalHours).ToString("#.00"));

                    //Return 0 if did not make in minOT required

                    otRD = GetOTandOT8(otRD, out ot8);
                    return otRD;


                }

                //Check if has OT filed
                if (record.ot_in != null && record.ot_out != null)
                {
                    ot_in = TimeSpanConverter(record.ot_in);
                    ot_out = TimeSpanConverter(record.ot_out);
                }

                //Check if RD
                if (record.ref_day_type_.date_type_code == "RD" ||
                    record.holiday_day_type_id != null
                    )
                {
                    //Rest Day OT
                    // Check and update the actual IN/OUT vs with filed OT
                    if (actual_in > ot_in)
                    {
                        ot_in = actual_in;
                    }
                    if (ot_out > actual_out)
                    {
                        ot_out = actual_out;
                    }

                    TimeSpan resultOt = ot_out.Subtract(ot_in);
                    decimal otRD = Convert.ToDecimal((resultOt.TotalHours).ToString("#.00"));
                    //Return 0 if did not make in minOT required

                    otRD = GetOTandOT8(otRD, out ot8);
                    return otRD;
                }

                //Check if RWD

                if (record.ref_day_type_.date_type_code == "RWD")
                {
                    //CHECKING SHIFT. IF Undertime no OT
                    //Considered undertime
                    if (actual_out < shift_out)
                    { return 0; }

                    if (ot_in > actual_out)
                    { return 0; }

                    //CHECK OT and ACTUAL OUT
                    if (ot_out > actual_out)
                    { ot_out = actual_out; }


                    //Checking OT below shift out
                    if (ot_in < shift_out)
                    {
                        ot_in = shift_out;
                    }


                    TimeSpan resultOt = ot_out.Subtract(ot_in);
                    decimal otRWD = Convert.ToDecimal((resultOt.TotalHours).ToString("#.00"));

                    otRWD = GetOTandOT8(otRWD, out ot8);
                    return otRWD;
                }
            }

            return returnValue;
        }

        public decimal GetOTandOT8(decimal Ot, out decimal ot8)
        {
            ot8 = 0;
            int minOT = RefConfigurationRepo.GetMinimumOvertime();
            if (Ot > minOT)
            {
                //CHECK for OT after 8hours
                if (Ot > 8)
                {
                    ot8 = Ot > 8 ? Ot - 8 : 0;
                    Ot = Ot - ot8;
                }
                return Ot;
            }

            return 0;
        }
        public TimeSpan ComputeWorkingHours(TimeSpan shiftStart, TimeSpan shiftEnd, TimeSpan actualIn,
             TimeSpan actualOut)
        {
            TimeSpan returnValue = new TimeSpan(00, 00, 00);

            if (actualIn <= shiftStart)
            {
                actualIn = shiftStart;
            }

            if (actualOut >= shiftEnd)
            {
                actualOut = shiftEnd;
            }


            return actualOut.Subtract(actualIn);
        }

        public decimal ComputeLate(DateTime actualdtIn, DateTime actualdtOut, DateTime shiftIn, DateTime shiftOut)
        {
            throw new NotImplementedException();
        }


        public bool IsApproveLeave(int employeeId, int employeeTimesheetId)
        {
            throw new NotImplementedException();
        }

        public bool IsHoliday(int employeeId, DateTime shiftDate)
        {
            var calendar = db.ref_calendar_activity.Where(a => a.work_date == shiftDate).FirstOrDefault();

            if (calendar != null) return true;

            return false;
        }

        public bool IsRestDay(int employeeId, DateTime shiftDate, int shiftId)
        {
            throw new NotImplementedException();
        }

        public bool TagAsProcessed(int employeeTimesheetId)
        {
            throw new NotImplementedException();
        }

        public bool CheckDateAlreadyUsed(int employeeId, DateTime dtShiftDate)
        {
            throw new NotImplementedException();
        }





        public IEnumerable<EmployeeTimesheetEntity> GetAllEmployeeTimeSheet(int employeeId, DateTime dateStart, DateTime dateEnd)
        {
            IEnumerable<EmployeeTimesheetEntity> record;

            var data = db.employee_timesheet.Include(a => a.ref_day_type_).Include(a => a.ref_shift_).Where(a => a.employee_id == employeeId && a.shift_date >= dateStart && a.shift_date <= dateEnd).ToList();
            record = _mapper.Map<IEnumerable<employee_timesheet>, IEnumerable<EmployeeTimesheetEntity>>(data);

            return record;
        }

        public IEnumerable<EmployeeTimesheetEntity> GetUnprocessedTimeSheet(DateTime dateStart, DateTime dateEnd)
        {
            IEnumerable<EmployeeTimesheetEntity> record;
            var data = db.employee_timesheet.Where(a => a.shift_date >= dateStart && a.shift_date <= dateEnd && a.payroll_process == false);
            record = _mapper.Map<IEnumerable<employee_timesheet>, IEnumerable<EmployeeTimesheetEntity>>(data);

            return record;
        }

        public IEnumerable<EmployeeTimesheetEntity> GetProcessedTimeSheet(DateTime dateStart, DateTime dateEnd)
        {
            IEnumerable<EmployeeTimesheetEntity> record;
            var data = db.employee_timesheet.Where(a => a.shift_date >= dateStart && a.shift_date <= dateEnd && a.payroll_process == true);
            record = _mapper.Map<IEnumerable<employee_timesheet>, IEnumerable<EmployeeTimesheetEntity>>(data);

            return record;
        }

        public List<EmployeeTimesheetEntity> GetIncompleteTimesheet(DateTime dateStart, DateTime dateEnd)
        {
            //What will be the condition
            throw new NotImplementedException();
        }

        public List<EmployeeTimesheetEntity> GetIncompleteTimesheet(int employeeId, DateTime dateStart, DateTime dateEnd)
        {
            //What will be the condition
            throw new NotImplementedException();
        }

        public IEnumerable<EmployeeTimesheetDTREntity> GetAllEmployeeTimeSheet(int employeeId, int ref_payroll_cutoff_id)
        {
            var cutoff = db.ref_payroll_cutoff.Where(a => a.ref_payroll_cutoff_id == ref_payroll_cutoff_id).FirstOrDefault();
            DateTime dateStart = cutoff.cutoff_date_start;
            DateTime dateEnd = cutoff.cutoff_date_end;
            var record = db.employee_timesheet.Include(a => a.employee_).Include(a => a.ref_day_type_).Include(a => a.ref_shift_).Where(a => a.employee_id == employeeId && a.shift_date >= dateStart && a.shift_date <= dateEnd).
                Select(a => new EmployeeTimesheetDTREntity
            {
                employee_timesheet_id = a.employee_timesheet_id,
                rendered_hour = a.rendered_hour,
                last_name = a.employee_.last_name,
                first_name = a.employee_.first_name,
                shift_date = a.shift_date,
                shift_name = a.ref_shift_.shift_name,
                date_type_code = a.ref_day_type_.date_type_code,
                required_hour = a.required_hour,
                time_in1 = a.time_in1,
                time_out1 = a.time_out1,
                ot_in = a.ot_in,
                ot_out = a.ot_out,
                late = a.late,
                undertime = a.undertime,
                ot = a.ot,
                ot8 = a.ot8,
                night_dif = a.night_dif,
                absent = a.absent,
            }).ToList();
            
            
            
            //record = _mapper.Map<IEnumerable<employee_timesheet>, IEnumerable<EmployeeTimesheetEntity>>(data);

            return record;
        }

        public IEnumerable<EmployeeTimesheetDTREntity> GetAllEmployeeTimeSheetDTR(int group_id,int ref_payroll_cutoff_id)
        {
            var cutoff = db.ref_payroll_cutoff.Where(a => a.ref_payroll_cutoff_id == ref_payroll_cutoff_id).FirstOrDefault();
            DateTime dateStart = cutoff.cutoff_date_start;
            DateTime dateEnd = cutoff.cutoff_date_end;
            var data = db.employee_timesheet_dto.FromSql(string.Format(sql, group_id)).Where(a => a.shift_date >= dateStart && a.shift_date <= dateEnd)
                .Select(a=> new EmployeeTimesheetDTREntity {
                    rendered_hour=a.rendered_hour,
                    last_name=a.last_name,
                    first_name = a.first_name,
                    shift_date = a.shift_date,
                    shift_name = a.shift_name,
                    date_type_code = a.date_type_code,
                    required_hour = a.required_hour,
                    time_in1=a.time_in1,
                    time_out1 = a.time_out1,
                    ot_in = a.ot_in,
                    ot_out = a.ot_out,
                    late = a.late,
                    undertime = a.undertime,
                    ot = a.ot,
                    ot8 = a.ot8,
                    night_dif = a.night_dif,
                    absent = a.absent,
                })
                .ToList();


            
            return data;
        }

        public EmployeeTimesheetEntity GetAllEmployeeTimeSheetSummarized(int employeeId, int ref_payroll_cutoff_id)
        {
            var cutoff = db.ref_payroll_cutoff.Where(a => a.ref_payroll_cutoff_id == ref_payroll_cutoff_id).FirstOrDefault();
            DateTime dateStart = cutoff.cutoff_date_start;
            DateTime dateEnd = cutoff.cutoff_date_end;
            var data = db.employee_timesheet.Include(a => a.ref_day_type_).Include(a => a.ref_shift_).Where(a => a.employee_id == employeeId && a.shift_date >= dateStart && a.shift_date <= dateEnd).ToList();
            var getd = _mapper.Map<IEnumerable<employee_timesheet>, IEnumerable<EmployeeTimesheetEntity>>(data);
            var records = getd.GroupBy(a => a.employee_id).Select(g => new EmployeeTimesheetEntity
            {
                required_hour = g.Sum(a => a.required_hour),
                rendered_hour = g.Sum(a => a.rendered_hour),
                ot = g.Sum(a => a.ot),
                night_dif = g.Sum(a => a.night_dif),
                late = g.Sum(a => a.late),
                undertime = g.Sum(a => a.undertime)
            }).FirstOrDefault();
            return records;
        }

        public bool UpdateTimesheet(EmployeeTimesheetTemp data)
        {
            try
            {
                int id = int.Parse(data.empid);
                var result = db.employee_timesheet.Where(a => a.employee_timesheet_id == id).FirstOrDefault();
                result.time_in1 = data.timein;
                result.time_out1 = data.timeout;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateTimesheetFromDevice(EmployeeTimesheetTempDevice data)
        {
            try
            {
                var employee = db.employee.Where(a => a.fingerprint == data.empid).FirstOrDefault();
                if (employee != null)
                {
                    var result = db.employee_timesheet.Where(a => a.shift_date == data.shiftDate).FirstOrDefault();
                    if (result != null)
                    {
                        result.time_in1 = data.timein;
                        result.time_out1 = data.timeout;
                        db.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

    public static class TimeUtils
    {
        static readonly TimeSpan DayStart = new TimeSpan(0, 0, 0);
        static readonly TimeSpan DayEnd = new TimeSpan(24, 0, 0);
        public static TimeSpan Intersect(TimeSpan startA, TimeSpan endA, TimeSpan startB, TimeSpan endB)
        {
            if (startA < endA)
            {
                if (startB < endB)
                    return IntersectCore(startA, endA, startB, endB);
                else
                    return IntersectCore(startA, endA, startB, DayEnd)
                        + IntersectCore(startA, endA, DayStart, endB);
            }
            else
            {
                if (startB < endB)
                    return IntersectCore(startA, DayEnd, startB, endB)
                        + IntersectCore(DayStart, endA, startB, endB);
                else
                    return IntersectCore(startA, DayEnd, startB, DayEnd)
                        + IntersectCore(startA, DayEnd, DayStart, endB)
                        + IntersectCore(DayStart, endA, startB, DayEnd)
                        + IntersectCore(DayStart, endA, DayStart, endB);
            }
        }


        private static TimeSpan IntersectCore(TimeSpan startA, TimeSpan endA, TimeSpan startB, TimeSpan endB)
        {
            return Max(Min(endA, endB) - Max(startA, startB), TimeSpan.Zero);
        }
        public static TimeSpan Max(TimeSpan a, TimeSpan b)
        {
            return a > b ? a : b;
        }
        public static TimeSpan Min(TimeSpan a, TimeSpan b)
        {
            return a < b ? a : b;
        }
    }
}
