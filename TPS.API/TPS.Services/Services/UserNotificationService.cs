using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TPS.Infrastructure;
using TPS.Infrastructure.Enums;
using TPS.Infrastructure.Models;
using TPS.Services.Interfaces;

namespace TPS.Services.Services
{

    public class UserNotificationService : IUserNotificationService
    {
        private readonly IDBService<UserNotification> _data;
        public UserNotificationService(IDBService<UserNotification> data)
        {
            _data = data;
        }

        public async Task<ApiResponse<StatusCode>> Create(UserNotification entity)
        {
            await _data.InsertOneAsync(entity);
            return new ApiResponse<StatusCode>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString()
            };
        }

        public async Task<ApiResponse<StatusCode>> Delete(string id)
        {
            await _data.DeleteOneAsync(id);
            return new ApiResponse<StatusCode>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString()
            };
        }
        public async Task<ApiResponse<StatusCode>> DeleteByType(NotificationType type)
        {
            await _data.DeleteManyAsync(x => x.Type == type);
            return new ApiResponse<StatusCode>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString()
            };
        }
        public async Task<ApiResponse<UserNotification>> FindById(string id)
        {
            return new ApiResponse<UserNotification>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString(),
                Result = _data.FindById(id)
            };
        }

        public ApiResponse<IEnumerable<UserNotification>> GetAll()
        {
            return new ApiResponse<IEnumerable<UserNotification>>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString(),
                Result = _data.FilterBy(x => x.DateDeleted == null)
            };
        }

        public async Task<ApiResponse<StatusCode>> Update(UserNotification entity)
        {
            await _data.ReplaceOneAsync(entity);
            return new ApiResponse<StatusCode>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString()
            };
        }
        public async Task<ApiResponse<StatusCode>> UpdateAllToRead()
        {
            await _data.UpdateManyAsync(x => x.IsRead == false, x => x.IsRead, true);
            return new ApiResponse<StatusCode>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString()
            };
        }

        public ApiResponse<IEnumerable<UserNotification>> GetByUserId(string userId)
        {
            return new ApiResponse<IEnumerable<UserNotification>>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString(),
                Result = _data.FilterBy(x => x.EmployeeId == userId)
            };
        }
        public ApiResponse<IEnumerable<UserNotification>> GetUnreadByUserId(string userId)
        {
            return new ApiResponse<IEnumerable<UserNotification>>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString(),
                Result = _data.FilterBy(x => x.EmployeeId == userId && x.IsRead == false)
            };
        }
    }
}
