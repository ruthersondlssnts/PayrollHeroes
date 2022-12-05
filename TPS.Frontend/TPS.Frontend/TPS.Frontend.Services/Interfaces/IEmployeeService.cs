using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TPS.Frontend.Infrastructure;
using TPS.Frontend.Infrastructure.DTO;
using TPS.Frontend.Infrastructure.Pagination;

namespace TPS.Frontend.Services.Interface
{
    public interface IEmployeeService
    {
        Task<PaginatedReturn<Employee>> GetEmployeesAsync(CancellationToken cancellationToken, string accessToken, PaginatedEmployees param);
        Task<ApiResponse<StatusCode>> AddOrEditEmployeeAsync(CancellationToken cancellationToken, string accessToken, Employee model);
        Task<ApiResponse<StatusCode>> DeleteEmployeeAsync(CancellationToken cancellationToken, string accessToken, string id);

        Task<ApiResponse<Employee>> FindEmployeeAsync(CancellationToken cancellationToken, string accessToken, string id);
        Task<ApiResponse<IEnumerable<DTOEmployee>>> GetFullNamesAsync(CancellationToken token, string accessToken);
    }
}