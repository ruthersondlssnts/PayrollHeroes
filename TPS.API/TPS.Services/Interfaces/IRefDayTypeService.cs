using System.Collections.Generic;
using System.Threading.Tasks;
using TPS.Infrastructure;
using TPS.Infrastructure.Enums;
using TPS.Infrastructure.Models;

namespace TPS.Services.Interfaces
{
    public interface IRefDayTypeService
    {
        ApiResponse<IEnumerable<RefDayType>> GetAll();
        Task<ApiResponse<RefDayType>> FindById(string id);
        Task<ApiResponse<StatusCode>> Create(RefDayType entity);
        Task<ApiResponse<StatusCode>> Update(RefDayType entity);
        Task<ApiResponse<StatusCode>> Delete(string id);
    }
}
