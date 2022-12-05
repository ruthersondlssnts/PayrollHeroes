using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TPS.Frontend.Infrastructure;

namespace TPS.Frontend.Services.Interface
{
    public interface IRequestChangeShiftService
    {
        Task<ApiResponse<StatusCode>> AddOrEditRequestChangeShiftAsync(CancellationToken cancellationToken, string accessToken, RequestChangeShift model);
        Task<ApiResponse<StatusCode>> DeleteRequestChangeShiftAsync(CancellationToken cancellationToken, string accessToken, string id);
        Task<ApiResponse<RequestChangeShift>> FindRequestChangeShiftAsync(CancellationToken cancellationToken, string accessToken, string id);
        Task<ApiResponse<IEnumerable<RequestChangeShift>>> GetAllRequestsChangeShiftAsync(CancellationToken cancellationToken, string accessToken);
        Task<ApiResponse<IEnumerable<RequestChangeShift>>> GetUserRequestsChangeShiftAsync(CancellationToken cancellationToken, string accessToken, string userId);
        Task<ApiResponse<IEnumerable<RequestChangeShift>>> GetChangeShiftApprovalsAsync(CancellationToken token, string accessToken, string userId);

    }
}