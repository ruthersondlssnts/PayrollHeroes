using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TPS.Infrastructure;
using TPS.Infrastructure.DTO;

namespace TPS.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<ApiResponse<List<DTODashboardForApproval>>> ForApprovals(string userId);
    }
}
