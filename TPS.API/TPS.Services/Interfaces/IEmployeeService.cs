using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TPS.Infrastructure;
using TPS.Infrastructure.DTO;
using TPS.Infrastructure.Enums;
using TPS.Infrastructure.Models;
using TPS.Infrastructure.Pagination;

namespace TPS.Services.Interfaces
{
    public interface IEmployeeService
    {
        ApiResponse<IEnumerable<Employee>> GetAll();
        PaginatedReturn<Employee> GetEmployees(PaginatedEmployees param);
        Task<ApiResponse<Employee>> FindById(string id);
        Task<ApiResponse<StatusCode>> Create(Employee entity);
        Task<ApiResponse<StatusCode>> Update(Employee entity);
        Task<ApiResponse<StatusCode>> Delete(string id);
        ApiResponse<IEnumerable<DTOEmployee>> GetFullNames();

    }
}
