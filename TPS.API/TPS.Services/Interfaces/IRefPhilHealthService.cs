using System.Collections.Generic;
using System.Threading.Tasks;
using TPS.Infrastructure;
using TPS.Infrastructure.Enums;
using TPS.Infrastructure.Models;

namespace TPS.Services.Interfaces
{
    public interface IRefPhilHealthService
    {
        Task<ApiResponse<StatusCode>> Create(RefPhilHealth entity);
        Task<ApiResponse<StatusCode>> Delete(string id);
        Task<ApiResponse<RefPhilHealth>> FindById(string id);
        ApiResponse<IEnumerable<RefPhilHealth>> GetAll();
        Task<ApiResponse<StatusCode>> Update(RefPhilHealth entity);
    }
}
