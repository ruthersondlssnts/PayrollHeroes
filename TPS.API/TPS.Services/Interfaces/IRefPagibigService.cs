using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TPS.Infrastructure;
using TPS.Infrastructure.Enums;
using TPS.Infrastructure.Models;

namespace TPS.Services.Interfaces
{
    public interface IRefPagibigService
    {
        ApiResponse<IEnumerable<RefPagibig>> GetAll();
        Task<ApiResponse<RefPagibig>> FindById(string id);
        Task<ApiResponse<StatusCode>> Create(RefPagibig entity);
        Task<ApiResponse<StatusCode>> Update(RefPagibig entity);
        Task<ApiResponse<StatusCode>> Delete(string id);
    }
}
