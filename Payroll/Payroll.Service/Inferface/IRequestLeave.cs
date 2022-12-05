using Payroll.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll.Service.Interface
{
    public interface IRequestLeave
    {
        IEnumerable<RequestLeaveEntity> GetApprovedLeave(int employee_id, DateTime shiftdate);
        IEnumerable<RequestLeaveEntity> GetList(int employee_id);
        IEnumerable<RequestLeaveEntity> GetForApproval(int employee_id);

        bool IsExist(int employee_id, DateTime shiftdate);
        bool IsApproved(int employee_id, DateTime shiftdate, decimal leave);
        bool Delete(int request_leave_id);
        
        
        bool Approve(long request_leave_id, string approver_remark, int approver_id);
        bool Disapprove(long request_leave_id, string approver_remark, int approver_id);
        bool CreateOrUpdate(RequestLeaveEntity obj);

        RequestLeaveEntity GetByID(int id);

    }
}
