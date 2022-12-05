using Payroll.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll.Service.Interface
{
    public interface IRequestOvertime
    {

        IEnumerable<RequestOvertimeEntity> GetList(int employee_id);
        IEnumerable<RequestOvertimeEntity> GetForApproval(int employee_id);

        bool IsExist(int employee_id, DateTime shiftdate);
 
        bool Delete(int request_leave_id);
        
        
        bool Approve(long request_leave_id, string approver_remark, int approver_id);
        bool Disapprove(long request_leave_id, string approver_remark, int approver_id);
        bool CreateOrUpdate(RequestOvertimeEntity obj);

        RequestOvertimeEntity GetByID(int id);

    }
}
