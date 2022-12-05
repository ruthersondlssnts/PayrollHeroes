using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TPS.Frontend.Infrastructure;

namespace TPS.Frontend.Services.Interface
{
    public interface IRefPayrollCutOffService
    {
        Task<ApiResponse<StatusCode>> AddOrEditPayrollCutoffAsync(CancellationToken token, string accessToken, RefPayrollCutoff model);
        Task<ApiResponse<StatusCode>> DeletePayrollCutoffAsync(CancellationToken token, string accessToken, string id);
        Task<ApiResponse<RefPayrollCutoff>> FindPayrollCutoffAsync(CancellationToken cancellationToken, string accessToken, string id);
        Task<ApiResponse<IEnumerable<RefPayrollCutoff>>> GetPayrollCutoffsAsync(CancellationToken cancellationToken, string accessToken);
    }
}