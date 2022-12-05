using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TPS.Frontend.Infrastructure;

namespace TPS.Frontend.Services.Interface
{
    public interface IRefSSSService
    {
        Task<ApiResponse<StatusCode>> AddOrEditSSSAsync(CancellationToken cancellationToken, string accessToken, RefSSS model);
        Task<ApiResponse<StatusCode>> DeleteSSSAsync(CancellationToken cancellationToken, string accessToken, string id);
        Task<ApiResponse<RefSSS>> FindSSSAsync(CancellationToken cancellationToken, string accessToken, string id);
        Task<ApiResponse<IEnumerable<RefSSS>>> GetAllSSSAsync(CancellationToken cancellationToken, string accessToken);
    }
}