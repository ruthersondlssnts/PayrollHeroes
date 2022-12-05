using System.Collections.Generic;
using System.Threading.Tasks;
using TPS.Infrastructure;
using TPS.Infrastructure.Enums;
using TPS.Infrastructure.Models;

namespace TPS.Services.Interfaces
{
    public interface IRefBIRService
    {
        Task<ApiResponse<StatusCode>> Create(RefBIR entity);
        Task<ApiResponse<StatusCode>> Delete(string id);
        Task<ApiResponse<RefBIR>> FindById(string id);
        ApiResponse<IEnumerable<RefBIR>> GetAll();
        Task<ApiResponse<StatusCode>> Update(RefBIR entity);
    }
}
