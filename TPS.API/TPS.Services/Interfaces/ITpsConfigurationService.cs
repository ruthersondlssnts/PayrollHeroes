using System.Collections.Generic;
using System.Threading.Tasks;
using TPS.Infrastructure;
using TPS.Infrastructure.Enums;
using TPS.Infrastructure.Models;

namespace TPS.Services.Interfaces
{
    public interface ITpsConfigurationService
    {
        ApiResponse<IEnumerable<TpsConfiguration>> GetAll();
        Task<ApiResponse<TpsConfiguration>> FindById(string id);
        Task<ApiResponse<StatusCode>> Create(TpsConfiguration entity);
        Task<ApiResponse<StatusCode>> Update(TpsConfiguration entity);
        Task<ApiResponse<StatusCode>> Delete(string id);
    }

}
