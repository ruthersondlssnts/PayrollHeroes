using System.Collections.Generic;
using System.Threading.Tasks;
using TPS.Infrastructure;
using TPS.Infrastructure.Enums;
using TPS.Infrastructure.Models;

namespace TPS.Services.Interfaces
{
    public interface IBiometricsService
    {
        ApiResponse<IEnumerable<BiometricsData>> GetAll();
        Task<ApiResponse<BiometricsData>> FindById(string id);
        Task<ApiResponse<StatusCode>> Create(BiometricsData entity);
        Task<ApiResponse<StatusCode>> Update(BiometricsData entity);
        Task<ApiResponse<StatusCode>> Delete(string id);
    }
}
