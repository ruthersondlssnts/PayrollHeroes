using Payroll.Core.Entities;
using Payroll.Infrastructure.Repositories;
using Payroll.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Service
{
    public class RequestOvertimeService : IRequestOvertime
    {
        RequestOvertimeRepository _requestovertimerepo;

        public RequestOvertimeService()
        {
            _requestovertimerepo = new RequestOvertimeRepository();
        }

        public bool Approve(long request_leave_id, string approver_remark, int approver_id)
        {
            return _requestovertimerepo.Approve(request_leave_id,  approver_remark,  approver_id);
        }

        public bool Delete(int request_leave_id)
        {
            return _requestovertimerepo.Delete(request_leave_id);
        }
        public RequestOvertimeEntity GetByID(int id) {
            return _requestovertimerepo.GetByID(id);
        }
        public bool Disapprove(long request_leave_id, string approver_remark, int approver_id)
        {
            return _requestovertimerepo.Disapprove(request_leave_id, approver_remark, approver_id);
        }

        public IEnumerable<RequestOvertimeEntity> GetApprovedOvertime(int employee_id, DateTime shiftdate)
        {
            return _requestovertimerepo.GetApprovedOvertime( employee_id,  shiftdate);
        }

        public IEnumerable<RequestOvertimeEntity> GetForApproval(int employee_id)
        {
            return _requestovertimerepo.GetForApproval(employee_id);
        }

        public bool GetApprovedOvertime(int employee_id, DateTime leave_date, out decimal leave, out int ref_leave_type_id)
        {
            throw new NotImplementedException();
        }

        public RequestOvertimeEntity GetApprovedLeave(int employee_id, DateTime shiftdate, RequestLeaveEntity data)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RequestOvertimeEntity> GetList(int employee_id)
        {
            return _requestovertimerepo.GetList(employee_id);

        }

        public bool IsApproved(int employee_id, DateTime shiftdate, decimal leave)
        {
            throw new NotImplementedException();
        }

        public bool IsExist(int employee_id, DateTime shiftdate)
        {
            return _requestovertimerepo.IsExist(employee_id, shiftdate);
        }

        public bool UpdateLeave(int employee_id, DateTime leave_date, decimal leave, int ref_leave_type_id)
        {
            throw new NotImplementedException();
        }

        public bool CreateOrUpdate(RequestOvertimeEntity obj)
        {
            return _requestovertimerepo.CreateOrUpdate(obj);
        }
    }
}
