using System.Collections.Generic;
using System.Threading.Tasks;
using TPS.Infrastructure;
using TPS.Infrastructure.Enums;
using TPS.Infrastructure.Models;
using TPS.Infrastructure.Pagination;

namespace TPS.Services.Interfaces
{
    public interface IRequestDtrService
    {
        ApiResponse<IEnumerable<RequestDtr>> GetAll();
        ApiResponse<IEnumerable<RequestDtr>> GetAllByUserId(string userId);
        Task<ApiResponse<RequestDtr>> FindById(string id);
        Task<ApiResponse<StatusCode>> Create(RequestDtr entity);
        Task<ApiResponse<StatusCode>> Update(RequestDtr entity);
        Task<ApiResponse<StatusCode>> Delete(string id);
        Task<ApiResponse<StatusCode>> GetListForApproval(string groupId);
        Task<ApiResponse<StatusCode>> ProcessApproval(string id, ApprovalStatus status);

        ApiResponse<IEnumerable<RequestDtr>> GetAllByApproverId(string userId);

        Task<ApiResponse<StatusCode>> ProcessApproval(string id, string approverUserId, string remarks, ApprovalStatus approvalStatus);

        PaginatedReturn<RequestDtr> GetPaginatedList(PaginatedRequest param);
    }
}
