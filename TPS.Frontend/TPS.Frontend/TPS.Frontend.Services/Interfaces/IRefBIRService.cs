using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TPS.Frontend.Infrastructure;

namespace TPS.Frontend.Services.Interface
{
    public interface IRefBIRService
    {
        Task<ApiResponse<StatusCode>> AddOrEditBIRAsync(CancellationToken cancellationToken, string accessToken, RefBIR model);
        Task<ApiResponse<StatusCode>> DeleteBIRAsync(CancellationToken cancellationToken, string accessToken, string id);
        Task<ApiResponse<RefBIR>> FindBIRAsync(CancellationToken cancellationToken, string accessToken, string id);
        Task<ApiResponse<IEnumerable<RefBIR>>> GetAllBIRAsync(CancellationToken cancellationToken, string accessToken);
    }
}