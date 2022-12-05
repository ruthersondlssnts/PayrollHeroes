using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TPS.Infrastructure;
using TPS.Infrastructure.DTO;
using TPS.Infrastructure.Enums;
using TPS.Infrastructure.Models;

namespace TPS.Services.Interfaces
{
    public interface IEmployeeTimesheetService
    {
        ApiResponse<IEnumerable<EmployeeTimesheet>> GetAll();
        Task<ApiResponse<EmployeeTimesheet>> FindById(string id);
        Task<ApiResponse<StatusCode>> Create(EmployeeTimesheet entity);
        Task<ApiResponse<StatusCode>> Update(EmployeeTimesheet entity);
        Task<ApiResponse<StatusCode>> Delete(string id);

        ApiResponse<IEnumerable<EmployeeTimesheet>> FindByCutoffEmployeeId(string cutoff, string employeeId);


        Task<ApiResponse<StatusCode>> GenerateTimesheet(DTOPayload request);
        Task<ApiResponse<StatusCode>> GetActualInOut(DTOPayload request);
        Task<ApiResponse<StatusCode>> ComputeWorkingHourByShift(DTOPayload request);
        Task<ApiResponse<StatusCode>> GetApprovedDtr(DTOPayload request);
        Task<ApiResponse<StatusCode>> GetApprovedChangeShift(DTOPayload request);
        Task<ApiResponse<StatusCode>> ComputeApprovedDtrHourByShift(DTOPayload request);
        Task<ApiResponse<StatusCode>> GetApprovedOvertime(DTOPayload request);
        Task<ApiResponse<StatusCode>> ComputeApprovedOvertimeHr(DTOPayload request);
        Task<ApiResponse<StatusCode>> ComputeNightShiftHours(DTOPayload request);
        Task<ApiResponse<StatusCode>> CheckLeave(DTOPayload request);
        Task<ApiResponse<StatusCode>> ComputeLate(DTOPayload request);
        Task<ApiResponse<StatusCode>> ComputeUnderTime(DTOPayload request);
        Task<ApiResponse<StatusCode>> GetHolidayFromCalendar(DTOPayload request);
        Task<ApiResponse<StatusCode>> CheckAbsent(DTOPayload request);
        ApiResponse<IEnumerable<EmployeeTimesheet>> FindByEmployeeId(string employeeId);


        //Mass Update
        Task<ApiResponse<StatusCode>> GenerateTimesheetSingleEmployee(string employeeId,string payrollCutoffId);

        Task<ApiResponse<StatusCode>> GenerateTimesheetOthersSingleEmployee(string employeeId, string payrollCutoffId);


        Task<ApiResponse<StatusCode>> GenerateTimesheet(string payrollCutoffId);

        Task<ApiResponse<StatusCode>> GenerateTimesheetOthers(string payrollCutoffId);
        ApiResponse<IEnumerable<DTODtrCalendar>> FindDtrEmployeeByDate(string employeeId, DateTime start, DateTime end);
        Task<ApiResponse<StatusCode>> UploadBiometricsFile(string filename);

    }
}
