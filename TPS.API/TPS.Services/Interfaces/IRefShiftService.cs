using System.Collections.Generic;
using System.Threading.Tasks;
using TPS.Infrastructure;
using TPS.Infrastructure.Enums;
using TPS.Infrastructure.Models;

namespace TPS.Services.Interfaces
{
    public interface IRefShiftService
    {
        ApiResponse<IEnumerable<RefShift>> GetAll();
        Task<ApiResponse<RefShift>> FindById(string id);
        Task<ApiResponse<StatusCode>> Create(RefShift entity);
        Task<ApiResponse<StatusCode>> Update(RefShift entity);
        Task<ApiResponse<StatusCode>> Delete(string id);
    }
}
