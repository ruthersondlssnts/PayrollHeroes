using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TPS.Frontend.Infrastructure;
using TPS.Frontend.Infrastructure.DTO;

namespace TPS.Frontend.Services.Interface
{
    public interface IEmployeeTimesheetService
    {
        Task<ApiResponse<List<EmployeeTimesheet>>> GetTimesheetByEmployeeId(CancellationToken cancellationToken, string accessToken, string cutoffId, string employeeId);

        Task GenerateTimesheet(CancellationToken cancellationToken, string accessToken, DTOPayrollCutoffRequest model);
        Task<ApiResponse<List<DTODtrCalendar>>> FindDtrEmployeeByDate(CancellationToken cancellationToken, string accessToken, string employeeId, DateTime start, DateTime end);
        Task GenerateTimesheetOthers(CancellationToken cancellationToken, string accessToken, DTOPayrollCutoffRequest model);

        Task UploadBiometricsFile(CancellationToken cancellationToken, string accessToken, string filename);
    }
}
