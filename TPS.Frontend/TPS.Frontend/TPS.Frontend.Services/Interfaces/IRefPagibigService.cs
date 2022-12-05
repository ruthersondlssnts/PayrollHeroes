using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TPS.Frontend.Infrastructure;

namespace TPS.Frontend.Services.Interface
{
    public interface IRefPagibigService
    {
        Task<ApiResponse<StatusCode>> AddOrEditPagibigAsync(CancellationToken cancellationToken, string accessToken, RefPagibig model);
        Task<ApiResponse<StatusCode>> DeletePagibigAsync(CancellationToken cancellationToken, string accessToken, string id);
        Task<ApiResponse<RefPagibig>> FindPagibigAsync(CancellationToken cancellationToken, string accessToken, string id);
        Task<ApiResponse<IEnumerable<RefPagibig>>> GetAllPagibigAsync(CancellationToken cancellationToken, string accessToken);
    }
}