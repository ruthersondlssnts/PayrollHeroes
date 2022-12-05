using Payroll.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll.Core.Interface
{
    public interface IRequestOvertime
    {
        //ACTIONS
        bool Create(DateTime dtOt, int employee_id, int ref_department_id, string time_in, string time_out, string reason);



        bool Approve(DateTime dtOt, int employee_id, string approver_remark, int approver_id);

        bool Disapprove(DateTime dtOt, int employee_id, string approver_remark, int approver_id);

        //VERIFY

        bool IsApproved(int employee_id, DateTime dtOt, out string time_in, out string time_out);

        bool BatchApprove(IEnumerable<RequestOvertimeEntity> list);

        bool BatchDisapprove(IEnumerable<RequestOvertimeEntity> list);

        //GET DATA
        IEnumerable<RequestOvertimeEntity> GetApproved(int employee_id);

        IEnumerable<RequestOvertimeEntity> GetAll(int employee_id);

        IEnumerable<RequestOvertimeEntity> GetAllForApproval();

        IEnumerable<RequestOvertimeEntity> GetAllForApproval(int employee_id);

        RequestOvertimeEntity GetOvertimeDetails(int employee_id, DateTime dtOt);

        int CountApprovedOvertime(int employee_id);

        int CountApprovedOvertime(int employee_id, DateTime dtStart,DateTime dtEnd);





        ////
        IEnumerable<RequestOvertimeEntity> GetList(int employee_id);
        IEnumerable<RequestOvertimeEntity> GetForApproval(int employee_id);

        bool IsExist(int employee_id, DateTime shiftdate);
        bool IsApproved(int employee_id, DateTime shiftdate, decimal leave);
        bool Delete(int request_leave_id);


        bool Approve(long request_leave_id, string approver_remark, int approver_id);
        bool Disapprove(long request_leave_id, string approver_remark, int approver_id);
        bool CreateOrUpdate(RequestOvertimeEntity obj);

        RequestOvertimeEntity GetByID(int id);
    }
}
