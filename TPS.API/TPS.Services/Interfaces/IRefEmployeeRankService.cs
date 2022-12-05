using System.Collections.Generic;
using System.Threading.Tasks;
using TPS.Infrastructure;
using TPS.Infrastructure.Enums;
using TPS.Infrastructure.Models;

namespace TPS.Services.Interfaces
{
    public interface IRefEmployeeRankService
    {
        ApiResponse<IEnumerable<RefEmployeeRank>> GetAll();
        Task<ApiResponse<RefEmployeeRank>> FindById(string id);
        Task<ApiResponse<StatusCode>> Create(RefEmployeeRank entity);
        Task<ApiResponse<StatusCode>> Update(RefEmployeeRank entity);
        Task<ApiResponse<StatusCode>> Delete(string id);
    }
}
