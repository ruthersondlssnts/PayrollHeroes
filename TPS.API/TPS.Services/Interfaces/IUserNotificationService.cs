using System.Collections.Generic;
using System.Threading.Tasks;
using TPS.Infrastructure;
using TPS.Infrastructure.Enums;
using TPS.Infrastructure.Models;

namespace TPS.Services.Interfaces
{
    public interface IUserNotificationService
    {
        Task<ApiResponse<StatusCode>> Create(UserNotification entity);
        Task<ApiResponse<StatusCode>> Delete(string id);
        Task<ApiResponse<StatusCode>> DeleteByType(NotificationType notificationType);
        Task<ApiResponse<UserNotification>> FindById(string id);
        ApiResponse<IEnumerable<UserNotification>> GetAll();
        Task<ApiResponse<StatusCode>> Update(UserNotification entity);
        ApiResponse<IEnumerable<UserNotification>> GetByUserId(string userId);
        ApiResponse<IEnumerable<UserNotification>> GetUnreadByUserId(string userId);
        Task<ApiResponse<StatusCode>> UpdateAllToRead();
    }

}
