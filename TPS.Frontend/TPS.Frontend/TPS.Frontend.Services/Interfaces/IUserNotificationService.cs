using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TPS.Frontend.Infrastructure;

namespace TPS.Frontend.Services.Interface
{
    public interface IUserNotificationService
    {
        Task<ApiResponse<StatusCode>> AddOrEditUserNotificationAsync(CancellationToken cancellationToken, string accessToken, UserNotification model);
        Task<ApiResponse<StatusCode>> DeleteUserNotificationAsync(CancellationToken cancellationToken, string accessToken, string id);
        Task<ApiResponse<IEnumerable<UserNotification>>> GetUnreadUserNotificationsAsync(CancellationToken cancellationToken, string accessToken, string id);
        Task<ApiResponse<UserNotification>> FindUserNotificationAsync(CancellationToken token, string accessToken, string id);
        Task<ApiResponse<StatusCode>> DeleteByTypeAsync(CancellationToken token, string accessToken, int type);
        Task<ApiResponse<StatusCode>> UpdateAllToReadAsync(CancellationToken token, string accessToken);
        Task<ApiResponse<IEnumerable<UserNotification>>> GetAllUserNotificationsAsync(CancellationToken token, string accessToken, string userId);
    }
}
