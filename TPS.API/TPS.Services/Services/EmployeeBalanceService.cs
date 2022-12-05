using System.Collections.Generic;
using System.Threading.Tasks;
using TPS.Infrastructure;
using TPS.Infrastructure.Enums;
using TPS.Infrastructure.Models;
using TPS.Services.Interfaces;

namespace TPS.Services.Services
{
    public class EmployeeBalanceService : IEmployeeBalanceService
    {
        private readonly IDBService<EmployeeBalance> _data;
        public EmployeeBalanceService(IDBService<EmployeeBalance> data)
        {
            _data = data;
        }

        public async Task<ApiResponse<StatusCode>> Create(EmployeeBalance entity)
        {
            //Check Duplicate
            var validateDup = _data.FindOne(x => x.EmployeeId == entity.EmployeeId && x.LeaveTypeId == entity.LeaveTypeId && x.DateDeleted == null);
            if (validateDup != null)
            {
                return new ApiResponse<StatusCode>
                {
                    StatusCode = StatusCode.DUPLICATE,
                    Message = "Duplicate Employee Balance"
                };
            }

            await _data.InsertOneAsync(entity);
            return new ApiResponse<StatusCode>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString()
            };
        }

        public async Task<ApiResponse<StatusCode>> Delete(string id)
        {
            await _data.DeleteOneAsync(id);
            return new ApiResponse<StatusCode>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString()
            };
        }

        public async Task<ApiResponse<EmployeeBalance>> FindById(string id)
        {
            return new ApiResponse<EmployeeBalance>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString(),
                Result = _data.FindById(id)
            };
        }
        public async Task<ApiResponse<EmployeeBalance>> FindByEmployeeIdLeaveTypeId(string empId, string leaveTypeId)
        {
            return new ApiResponse<EmployeeBalance>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString(),
                Result = _data.FindOne(x => x.EmployeeId == empId && x.LeaveTypeId == leaveTypeId && x.DateDeleted == null)
            };
        }
        public ApiResponse<IEnumerable<EmployeeBalance>> GetAll()
        {
            return new ApiResponse<IEnumerable<EmployeeBalance>>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString(),
                Result = _data.FilterBy(x => x.DateDeleted == null)
            };
        }

        public async Task<ApiResponse<StatusCode>> Update(EmployeeBalance entity)
        {
            //Check Duplicate
            var validateDup = _data.FindOne(x => x.EmployeeId == entity.EmployeeId && x.LeaveTypeId == entity.LeaveTypeId && x.DateDeleted == null && x.Id != entity.Id);
            if (validateDup != null)
            {
                return new ApiResponse<StatusCode>
                {
                    StatusCode = StatusCode.DUPLICATE,
                    Message = "Duplicate Employee Balance"
                };
            }

            await _data.ReplaceOneAsync(entity);
            return new ApiResponse<StatusCode>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString()
            };
        }
    }
}
