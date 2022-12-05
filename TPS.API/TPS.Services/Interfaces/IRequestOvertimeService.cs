using System.Collections.Generic;
using System.Threading.Tasks;
using TPS.Infrastructure;
using TPS.Infrastructure.Enums;
using TPS.Infrastructure.Models;
using TPS.Infrastructure.Pagination;

namespace TPS.Services.Interfaces
{
    public interface IRequestOvertimeService
    {
        ApiResponse<IEnumerable<RequestOvertime>> GetAll();
        ApiResponse<IEnumerable<RequestOvertime>> GetAllByUserId(string userId);
        Task<ApiResponse<RequestOvertime>> FindById(string id);
        Task<ApiResponse<StatusCode>> Create(RequestOvertime entity);
        Task<ApiResponse<StatusCode>> Update(RequestOvertime entity);
        Task<ApiResponse<StatusCode>> Delete(string id);
        ApiResponse<IEnumerable<RequestOvertime>> GetAllByApproverId(string userId);

        Task<ApiResponse<StatusCode>> ProcessApproval(string id, string approverUserId, string remarks, ApprovalStatus approvalStatus);

        PaginatedReturn<RequestOvertime> GetPaginatedList(PaginatedRequest param);
    }
}
