using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TPS.Frontend.Infrastructure;

namespace TPS.Frontend.Services.Interface
{
    public interface IRefPhilHealthService
    {
        Task<ApiResponse<StatusCode>> AddOrEditPhilHealthAsync(CancellationToken cancellationToken, string accessToken, RefPhilHealth model);
        Task<ApiResponse<StatusCode>> DeletePhilHealthAsync(CancellationToken cancellationToken, string accessToken, string id);
        Task<ApiResponse<RefPhilHealth>> FindPhilHealthAsync(CancellationToken cancellationToken, string accessToken, string id);
        Task<ApiResponse<IEnumerable<RefPhilHealth>>> GetAllPhilHealthAsync(CancellationToken cancellationToken, string accessToken);
    }
}