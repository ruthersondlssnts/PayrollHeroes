using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TPS.Frontend.Infrastructure;

namespace TPS.Frontend.Services.Interface
{
    public interface IRefCalendarActivityService
    {
        Task<ApiResponse<StatusCode>> AddEditCalendarActivityAsync(CancellationToken cancellationToken, string accessToken, RefCalendarActivity model);
        Task<ApiResponse<StatusCode>> DeleteCalendarActivityAsync(CancellationToken cancellationToken, string accessToken, string id);
        Task<ApiResponse<RefCalendarActivity>> FindCalendarActivityAsync(CancellationToken cancellationToken, string accessToken, string id);
        Task<ApiResponse<IEnumerable<RefCalendarActivity>>> GetCalendarActivitiesAsync(CancellationToken cancellationToken, string accessToken);
    }

}