using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPS.Infrastructure;
using TPS.Infrastructure.DTO;
using TPS.Infrastructure.Enums;
using TPS.Services.Interfaces;

namespace TPS.Services.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IRequestLeaveService _dataLeave;
        private readonly IRequestDtrService _dataDTR;
        private readonly IRequestOvertimeService _dataOvertime;
        private readonly IEmployeeService _dataEmployee;
        public DashboardService(IEmployeeService dataEmployee, IRequestLeaveService dataLeave, IRequestDtrService dataDTR, IRequestOvertimeService dataOvertime)
        {
            _dataLeave = dataLeave;
            _dataDTR = dataDTR;
            _dataOvertime = dataOvertime;
            _dataEmployee = dataEmployee;
        }

        public async Task<ApiResponse<List<DTODashboardForApproval>>> ForApprovals(string userId)
        {
            List<DTODashboardForApproval> returnData = new List<DTODashboardForApproval>();

            returnData.Add(new DTODashboardForApproval
            {
                Type = "DTR",
                Count = _dataDTR.GetAllByApproverId(userId).Result.Count()
            });

            returnData.Add(new DTODashboardForApproval
            {
                Type = "LEAVE",
                Count = _dataLeave.GetAllByApproverId(userId).Result.Count()
            });

            returnData.Add(new DTODashboardForApproval
            {
                Type = "OVERTIME",
                Count = _dataOvertime.GetAllByApproverId(userId).Result.Count()
            });

            returnData.Add(new DTODashboardForApproval
            {
                Type = "EMPLOYEE",
                Count = _dataEmployee.GetAll().Result.Count()
            });

            return new ApiResponse<List<DTODashboardForApproval>>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString(),
                Result = returnData
            };
        }



    }
}
