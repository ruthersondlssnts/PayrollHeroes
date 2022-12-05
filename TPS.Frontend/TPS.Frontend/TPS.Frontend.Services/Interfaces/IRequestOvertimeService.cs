using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TPS.Frontend.Infrastructure;
using TPS.Frontend.Infrastructure.DTO;
using TPS.Frontend.Infrastructure.Pagination;

namespace TPS.Frontend.Services.Interface
{
    public interface IRequestOvertimeService
    {
        Task<ApiResponse<StatusCode>> AddOrEditRequestOvertimeAsync(CancellationToken cancellationToken, string accessToken, RequestOvertime model);
        Task<ApiResponse<StatusCode>> DeleteRequestOvertimeAsync(CancellationToken cancellationToken, string accessToken, string id);
        Task<ApiResponse<RequestOvertime>> FindRequestOvertimeAsync(CancellationToken cancellationToken, string accessToken, string id);
        Task<ApiResponse<IEnumerable<RequestOvertime>>> GetAllRequestsOvertimeAsync(CancellationToken cancellationToken, string accessToken);
        Task<ApiResponse<IEnumerable<RequestOvertime>>> GetUserRequestsOvertimeAsync(CancellationToken cancellationToken, string accessToken, string userId);
        Task<ApiResponse<IEnumerable<RequestOvertime>>> GetOvertimeApprovalsAsync(CancellationToken token, string accessToken, string userId);
        Task<ApiResponse<StatusCode>> ProcessApprovalAsync(CancellationToken cancellationToken, string accessToken, DTOApproval model);
        Task<PaginatedReturn<RequestOvertime>> GetPaginatedRequestOvertimeAsync(CancellationToken cancellationToken, string accessToken, PaginatedRequest param);

    }
}