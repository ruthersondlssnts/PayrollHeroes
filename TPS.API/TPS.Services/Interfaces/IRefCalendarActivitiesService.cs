using System.Collections.Generic;
using System.Threading.Tasks;
using TPS.Infrastructure;
using TPS.Infrastructure.Enums;
using TPS.Infrastructure.Models;

namespace TPS.Services.Interfaces
{
    public interface IRefCalendarActivitiesService
    {
        ApiResponse<IEnumerable<RefCalendarActivities>> GetAll();
        Task<ApiResponse<RefCalendarActivities>> FindById(string id);
        Task<ApiResponse<StatusCode>> Create(RefCalendarActivities entity);
        Task<ApiResponse<StatusCode>> Update(RefCalendarActivities entity);
        Task<ApiResponse<StatusCode>> Delete(string id);
    }
}
