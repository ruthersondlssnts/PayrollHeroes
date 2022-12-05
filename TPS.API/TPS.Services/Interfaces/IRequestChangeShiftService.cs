using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TPS.Infrastructure;
using TPS.Infrastructure.Enums;
using TPS.Infrastructure.Models;
using TPS.Infrastructure.Pagination;

namespace TPS.Services.Interfaces
{
    public interface IRequestChangeShiftService
    {
        ApiResponse<IEnumerable<RequestChangeShift>> GetAll();
        ApiResponse<IEnumerable<RequestChangeShift>> GetAllByUserId(string userId);
        Task<ApiResponse<RequestChangeShift>> FindById(string id);
        Task<ApiResponse<StatusCode>> Create(RequestChangeShift entity);
        Task<ApiResponse<StatusCode>> Update(RequestChangeShift entity);
        Task<ApiResponse<StatusCode>> Delete(string id);

    }
}
