using System.Collections.Generic;
using System.Threading.Tasks;
using TPS.Infrastructure;
using TPS.Infrastructure.Enums;
using TPS.Infrastructure.Models;

namespace TPS.Services.Interfaces
{
    public interface IRefSSSService
    {
        Task<ApiResponse<StatusCode>> Create(RefSSS entity);
        Task<ApiResponse<StatusCode>> Delete(string id);
        Task<ApiResponse<RefSSS>> FindById(string id);
        ApiResponse<IEnumerable<RefSSS>> GetAll();
        Task<ApiResponse<StatusCode>> Update(RefSSS entity);
    }
}
