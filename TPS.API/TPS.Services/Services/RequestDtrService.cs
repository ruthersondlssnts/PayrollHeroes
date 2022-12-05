using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TPS.Infrastructure;
using TPS.Infrastructure.Enums;
using TPS.Infrastructure.Models;
using TPS.Services.Interfaces;
using System.Linq.Dynamic.Core;
using TPS.Infrastructure.Pagination;

namespace TPS.Services.Services
{
    public class RequestDtrService : IRequestDtrService
    {
        private readonly IDBService<RequestDtr> _data;
        //private readonly IRefGroupService _refGroup;
        private readonly IDBService<RefGroup> _dataRefGroup;
        private readonly IDBService<Employee> _dataEmployee;
        private readonly IDBService<UserNotification> _dataUserNotification;
        public RequestDtrService(IDBService<RequestDtr> data, IDBService<Employee> dataEmployee, IDBService<RefGroup> dataRefGroup, IDBService<UserNotification> dataUserNotification)
        {
            _data = data;
            _dataEmployee = dataEmployee;
            _dataRefGroup = dataRefGroup;
            _dataUserNotification = dataUserNotification;
        }

        public async Task<ApiResponse<StatusCode>> Create(RequestDtr entity)
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

            ////getApprovers
            //var approvers = _refGroup.GetApprovers(entity.GroupId, ApproverType.DTR).Result;
            //entity.Approvers = new List<Infrastructure.Shared.Approvers>();
            //foreach (var approver in approvers.Result)
            //{
            //    Infrastructure.Shared.Approvers newRec = new Infrastructure.Shared.Approvers();
            //    newRec.ApprovalStatusId = (int)ApprovalStatus.FOR_APPROVAL;
            //    newRec.ApprovalStatusName = ApprovalStatus.FOR_APPROVAL.ToString();
            //    newRec.ApproverUserId = approver.ApproverUserId;
            //    newRec.ApproverName = approver.ApproverName;
            //    entity.Approvers.Add(newRec);
            //}

            ////getFirst record
            //entity.ApproverUserId = entity.Approvers.FirstOrDefault().ApproverUserId;
            //entity.ApproverName = entity.Approvers.FirstOrDefault().ApproverName;
            //entity.ApprovalStatusId = (int)ApprovalStatus.FOR_APPROVAL;
            //entity.ApprovalStatusName = ApprovalStatus.FOR_APPROVAL.ToString();

            //Guid empId = new Guid(entity.EmployeeId);
            //var employeeGroupId = _dataEmployee.FindOne(x => x.Id == empId);
            //Guid groupId = new Guid(employeeGroupId.GroupId);
            //entity.GroupId = employeeGroupId.GroupId;
            //entity.GroupName = employeeGroupId.GroupName;

            entity.ApprovalStatusId = (int)ApprovalStatus.FOR_APPROVAL;
            entity.ApprovalStatusName = ApprovalStatus.FOR_APPROVAL.ToString();

            //Get approvers
            Guid empId = new Guid(entity.EmployeeId);
            var employeeGroupId = _dataEmployee.FindOne(x => x.Id == empId);
            Guid groupId = new Guid(employeeGroupId.GroupId);
            var approvers = _dataRefGroup.FindOne(x => x.Id == groupId);

            if (approvers == null || approvers.DTRApprover.Count == 0)
            {
                return new ApiResponse<StatusCode>
                {
                    StatusCode = StatusCode.Conflict,
                    Message = "No approver defined for your department"
                };
            }

            entity.LastName = employeeGroupId.LastName;
            entity.FirstName = employeeGroupId.FirstName;
            entity.ApproverUserId = approvers.LeaveApprover.FirstOrDefault().ApproverUserId;
            entity.ApproverName = approvers.LeaveApprover.FirstOrDefault().ApproverName;

            await _data.InsertOneAsync(entity);

            await _dataUserNotification.InsertOneAsync(new UserNotification
            {
                EmployeeId = entity.ApproverUserId,
                FullName = entity.ApproverName,
                Status = entity.ApprovalStatusName,
                IsRead = false,
                DateTime = DateTime.Now,
                Type = NotificationType.DTRRequest
            });

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

        public async Task<ApiResponse<RequestDtr>> FindById(string id)
        {
            return new ApiResponse<RequestDtr>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString(),
                Result = _data.FindById(id)
            };
        }

