using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TPS.Frontend.Infrastructure;

namespace TPS.Frontend.Services.Interface
{
    public interface IEmployeeBalanceService
    {
        Task<ApiResponse<StatusCode>> AddEditEmployeeBalanceAsync(CancellationToken cancellationToken, string accessToken, EmployeeBalance model);
        Task<ApiResponse<StatusCode>> DeleteEmployeeBalanceAsync(CancellationToken cancellationToken, string accessToken, string id);
        Task<ApiResponse<EmployeeBalance>> FindEmployeeBalanceAsync(CancellationToken cancellationToken, string accessToken, string id);
        Task<ApiResponse<IEnumerable<EmployeeBalance>>> GetEmployeeBalancesAsync(CancellationToken cancellationToken, string accessToken);
        Task<ApiResponse<EmployeeBalance>> FindEmployeeLeaveTypeBalanceAsync(CancellationToken token, string accessToken, string empId, string leaveId);
    }

}