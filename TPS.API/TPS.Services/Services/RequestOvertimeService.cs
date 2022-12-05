using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TPS.Infrastructure;
using TPS.Infrastructure.Enums;
using TPS.Infrastructure.Models;
using TPS.Infrastructure.Pagination;
using TPS.Services.Interfaces;
using System.Linq.Dynamic.Core;

namespace TPS.Services.Services
{
    public class RequestOvertimeService : IRequestOvertimeService
    {
        private readonly IDBService<RequestOvertime> _data;
        private readonly IDBService<RefGroup> _dataRefGroup;
        private readonly IDBService<Employee> _dataEmployee;
        private readonly IDBService<UserNotification> _dataUserNotification;
        public RequestOvertimeService(IDBService<RefGroup> dataRefGroup, IDBService<Employee> dataEmployee, IDBService<RequestOvertime> data, IDBService<UserNotification> dataUserNotification)
        {
            _data = data;
            _dataEmployee = dataEmployee;
            _dataRefGroup = dataRefGroup;
            _dataUserNotification = dataUserNotification;
        }

        public async Task<ApiResponse<StatusCode>> Create(RequestOvertime entity)
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
            //Check Duplicate
            var validateDup = _data.FindOne(x => x.EmployeeId == entity.EmployeeId && x.OvertimeDate == entity.OvertimeDate && x.DateDeleted == null);
            if (validateDup != null)
            {
                return new ApiResponse<StatusCode>
                {
                    StatusCode = StatusCode.DUPLICATE,
                    Message = "Duplicate Date Overtime"
                };
            }


            entity.ApprovalStatusId = (int)ApprovalStatus.FOR_APPROVAL;
            entity.ApprovalStatusName = ApprovalStatus.FOR_APPROVAL.ToString();

            //Get approvers
            Guid empId = new Guid(entity.EmployeeId);
            var employeeGroupId = _dataEmployee.FindOne(x => x.Id == empId);
            Guid groupId = new Guid(employeeGroupId.GroupId);
            var approvers = _dataRefGroup.FindOne(x => x.Id == groupId);
            //Validate empty apporovers
            if (approvers == null || approvers.OvertimeApprover.Count == 0)
            {
                return new ApiResponse<StatusCode>
                {
                    StatusCode = StatusCode.Conflict,
                    Message = "No approver defined for your department"
                };
            }

            entity.LastName = employeeGroupId.LastName;
            entity.FirstName = employeeGroupId.FirstName;
            entity.ApproverUserId = approvers.OvertimeApprover.FirstOrDefault().ApproverUserId;
            entity.ApproverName = approvers.OvertimeApprover.FirstOrDefault().ApproverName;

            //TODO multiple approver

            entity.GroupId = employeeGroupId.GroupId;
            entity.GroupName = employeeGroupId.GroupName;

            //TODO able to bank overtime to be used for Leave

            entity.OvertimeTypeId = (int)OvertimeType.WITHPAY;
            entity.OvertimeTypeName = OvertimeType.WITHPAY.ToString();

            await _data.InsertOneAsync(entity);

            await _dataUserNotification.InsertOneAsync(new UserNotification
            {
                EmployeeId = entity.ApproverUserId,
                FullName = entity.ApproverName,
                Status = entity.ApprovalStatusName,
                IsRead = false,
                DateTime = DateTime.Now,
                Type = NotificationType.OvertimeRequest
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

        public async Task<ApiResponse<RequestOvertime>> FindById(string id)
        {
            return new ApiResponse<RequestOvertime>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString(),
                Result = _data.FindById(id)
            };
        }

        public ApiResponse<IEnumerable<RequestOvertime>> GetAll()
        {
            return new ApiResponse<IEnumerable<RequestOvertime>>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString(),
                Result = _data.FilterBy(x => x.DateDeleted == null)
            };
        }
        public ApiResponse<IEnumerable<RequestOvertime>> GetAllByUserId(string userId)
        {
            return new ApiResponse<IEnumerable<RequestOvertime>>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString(),
                Result = _data.FilterBy(x => x.DateDeleted == null && x.EmployeeId == userId)
            };
        }
        public async Task<ApiResponse<StatusCode>> Update(RequestOvertime entity)
        {
            //Check Duplicate
            var validateDup = _data.FindOne(x => x.Id != entity.Id && x.OvertimeDate == entity.OvertimeDate && x.DateDeleted == null);
            if (validateDup != null)
            {
                return new ApiResponse<StatusCode>
                {
                    StatusCode = StatusCode.DUPLICATE,
                    Message = "Duplicate Date Overtime"
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

        public ApiResponse<IEnumerable<RequestOvertime>> GetAllByApproverId(string userId)
        {
            return new ApiResponse<IEnumerable<RequestOvertime>>
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
                Type = NotificationType.OvertimeApproval
            });

            return new ApiResponse<StatusCode>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString()
            };
        }

        public PaginatedReturn<RequestOvertime> GetPaginatedList(PaginatedRequest param)
        {
            var returnObject = new PaginatedReturn<RequestOvertime>();

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
