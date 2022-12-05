using System.Collections.Generic;
using System.Threading.Tasks;
using TPS.Infrastructure;
using TPS.Infrastructure.Enums;
using TPS.Infrastructure.Models;

namespace TPS.Services.Interfaces
{
    public interface IRefLeaveTypeService
    {
        ApiResponse<IEnumerable<RefLeaveType>> GetAll();
        Task<ApiResponse<RefLeaveType>> FindById(string id);
        Task<ApiResponse<StatusCode>> Create(RefLeaveType entity);
        Task<ApiResponse<StatusCode>> Update(RefLeaveType entity);
        Task<ApiResponse<StatusCode>> Delete(string id);
    }
}
