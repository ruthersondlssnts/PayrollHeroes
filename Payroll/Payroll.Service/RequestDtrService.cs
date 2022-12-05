using Payroll.Core.Entities;
using Payroll.Infrastructure.Repositories;
using Payroll.Service.Inferface;
using Payroll.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Service
{
    public class RequestDtrService : IRequestDtr
    {
        RequestDTRRepository _requestdtrrepo;

        public RequestDtrService()
        {
            _requestdtrrepo = new RequestDTRRepository();
        }

        public bool Approve(long request_leave_id, string approver_remark, int approver_id)
        {
            return _requestdtrrepo.Approve(request_leave_id,  approver_remark,  approver_id);
        }

        public bool Delete(int request_leave_id)
        {
            return _requestdtrrepo.Delete(request_leave_id);
        }
        public RequestDTREntity GetByID(int id) {
            return _requestdtrrepo.GetByID(id);
        }
        public bool Disapprove(long request_leave_id, string approver_remark, int approver_id)
        {
            return _requestdtrrepo.Disapprove(request_leave_id, approver_remark, approver_id);
        }

        public IEnumerable<RequestDTREntity> GetApprovedDtr(int employee_id, DateTime shiftdate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RequestDTREntity> GetForApproval(int employee_id)
        {
            return _requestdtrrepo.GetForApproval(employee_id);
        }

        public bool GetApprovedOvertime(int employee_id, DateTime leave_date, out decimal leave, out int ref_leave_type_id)
        {
            throw new NotImplementedException();
        }

        public RequestDTREntity GetApprovedLeave(int employee_id, DateTime shiftdate, RequestLeaveEntity data)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RequestDTREntity> GetList(int employee_id)
        {
            return _requestdtrrepo.GetList(employee_id);

        }

        public bool IsApproved(int employee_id, DateTime shiftdate, decimal leave)
        {
            throw new NotImplementedException();
        }

        public bool IsExist(int employee_id, DateTime shiftdate)
        {
            return _requestdtrrepo.IsExist(employee_id, shiftdate);
        }

        public bool UpdateLeave(int employee_id, DateTime leave_date, decimal leave, int ref_leave_type_id)
        {
            throw new NotImplementedException();
        }

        public bool CreateOrUpdate(RequestDTREntity obj)
        {
            return _requestdtrrepo.CreateOrUpdate(obj);
        }
    }
}
