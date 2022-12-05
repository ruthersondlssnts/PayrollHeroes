using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TPS.Frontend.Infrastructure;

namespace TPS.Frontend.Services.Interface
{
    public interface IRefLeaveTypeService
    {
        Task<ApiResponse<StatusCode>> AddOrEditLeaveTypeAsync(CancellationToken cancellationToken, string accessToken, RefLeaveType model);
        Task<ApiResponse<StatusCode>> DeleteLeaveTypeAsync(CancellationToken cancellationToken, string accessToken, string id);
        Task<ApiResponse<RefLeaveType>> FindLeaveTypeAsync(CancellationToken cancellationToken, string accessToken, string id);
        Task<ApiResponse<IEnumerable<RefLeaveType>>> GetLeaveTypesAsync(CancellationToken cancellationToken, string accessToken);
    }
}