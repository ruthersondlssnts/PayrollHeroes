using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TPS.Infrastructure;
using TPS.Infrastructure.DTO;
using TPS.Infrastructure.Enums;
using TPS.Infrastructure.Models;
using TPS.Services.Interfaces;
using TPS.Services.Utility;

namespace TPS.Services.Services
{
    public class EmployeeTimesheetService : IEmployeeTimesheetService
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
        private readonly IMessageBroker _messageBroker;
        public EmployeeTimesheetService(
            IDBService<EmployeeTimesheet> data,
            IDBService<Employee> dataEmployee,
            IDBService<RefPayrollCutoff> dataPayrollCutoff,
            IDBService<BiometricsData> dataBiometrics,
            IDBService<RequestOvertime> dataRequestOt,
            IDBService<RequestLeave> dataRequestLeave,
            IDBService<RequestDtr> dataRequestDtr,
            IDBService<RefCalendarActivities> dataCalendarActivities,
            IDBService<RequestChangeShift> dataRequestChangeShift,
            IMessageBroker messageBroker
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
            _messageBroker = messageBroker;
            _dataCalendarActivities = dataCalendarActivities;

            _messageBroker.Connect();
        }

        public async Task<ApiResponse<StatusCode>> Create(EmployeeTimesheet entity)
        {
            await _data.InsertOneAsync(entity);
            return new ApiResponse<StatusCode>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString()
            };
        }

        public async Task<ApiResponse<StatusCode>> Delete(string id)
        {
            await _data.DeleteOneAsync(id);
            return new ApiResponse<StatusCode>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString()
            };
        }

        public async Task<ApiResponse<EmployeeTimesheet>> FindById(string id)
        {
            return new ApiResponse<EmployeeTimesheet>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString(),
                Result = _data.FindById(id)
            };
        }

        public ApiResponse<IEnumerable<EmployeeTimesheet>> FindByEmployeeId(string employeeId)
        {
            return new ApiResponse<IEnumerable<EmployeeTimesheet>>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString(),
                Result = _data.FilterBy(x => x.EmployeeId == employeeId)
            };
        }
        public ApiResponse<IEnumerable<EmployeeTimesheet>> FindByCutoffEmployeeId(string cutoff, string employeeId)
        {
            return new ApiResponse<IEnumerable<EmployeeTimesheet>>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString(),
                Result = _data.FilterBy(x => x.EmployeeId == employeeId && x.PayrollCutoffId == cutoff)
            };
        }

        public ApiResponse<IEnumerable<DTODtrCalendar>> FindDtrEmployeeByDate(string employeeId, DateTime start, DateTime end)
        {
            List<DTODtrCalendar> returnData = new List<DTODtrCalendar>();
            var dtr = _data.FilterBy(x => x.EmployeeId == employeeId && x.ShiftDate > start && x.ShiftDate < end);
            returnData.AddRange(dtr.Select(x=> new DTODtrCalendar 
            {
                Title = "DTR " + (x.ShiftIn != "" ? "(" + x.DayTypeName + ")" + x.ShiftIn + "-" + x.ShiftOut : "(" + x.DayTypeName + ")"),
                Absent = x.Absent > 0 ? true : false,
                Content = "<b>Shift:</b> " + (x.ShiftIn != "" ? "(" + x.DayTypeName + ")" + x.ShiftIn + "-" + x.ShiftOut : "(" + x.DayTypeName + ")") + "<br><b>ActualIn:</b> " + (x.ActualIn != null ? "(In)" + x.ActualIn.Value.ToString("HH:mm") : "(In)None") + "<br><b>ActualOut:</b> " + (x.ActualOut != null ? "(Out)" + x.ActualOut.Value.ToString("HH:mm") : "(Out)None"),
                Start = x.ShiftDate,
                End = x.ShiftDate,
                Color = x.Absent > 0 ? "#FF0000" : "#008000"
            }));

            var leave = _dataRequestLeave.FilterBy(x => x.EmployeeId == employeeId && x.DateStart > start && x.DateEnd < end && x.ApprovalStatusId == (int)ApprovalStatus.APPROVED);
            returnData.AddRange(leave.Select(x => new DTODtrCalendar
            {
                Title = "Leave " + x.LeaveTypeName,
                Absent = false,
                Content = "<b>Remarks:</b> " +x.Remarks,
                Start = x.DateStart,
                End = x.DateEnd,
                Color = "#808000",
            }));

            var overtime = _dataRequestOt.FilterBy(x => x.EmployeeId == employeeId && x.OvertimeDate > start && x.OvertimeDate < end && x.ApprovalStatusId == (int)ApprovalStatus.APPROVED);
            returnData.AddRange(overtime.Select(x => new DTODtrCalendar
            {
                Title = "Overtime " + x.OvertimeTypeName,
                Absent = false,
                Content = "<b>Remarks:</b> " + x.Remarks + "<br /><b>In:</b>" + x.OvertimeTimeIn.Value.ToString("HH:mm") + "<br /><b>Out:</b>" + x.OvertimeTimeOut.Value.ToString("HH:mm"),
                Start = x.OvertimeDate,
                End = x.OvertimeDate,
                Color = "#C0C0C0"
            }));

            var dtrrequest = _dataRequestDtr.FilterBy(x => x.EmployeeId == employeeId && x.ShiftDate > start && x.ShiftDate < end && x.ApprovalStatusId == (int)ApprovalStatus.APPROVED);
            returnData.AddRange(dtrrequest.Select(x => new DTODtrCalendar
            {
                Title = "DTR Request: " + x.Remarks,
                Absent = false,
                Content = "<b>DTRIn:</b> " + (x.ActualIn != null ? "(In)" + x.ActualIn.Value.ToString("HH:mm") : "(In)None") + "<br><b>DTROut:</b> " + (x.ActualOut != null ? "(Out)" + x.ActualOut.Value.ToString("HH:mm") : "(Out)None"),
                Start = x.ShiftDate,
                End = x.ShiftDate,
                Color = "#C0C0C0"
            }));

            return new ApiResponse<IEnumerable<DTODtrCalendar>>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString(),
                Result = returnData
            };
        }

        public ApiResponse<IEnumerable<EmployeeTimesheet>> GetAll()
        {
            return new ApiResponse<IEnumerable<EmployeeTimesheet>>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString(),
                Result = _data.FilterBy(x => x.DateDeleted == null)
            };
        }

        public async Task<ApiResponse<StatusCode>> Update(EmployeeTimesheet entity)
        {
            await _data.ReplaceOneAsync(entity);
            return new ApiResponse<StatusCode>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString()
            };
        }

        public async Task<ApiResponse<StatusCode>> GenerateTimesheet(DTOPayload request)
        {
            int getNumbers = Math.Abs(new Guid(request.Employee.Id.ToString()).GetHashCode()) % 10;
            _messageBroker.Publish(request, "GenerateTimesheet", "q.tps." + getNumbers, Guid.NewGuid().ToString());

            return new ApiResponse<StatusCode>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString()
            };
        }

        public async Task<ApiResponse<StatusCode>> GetActualInOut(DTOPayload request)
        {
            int getNumbers = Math.Abs(new Guid(request.Employee.Id.ToString()).GetHashCode()) % 10;
            _messageBroker.Publish(request, "GetActualInOut", "q.tps." + getNumbers, Guid.NewGuid().ToString());
            //_messageBroker.Publish(payload, "GetActualInOut", "q.tps.1", Guid.NewGuid().ToString());

            return new ApiResponse<StatusCode>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString()
            };
        }

        public async Task<ApiResponse<StatusCode>> ComputeWorkingHourByShift(DTOPayload request)
        {
            int getNumbers = Math.Abs(new Guid(request.Employee.Id.ToString()).GetHashCode()) % 10;
            _messageBroker.Publish(request, "ComputeWorkingHourByShift", "q.tps." + getNumbers % 10, Guid.NewGuid().ToString());
            
            return new ApiResponse<StatusCode>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString()
            };
        }

        public async Task<ApiResponse<StatusCode>> ComputeApprovedDtrHourByShift(DTOPayload request)
        {
            int getNumbers = Math.Abs(new Guid(request.Employee.Id.ToString()).GetHashCode()) % 10;
            _messageBroker.Publish(request, "ComputeApprovedDtrHourByShift", "q.tps." + getNumbers % 10, Guid.NewGuid().ToString());
            
            return new ApiResponse<StatusCode>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString()
            };
        }

        public async Task<ApiResponse<StatusCode>> ComputeApprovedOvertimeHr(DTOPayload request)
        {
            int getNumbers = Math.Abs(new Guid(request.Employee.Id.ToString()).GetHashCode()) % 10;
            _messageBroker.Publish(request, "ComputeApprovedOvertimeHr", "q.tps." + getNumbers % 10, Guid.NewGuid().ToString());

            return new ApiResponse<StatusCode>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString()
            };
        }

        public async Task<ApiResponse<StatusCode>> ComputeNightShiftHours(DTOPayload request)
        {
            int getNumbers = Math.Abs(new Guid(request.Employee.Id.ToString()).GetHashCode()) % 10;
            _messageBroker.Publish(request, "ComputeNightShiftHours", "q.tps." + getNumbers % 10, Guid.NewGuid().ToString());

            return new ApiResponse<StatusCode>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString()
            };
        }

        public async Task<ApiResponse<StatusCode>> ComputeLate(DTOPayload request)
        {
            int getNumbers = Math.Abs(new Guid(request.Employee.Id.ToString()).GetHashCode()) % 10;
            _messageBroker.Publish(request, "ComputeLate", "q.tps." + getNumbers % 10, Guid.NewGuid().ToString());
            return new ApiResponse<StatusCode>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString()
            };
        }

        public async Task<ApiResponse<StatusCode>> ComputeUnderTime(DTOPayload request)
        {
            int getNumbers = Math.Abs(new Guid(request.Employee.Id.ToString()).GetHashCode()) % 10;
            _messageBroker.Publish(request, "ComputeUnderTime", "q.tps." + getNumbers % 10, Guid.NewGuid().ToString());
            return new ApiResponse<StatusCode>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString()
            };
        }
        public async Task<ApiResponse<StatusCode>> GetHolidayFromCalendar(DTOPayload request)
        {
            int getNumbers = Math.Abs(new Guid(request.Employee.Id.ToString()).GetHashCode()) % 10;
            _messageBroker.Publish(request, "GetHolidayFromCalendar", "q.tps." + getNumbers % 10, Guid.NewGuid().ToString());

            return new ApiResponse<StatusCode>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString()
            };
        }

        public async Task<ApiResponse<StatusCode>> CheckAbsent(DTOPayload request)
        {
            int getNumbers = Math.Abs(new Guid(request.Employee.Id.ToString()).GetHashCode()) % 10;
            _messageBroker.Publish(request, "CheckAbsent", "q.tps." + getNumbers % 10, Guid.NewGuid().ToString());
            return new ApiResponse<StatusCode>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString()
            };
        }

        #region Request for Approval
        public async Task<ApiResponse<StatusCode>> GetApprovedChangeShift(DTOPayload request)
        {
            int getNumbers = Math.Abs(new Guid(request.Employee.Id.ToString()).GetHashCode()) % 10;
            _messageBroker.Publish(request, "GetApprovedChangeShift", "q.tps." + getNumbers % 10, Guid.NewGuid().ToString());
            return new ApiResponse<StatusCode>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString()
            };
        }
        public async Task<ApiResponse<StatusCode>> GetApprovedOvertime(DTOPayload request)
        {
            int getNumbers = Math.Abs(new Guid(request.Employee.Id.ToString()).GetHashCode()) % 10;
            _messageBroker.Publish(request, "GetApprovedOvertime", "q.tps." + getNumbers % 10, Guid.NewGuid().ToString());
            return new ApiResponse<StatusCode>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString()
            };
        }
        public async Task<ApiResponse<StatusCode>> CheckLeave(DTOPayload request)
        {
            int getNumbers = Math.Abs(new Guid(request.Employee.Id.ToString()).GetHashCode()) % 10;
            _messageBroker.Publish(request, "CheckLeave", "q.tps." + getNumbers % 10, Guid.NewGuid().ToString());
            return new ApiResponse<StatusCode>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString()
            };
        }
        public async Task<ApiResponse<StatusCode>> GetApprovedDtr(DTOPayload request)
        {
            int getNumbers = Math.Abs(new Guid(request.Employee.Id.ToString()).GetHashCode()) % 10;
            _messageBroker.Publish(request, "GetApprovedDtr", "q.tps." + getNumbers % 10, Guid.NewGuid().ToString());
            return new ApiResponse<StatusCode>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString()
            };
        }
        #endregion


        //Overall Processing


        public async Task<ApiResponse<StatusCode>> GenerateTimesheet(string payrollCutoffId)
        {
            var employee = _dataEmployee.FilterBy(x => x.DateDeleted == null && x.DateResign == null).ToList();
            var cutoff = _dataPayrollCutoff.FindById(payrollCutoffId);
            if (cutoff == null)
            {
                return new ApiResponse<StatusCode>
                {
                    StatusCode = StatusCode.Success,
                    Message = StatusCode.Success.ToString()
                };
            }
            foreach (var emp in employee)
            {
                DTOPayload request = new DTOPayload();
                request.Employee = emp;
                request.RefPayrollCutoff = cutoff;
                await GenerateTimesheet(request);
            }

            return new ApiResponse<StatusCode>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString()
            };
        }

        public async Task<ApiResponse<StatusCode>> GenerateTimesheetOthers(string payrollCutoffId)
        {
            var employee = _dataEmployee.FilterBy(x => x.DateResign == null && x.DateDeleted == null).ToList();
            var cutoff = _dataPayrollCutoff.FindById(payrollCutoffId);
            if (cutoff == null)
            {
                return new ApiResponse<StatusCode>
                {
                    StatusCode = StatusCode.Success,
                    Message = StatusCode.Success.ToString()
                };
            }
            foreach (var emp in employee)
            {
                DTOPayload request = new DTOPayload();
                request.Employee = emp;
                request.RefPayrollCutoff = cutoff;

                await GetActualInOut(request);

                await GetApprovedChangeShift(request);

                await GetApprovedOvertime(request);

                await GetApprovedDtr(request);
                await CheckLeave(request);
                await GetHolidayFromCalendar(request);

                await ComputeApprovedDtrHourByShift(request);

                //4 Compute working hours by shift
                await ComputeWorkingHourByShift(request);

                ////5 Compute Nighshift Hours
                await ComputeNightShiftHours(request);

                ////6 Compute Late
                await ComputeLate(request);

                ////7 Compute Undertime
                await ComputeUnderTime(request);

                await CheckAbsent(request);
            }

            return new ApiResponse<StatusCode>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString()
            };
        }

        public async Task<ApiResponse<StatusCode>> GenerateTimesheetSingleEmployee(string employeeId, string payrollCutoffId)
        {
            Guid empId = new Guid(employeeId);
            var employee = _dataEmployee.FilterBy(x => x.Id == empId && x.DateDeleted == null && x.DateResign == null).FirstOrDefault();

            if (employee == null)
            {
                return new ApiResponse<StatusCode>
                {
                    StatusCode = StatusCode.Success,
                    Message = StatusCode.Success.ToString()
                };
            }
                

            var cutoff = _dataPayrollCutoff.FindById(payrollCutoffId);

            if (cutoff == null)
            {
                return new ApiResponse<StatusCode>
                {
                    StatusCode = StatusCode.Success,
                    Message = StatusCode.Success.ToString()
                };
            }

            DTOPayload request = new DTOPayload();
            request.Employee = employee;
            request.RefPayrollCutoff = cutoff;
            await GenerateTimesheet(request);

            return new ApiResponse<StatusCode>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString()
            };
        }

        public async Task<ApiResponse<StatusCode>> GenerateTimesheetOthersSingleEmployee(string employeeId, string payrollCutoffId)
        {
            Guid empId = new Guid(employeeId);
            var employee = _dataEmployee.FilterBy(x => x.Id == empId && x.DateDeleted == null && x.DateResign == null).FirstOrDefault();

            if (employee == null)
            {
                return new ApiResponse<StatusCode>
                {
                    StatusCode = StatusCode.Success,
                    Message = StatusCode.Success.ToString()
                };
            }
            var cutoff = _dataPayrollCutoff.FindById(payrollCutoffId);
            if (cutoff == null)
            {
                return new ApiResponse<StatusCode>
                {
                    StatusCode = StatusCode.Success,
                    Message = StatusCode.Success.ToString()
                };
            }

            DTOPayload request = new DTOPayload();
            request.Employee = employee;
            request.RefPayrollCutoff = cutoff;

            await GetActualInOut(request);

            await GetApprovedChangeShift(request);
            await GetApprovedOvertime(request);
            await GetApprovedDtr(request);
            await CheckLeave(request);
            await GetHolidayFromCalendar(request);

            //4 Compute working hours by shift
            await ComputeWorkingHourByShift(request);

            ////5 Compute Nighshift Hours
            await ComputeNightShiftHours(request);

            ////6 Compute Late
            await ComputeLate(request);

            ////7 Compute Undertime
            await ComputeUnderTime(request);

            await CheckAbsent(request);

            return new ApiResponse<StatusCode>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString()
            };
        }

        public async Task<ApiResponse<StatusCode>> UploadBiometricsFile(string filename)
        {
            DTOBiometricUpload payload = new DTOBiometricUpload();
            payload.Filename = filename;

            _messageBroker.Publish(payload, "BiometricsUpload", "q.tps.0", Guid.NewGuid().ToString());


            return new ApiResponse<StatusCode>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString()
            };
        }
    }

}
