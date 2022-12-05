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
    public class RequestLeaveService : IRequestLeaveService
    {
        private readonly IDBService<RequestLeave> _data;
        private readonly IDBService<RefGroup> _dataRefGroup;
        private readonly IDBService<Employee> _dataEmployee;
        private readonly IDBService<UserNotification> _dataUserNotification;
        private readonly IEmployeeBalanceService _balanceService;
        public RequestLeaveService(IDBService<RequestLeave> data, IDBService<RefGroup> dataRefGroup, IDBService<Employee> dataEmployee, IEmployeeBalanceService service, IDBService<UserNotification> dataUserNotification)
        {
            _data = data;
            _dataRefGroup = dataRefGroup;
            _balanceService = service;
            _dataEmployee = dataEmployee;
            _dataUserNotification = dataUserNotification;
        }

        public async Task<ApiResponse<StatusCode>> Create(RequestLeave entity)
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
            var validateDup = _data.FindOne(x => x.EmployeeId == entity.EmployeeId && x.DateStart <= entity.DateEnd && entity.DateStart <= x.DateEnd && x.DateDeleted == null);
            if (validateDup != null)
            {
                return new ApiResponse<StatusCode>
                {
                    StatusCode = StatusCode.DUPLICATE,
                    Message = "Overlapping date entered"
                };
            }

            entity.ApprovalStatusId = (int)ApprovalStatus.FOR_APPROVAL;
            entity.ApprovalStatusName = ApprovalStatus.FOR_APPROVAL.ToString();

            //Get approvers
            Guid empId = new Guid(entity.EmployeeId);
            var employeeGroupId = _dataEmployee.FindOne(x => x.Id == empId);
            Guid groupId = new Guid(employeeGroupId.GroupId);
            var approvers = _dataRefGroup.FindOne(x => x.Id == groupId);

            if (approvers == null || approvers.LeaveApprover.Count == 0)
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

            //TODO multiple approver

            //check balance and update balance
            var empBalanceResponse = await _balanceService.FindByEmployeeIdLeaveTypeId(entity.EmployeeId, entity.LeaveTypeId);
            if (empBalanceResponse.StatusCode == StatusCode.Success && empBalanceResponse.Result != null)
            {
                if ((empBalanceResponse.Result.Quantity - entity.LeaveDay) < 0)
                {
                    return new ApiResponse<StatusCode>
                    {
                        StatusCode = StatusCode.Conflict,
                        Message = "Insufficient leave balance"
                    };
                }
                else
                {
                    var balance = empBalanceResponse.Result;
                    balance.Quantity -= entity.LeaveDay;
                    await _balanceService.Update(balance);
                }
            }
            else
            {
                return new ApiResponse<StatusCode>
                {
                    StatusCode = StatusCode.Conflict,
                    Message = "Employee no leave balance"
                };
            }

            entity.GroupId = employeeGroupId.GroupId;
            entity.GroupName = employeeGroupId.GroupName;

            await _data.InsertOneAsync(entity);

            await _dataUserNotification.InsertOneAsync(new UserNotification
            {
                EmployeeId = entity.ApproverUserId,
                FullName = entity.ApproverName,
                Status = entity.ApprovalStatusName,
                IsRead = false,
                DateTime = DateTime.Now,
                Type = NotificationType.LeaveRequest
            });

            return new ApiResponse<StatusCode>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString()
            };

        }

        public async Task<ApiResponse<StatusCode>> Delete(string id)
        {
            //check balance and update balance
            var requestLeave = _data.FindById(id);

            var empBalanceResponse = await _balanceService.FindByEmployeeIdLeaveTypeId(requestLeave.EmployeeId, requestLeave.LeaveTypeId);

            //return leave balance from deleted leave request 
            if (empBalanceResponse.StatusCode == StatusCode.Success && empBalanceResponse.Result != null)
            {
                empBalanceResponse.Result.Quantity += requestLeave.LeaveDay;
                await _balanceService.Update(empBalanceResponse.Result);
            }

            await _data.DeleteOneAsync(id);
            return new ApiResponse<StatusCode>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString()
            };
        }

        public async Task<ApiResponse<RequestLeave>> FindById(string id)
        {
            return new ApiResponse<RequestLeave>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString(),
                Result = _data.FindById(id)
            };
        }

        public ApiResponse<IEnumerable<RequestLeave>> GetAll()
        {
            return new ApiResponse<IEnumerable<RequestLeave>>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString(),
                Result = _data.FilterBy(x => x.DateDeleted == null)
            };
        }

        public ApiResponse<IEnumerable<RequestLeave>> GetAllByUserId(string userId)
        {
            return new ApiResponse<IEnumerable<RequestLeave>>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString(),
                Result = _data.FilterBy(x => x.DateDeleted == null && x.EmployeeId == userId)
            };
        }

        public ApiResponse<IEnumerable<RequestLeave>> GetAllByApproverId(string userId)
        {
            return new ApiResponse<IEnumerable<RequestLeave>>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString(),
                Result = _data.FilterBy(x => x.DateDeleted == null && x.ApprovalStatusName == ApprovalStatus.FOR_APPROVAL.ToString() && x.ApproverUserId == userId)
            };
        }

        public ApiResponse<IEnumerable<RequestLeave>> GetAllByApproverIdCalendar(string userId)
        {
            return new ApiResponse<IEnumerable<RequestLeave>>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString(),
                Result = _data.FilterBy(x => x.DateDeleted == null && (x.ApprovalStatusName == ApprovalStatus.FOR_APPROVAL.ToString() || x.ApprovalStatusName == ApprovalStatus.APPROVED.ToString()) && x.ApproverUserId == userId)
            };
        }

        public async Task<ApiResponse<StatusCode>> Update(RequestLeave entity)
        {

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

            //Check Duplicate
            var validateDup = _data.FindOne(x => x.EmployeeId == entity.EmployeeId && x.DateStart <= entity.DateEnd && entity.DateStart <= x.DateEnd && x.DateDeleted == null && x.Id != entity.Id);
            if (validateDup != null)
            {
                return new ApiResponse<StatusCode>
                {
                    StatusCode = StatusCode.DUPLICATE,
                    Message = "Overlapping date entered"
                };
            }

            //check balance and update balance
            var empBalanceResponse = await _balanceService.FindByEmployeeIdLeaveTypeId(entity.EmployeeId, entity.LeaveTypeId);

            if (empBalanceResponse.StatusCode == StatusCode.Success && empBalanceResponse.Result != null)
            {
                if (entity.LeaveDay < entity.PrevLeaveDay) //if no. of leave days decrease, update employee balance
                {
                    var decreasedVal = entity.PrevLeaveDay - entity.LeaveDay;
                    var balance = empBalanceResponse.Result;
                    balance.Quantity += decreasedVal.GetValueOrDefault();
                    await _balanceService.Update(balance);
                }
                else if (entity.LeaveDay > entity.PrevLeaveDay)//if no. of leave days increase, update employee balance
                {
                    var increasedVal = entity.LeaveDay - entity.PrevLeaveDay;
                    if ((empBalanceResponse.Result.Quantity - increasedVal) < 0)
                    {
                        return new ApiResponse<StatusCode>
                        {
                            StatusCode = StatusCode.Conflict,
                            Message = "Insufficient Leave Balance"
                        };
                    }
                    else
                    {
                        var balance = empBalanceResponse.Result;
                        balance.Quantity -= increasedVal.GetValueOrDefault();
                        await _balanceService.Update(balance);
                    }
                }
            }
            else
            {
                return new ApiResponse<StatusCode>
                {
                    StatusCode = StatusCode.Conflict,
                    Message = "Employee no leave balance"
                };
            }

            await _data.ReplaceOneAsync(entity);
            return new ApiResponse<StatusCode>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString()
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
                Type = NotificationType.LeaveApproval
            });
            return new ApiResponse<StatusCode>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString()
            };
        }

        public PaginatedReturn<RequestLeave> GetPaginatedList(PaginatedRequest param)
        {
            var returnObject = new PaginatedReturn<RequestLeave>();

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
