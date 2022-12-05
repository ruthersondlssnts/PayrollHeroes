using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TPS.Frontend.Infrastructure;

namespace TPS.Frontend.Services.Interface
{
    public interface IRefShiftService
    {
        Task<ApiResponse<StatusCode>> AddOrEditShiftAsync(CancellationToken token, string accessToken, RefShift model);
        Task<ApiResponse<StatusCode>> DeleteShiftAsync(CancellationToken token, string accessToken, string id);
        Task<ApiResponse<RefShift>> FindShiftAsync(CancellationToken cancellationToken, string accessToken, string id);
        Task<ApiResponse<IEnumerable<RefShift>>> GetShiftsAsync(CancellationToken cancellationToken, string accessToken);
    }

}