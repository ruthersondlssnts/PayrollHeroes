using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TPS.Frontend.Infrastructure;
using static TPS.Frontend.Infrastructure.Enums;

namespace TPS.Frontend.Services.Interface
{
    public interface IRefGroupService
    {
        Task<ApiResponse<StatusCode>> AddOrEditGroupAsync(CancellationToken cancellationToken, string accessToken, RefGroup model);
        Task<ApiResponse<StatusCode>> DeleteGroupAsync(CancellationToken cancellationToken, string accessToken, string id);
        Task<ApiResponse<RefGroup>> FindGroupAsync(CancellationToken cancellationToken, string accessToken, string id);
        Task<ApiResponse<IEnumerable<RefGroup>>> GetGroupDescendantsAsync(CancellationToken cancellationToken, string accessToken, string pathToNode);
        Task<ApiResponse<List<GroupApprover>>> GetGroupApproversAsync(CancellationToken cancellationToken, string accessToken, string groupId, string approverType);
        Task<ApiResponse<IEnumerable<RefGroup>>> GetGroupsAsync(CancellationToken cancellationToken, string accessToken);
    }

}