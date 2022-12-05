using Payroll.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll.Core.Interface
{
    public interface IRequestDTR
    {


        bool IsApproved(int employee_id, DateTime shiftdate,out string time_in,out string time_out, out int ref_shift_id);

        RequestDTREntity GetApprovedDTR(int employee_id, DateTime shiftdate, RequestDTREntity data);

        bool UpdateDTR(int employee_id, DateTime shiftdate, string time_in, string time_out);

        bool CreateorUpdate(RequestDTREntity obj);


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
