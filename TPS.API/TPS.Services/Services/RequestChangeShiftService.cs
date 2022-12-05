using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TPS.Infrastructure;
using TPS.Infrastructure.Enums;
using TPS.Infrastructure.Models;
using TPS.Services.Interfaces;

namespace TPS.Services.Services
{
    public class RequestChangeShiftService : IRequestChangeShiftService
    {
        private readonly IDBService<RequestChangeShift> _data;
        private readonly IRefGroupService _refGroup;
        private readonly IDBService<Employee> _dataEmployee;
        public RequestChangeShiftService(IDBService<RequestChangeShift> data, IRefGroupService refGroup, IDBService<Employee> dataEmployee)
        {
            _data = data;
            _refGroup = refGroup;
            _dataEmployee = dataEmployee;
        }

        public async Task<ApiResponse<StatusCode>> Create(RequestChangeShift entity)
        {
            //Validate empty group
            if (string.IsNullOrEmpty(entity.GroupId) || string.IsNullOrEmpty(entity.GroupName))
            {
                return new ApiResponse<StatusCode>
                {
                    StatusCode = StatusCode.Conflict,
                    Message = "No Group/Department defined"
                };
            }

            //Validate empty Shift
            if (string.IsNullOrEmpty(entity.ShiftIn) || string.IsNullOrEmpty(entity.ShiftOut))
            {
                return new ApiResponse<StatusCode>
                {
                    StatusCode = StatusCode.Conflict,
                    Message = "No Shiftin/Shiftout defined"
                };
            }


            //Check Duplicate
            var validateDup = _data.FindOne(x => x.EmployeeId == entity.EmployeeId && x.ShiftDate == entity.ShiftDate && x.DateDeleted == null);
            if (validateDup != null)
            {
                return new ApiResponse<StatusCode>
                {
                    StatusCode = StatusCode.DUPLICATE,
                    Message = "Duplicate Date Shift"
                };
            }

            //validate overlap
            DateTime shiftStart = DateTime.Parse(entity.ShiftDate.ToShortDateString() + " " + entity.ShiftIn);
            DateTime shiftEnd = DateTime.Parse(entity.IsNightShift ? entity.ShiftDate.AddDays(1).ToShortDateString() : entity.ShiftDate.ToShortDateString() + " " + entity.ShiftOut);

            if (shiftStart > shiftEnd)
            {
                return new ApiResponse<StatusCode>
                {
                    StatusCode = StatusCode.Conflict,
                    Message = "Shift start is greater than Shift End"
                };

            }

            //getApprovers
            var approvers = _refGroup.GetApprovers(entity.GroupId, ApproverType.DTR).Result;
            entity.Approvers = new List<Infrastructure.Shared.Approvers>();
            foreach (var approver in approvers.Result)
            {
                Infrastructure.Shared.Approvers newRec = new Infrastructure.Shared.Approvers();
                newRec.ApprovalStatusId = (int)ApprovalStatus.FOR_APPROVAL;
                newRec.ApprovalStatusName = ApprovalStatus.FOR_APPROVAL.ToString();
                newRec.ApproverUserId = approver.ApproverUserId;
                newRec.ApproverName = approver.ApproverName;
                entity.Approvers.Add(newRec);
            }

            //getFirst record
            entity.ApproverUserId = entity.Approvers.FirstOrDefault().ApproverUserId;
            entity.ApproverName = entity.Approvers.FirstOrDefault().ApproverName;
            entity.ApprovalStatusId = (int)ApprovalStatus.FOR_APPROVAL;
            entity.ApprovalStatusName = ApprovalStatus.FOR_APPROVAL.ToString();

            Guid empId = new Guid(entity.EmployeeId);
            var employeeGroupId = _dataEmployee.FindOne(x => x.Id == empId);
            Guid groupId = new Guid(employeeGroupId.GroupId);
            entity.GroupId = employeeGroupId.GroupId;
            entity.GroupName = employeeGroupId.GroupName;

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

        public async Task<ApiResponse<RequestChangeShift>> FindById(string id)
        {
            return new ApiResponse<RequestChangeShift>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString(),
                Result = _data.FindById(id)
            };
        }

        public ApiResponse<IEnumerable<RequestChangeShift>> GetAll()
        {
            return new ApiResponse<IEnumerable<RequestChangeShift>>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString(),
                Result = _data.FilterBy(x => x.DateDeleted == null)
            };
        }

        public ApiResponse<IEnumerable<RequestChangeShift>> GetAllByUserId(string userId)
        {
            return new ApiResponse<IEnumerable<RequestChangeShift>>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString(),
                Result = _data.FilterBy(x => x.DateDeleted == null && x.EmployeeId == userId)
            };
        }

        public async Task<ApiResponse<StatusCode>> Update(RequestChangeShift entity)
        {
            await _data.ReplaceOneAsync(entity);
            return new ApiResponse<StatusCode>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString()
            };
        }
    }
}
