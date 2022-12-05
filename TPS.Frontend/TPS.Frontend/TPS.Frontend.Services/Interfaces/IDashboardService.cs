using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TPS.Frontend.Infrastructure;
using TPS.Frontend.Infrastructure.DTO;

namespace TPS.Frontend.Services.Interface
{
    public interface IDashboardService
    {
        Task<ApiResponse<IEnumerable<DTODashboardForApproval>>> ForApprovalsAsync(CancellationToken cancellationToken, string accessToken, string userId);
    }

}