        public ApiResponse<IEnumerable<RequestDtr>> GetAll()
        {
            return new ApiResponse<IEnumerable<RequestDtr>>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString(),
                Result = _data.FilterBy(x => x.DateDeleted == null)
            };
        }



        public async Task<ApiResponse<StatusCode>> Update(RequestDtr entity)
        {
            //Check Duplicate
            var validateDup = _data.FindOne(x => x.Id != entity.Id && x.ShiftDate == entity.ShiftDate && x.DateDeleted == null);
            if (validateDup != null)
            {
                return new ApiResponse<StatusCode>
                {
                    StatusCode = StatusCode.DUPLICATE,
                    Message = "Duplicate Date Shift"
                };
            }
            //Validate if not for approva
            var checkCurrentStatus = _data.FindOne(x => x.Id == entity.Id);
            if (checkCurrentStatus.ApprovalStatusId != 1)
            {
                return new ApiResponse<StatusCode>
                {
                    StatusCode = StatusCode.Conflict,
                    Message = "Only filed request with status FOR_APPROVAL can be edited"
                };
            }


            await _data.ReplaceOneAsync(entity);
            return new ApiResponse<StatusCode>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString()
            };
        }

        public Task<ApiResponse<StatusCode>> GetListForApproval(string groupId)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResponse<StatusCode>> ProcessApproval(string id, ApprovalStatus status)
        {
            throw new System.NotImplementedException();
        }

        public ApiResponse<IEnumerable<RequestDtr>> GetAllByUserId(string userId)
        {
            return new ApiResponse<IEnumerable<RequestDtr>>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString(),
                Result = _data.FilterBy(x => x.DateDeleted == null && x.EmployeeId == userId)
            };
        }
        public ApiResponse<IEnumerable<RequestDtr>> GetAllByApproverId(string userId)
        {
            return new ApiResponse<IEnumerable<RequestDtr>>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString(),
                Result = _data.FilterBy(x => x.DateDeleted == null && x.ApprovalStatusName == ApprovalStatus.FOR_APPROVAL.ToString() && x.ApproverUserId == userId)
            };
        }

        public async Task<ApiResponse<StatusCode>> ProcessApproval(string id, string approverUserId, string remarks, ApprovalStatus approvalStatus)
        {
            var entity = _data.FindById(id);

            if (entity.ApproverUserId != approverUserId)
            {
                return new ApiResponse<StatusCode>
                {
                    StatusCode = StatusCode.Conflict,
                    Message = "Invalid approver"
                };
            }

            if (entity.ApprovalStatusName != ApprovalStatus.FOR_APPROVAL.ToString())
            {
                return new ApiResponse<StatusCode>
                {
                    StatusCode = StatusCode.Conflict,
                    Message = "For approval status mismatch"
                };
            }

            entity.ApprovalStatusId = (int)approvalStatus;
            entity.ApprovalStatusName = approvalStatus.ToString();
            entity.ApproverRemarks = remarks;
            entity.ApproveDate = DateTime.Now;

            await _data.ReplaceOneAsync(entity);

            await _dataUserNotification.InsertOneAsync(new UserNotification
            {
                EmployeeId = entity.EmployeeId,
                FullName = entity.FirstName + " " + entity.LastName,
                IsRead = false,
                DateTime = DateTime.Now,
                Status = entity.ApprovalStatusName,
                Type = NotificationType.DTRApproval
            });

            return new ApiResponse<StatusCode>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString()
            };
        }
        public PaginatedReturn<RequestDtr> GetPaginatedList(PaginatedRequest param)
        {
            var returnObject = new PaginatedReturn<RequestDtr>();

            // default sort column and type
            returnObject.OrderByColumn = param.OrderByColumn ?? "Remarks";
            returnObject.OrderByASCOrDESC = param.OrderByASCOrDESC ?? "ASC";
            returnObject.CurrentPage = param.PageNumber;

            var data = _data.FilterBy(x => x.DateDeleted == null && x.EmployeeId == param.EmployeeId).ToList();

            // filter by search keyword
            if (!String.IsNullOrWhiteSpace(param.SearchKeyword))
            {
                var filterdata = data.Where(a => a.Remarks.ToUpper().Contains(param.SearchKeyword.ToUpper())).ToList();

                if (filterdata.Count() > 0)
                {
                    data = filterdata;
                }
                else
                {
                    return returnObject;
                }
            }

            returnObject.TotalRecord = data.Count();
            returnObject.TotalPage = (int)Math.Ceiling((decimal)returnObject.TotalRecord / (decimal)param.PageSize);
            data = data.AsQueryable().OrderBy(returnObject.OrderByColumn + " " + returnObject.OrderByASCOrDESC.ToUpper())
                .Skip((param.PageNumber - 1) * param.PageSize)
                .Take(param.PageSize)
                .ToList();

            returnObject.DataList = data;
            returnObject.StatusCode = StatusCode.Success;
            returnObject.Message = StatusCode.Success.ToString();

            return returnObject;
        }
    }
}
