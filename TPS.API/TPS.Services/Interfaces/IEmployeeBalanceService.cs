using System.Collections.Generic;
using System.Threading.Tasks;
using TPS.Infrastructure;
using TPS.Infrastructure.Enums;
using TPS.Infrastructure.Models;

namespace TPS.Services.Interfaces
{
    public interface IEmployeeBalanceService
    {
        ApiResponse<IEnumerable<EmployeeBalance>> GetAll();
        Task<ApiResponse<EmployeeBalance>> FindById(string id);
        Task<ApiResponse<EmployeeBalance>> FindByEmployeeIdLeaveTypeId(string employeeId, string leaveTypeId);
        Task<ApiResponse<StatusCode>> Create(EmployeeBalance entity);
        Task<ApiResponse<StatusCode>> Update(EmployeeBalance entity);
        Task<ApiResponse<StatusCode>> Delete(string id);
    }
}
