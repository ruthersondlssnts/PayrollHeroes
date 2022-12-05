using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TPS.Frontend.Infrastructure;
using TPS.Frontend.Infrastructure.DTO;
using TPS.Frontend.Infrastructure.Pagination;

namespace TPS.Frontend.Services.Interface
{
    public interface IRequestDTRService
    {
        Task<ApiResponse<StatusCode>> AddOrEditRequestDTRAsync(CancellationToken cancellationToken, string accessToken, RequestDtr model);
        Task<ApiResponse<StatusCode>> DeleteRequestDTRAsync(CancellationToken cancellationToken, string accessToken, string id);
        Task<ApiResponse<RequestDtr>> FindRequestDTRAsync(CancellationToken cancellationToken, string accessToken, string id);
        Task<ApiResponse<IEnumerable<RequestDtr>>> GetAllRequestsDTRAsync(CancellationToken cancellationToken, string accessToken);
        Task<ApiResponse<IEnumerable<RequestDtr>>> GetUserRequestsDTRAsync(CancellationToken cancellationToken, string accessToken, string userId);
        Task<ApiResponse<IEnumerable<RequestDtr>>> GetDTRApprovalsAsync(CancellationToken token, string accessToken, string userId);
        Task<ApiResponse<StatusCode>> ProcessApprovalAsync(CancellationToken cancellationToken, string accessToken, DTOApproval model);
        Task<PaginatedReturn<RequestDtr>> GetPaginatedRequestDTRAsync(CancellationToken cancellationToken, string accessToken, PaginatedRequest param);
    }
}