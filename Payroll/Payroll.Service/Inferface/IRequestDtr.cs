using Payroll.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;


namespace Payroll.Service.Inferface
{
    public interface IRequestDtr
    {
        ////
        IEnumerable<RequestDTREntity> GetList(int employee_id);
        IEnumerable<RequestDTREntity> GetForApproval(int employee_id);

        bool IsExist(int employee_id, DateTime shiftdate);
        bool IsApproved(int employee_id, DateTime shiftdate, decimal leave);
        bool Delete(int request_leave_id);


        bool Approve(long request_leave_id, string approver_remark, int approver_id);
        bool Disapprove(long request_leave_id, string approver_remark, int approver_id);
        bool CreateOrUpdate(RequestDTREntity obj);

        RequestDTREntity GetByID(int id);
    }
}
