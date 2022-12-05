using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TPS.Frontend.Infrastructure;
using TPS.Frontend.Infrastructure.DTO;
using TPS.Frontend.Infrastructure.Pagination;

namespace TPS.Frontend.Services.Interface
{
    public interface IRequestLeaveService
    {
        Task<ApiResponse<StatusCode>> AddOrEditRequestLeaveAsync(CancellationToken cancellationToken, string accessToken, RequestLeave model);
        Task<ApiResponse<StatusCode>> DeleteRequestLeaveAsync(CancellationToken cancellationToken, string accessToken, string id);
        Task<ApiResponse<RequestLeave>> FindRequestLeaveAsync(CancellationToken cancellationToken, string accessToken, string id);
        Task<ApiResponse<IEnumerable<RequestLeave>>> GetAllRequestsLeaveAsync(CancellationToken cancellationToken, string accessToken);
        Task<ApiResponse<IEnumerable<RequestLeave>>> GetUserRequestsLeaveAsync(CancellationToken cancellationToken, string accessToken, string userId);
        Task<ApiResponse<IEnumerable<RequestLeave>>> GetLeaveApprovalsAsync(CancellationToken cancellationToken, string accessToken, string userId);
        Task<ApiResponse<StatusCode>> ProcessApprovalAsync(CancellationToken cancellationToken, string accessToken, DTOApproval model);
        Task<ApiResponse<IEnumerable<RequestLeave>>> GetAllByApproverIdCalendar(CancellationToken cancellationToken, string accessToken, string userId);
        Task<PaginatedReturn<RequestLeave>> GetPaginatedRequestLeaveAsync(CancellationToken cancellationToken, string accessToken, PaginatedRequest param);
    }
}