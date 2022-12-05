using System.Collections.Generic;
using System.Threading.Tasks;
using TPS.Infrastructure;
using TPS.Infrastructure.Enums;
using TPS.Infrastructure.Models;

namespace TPS.Services.Interfaces
{
    public interface IRefGroupService
    {
        ApiResponse<IEnumerable<RefGroup>> GetAll();
        ApiResponse<IEnumerable<RefGroup>> GetDescendants(string pathToNode);
        Task<ApiResponse<RefGroup>> FindById(string id);
        Task<ApiResponse<StatusCode>> Create(RefGroup entity);
        Task<ApiResponse<StatusCode>> Update(RefGroup entity);
        Task<ApiResponse<StatusCode>> Delete(string id);
        Task<ApiResponse<List<GroupApprover>>> GetApprovers(string groupId, ApproverType type);
    }
}
