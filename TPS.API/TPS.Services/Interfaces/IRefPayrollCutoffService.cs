using System.Collections.Generic;
using System.Threading.Tasks;
using TPS.Infrastructure;
using TPS.Infrastructure.Enums;
using TPS.Infrastructure.Models;

namespace TPS.Services.Interfaces
{
    public interface IRefPayrollCutoffService
    {
        ApiResponse<IEnumerable<RefPayrollCutoff>> GetAll();
        Task<ApiResponse<RefPayrollCutoff>> FindById(string id);
        Task<ApiResponse<StatusCode>> Create(RefPayrollCutoff entity);
        Task<ApiResponse<StatusCode>> Update(RefPayrollCutoff entity);
        Task<ApiResponse<StatusCode>> Delete(string id);
    }
}
