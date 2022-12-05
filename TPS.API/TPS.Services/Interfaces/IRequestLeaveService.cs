using System.Collections.Generic;
using System.Threading.Tasks;
using TPS.Infrastructure;
using TPS.Infrastructure.Enums;
using TPS.Infrastructure.Models;
using TPS.Infrastructure.Pagination;

namespace TPS.Services.Interfaces
{
    public interface IRequestLeaveService
    {
        ApiResponse<IEnumerable<RequestLeave>> GetAll();
        ApiResponse<IEnumerable<RequestLeave>> GetAllByUserId(string userId);
        Task<ApiResponse<RequestLeave>> FindById(string id);
        Task<ApiResponse<StatusCode>> Create(RequestLeave entity);
        Task<ApiResponse<StatusCode>> Update(RequestLeave entity);
        Task<ApiResponse<StatusCode>> Delete(string id);

        ApiResponse<IEnumerable<RequestLeave>> GetAllByApproverId(string userId);

        Task<ApiResponse<StatusCode>> ProcessApproval(string id, string approverUserId, string remarks, ApprovalStatus approvalStatus);

        ApiResponse<IEnumerable<RequestLeave>> GetAllByApproverIdCalendar(string userId);

        PaginatedReturn<RequestLeave> GetPaginatedList(PaginatedRequest param);
    }
}
