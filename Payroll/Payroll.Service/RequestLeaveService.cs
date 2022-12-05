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
    public class RequestLeaveService : IRequestLeave
    {
        RequestLeaveRepository _requestleaverepo;

        public RequestLeaveService()
        {
            _requestleaverepo = new RequestLeaveRepository();
        }

        public bool Approve(long request_leave_id, string approver_remark, int approver_id)
        {
            return _requestleaverepo.Approve(request_leave_id,  approver_remark,  approver_id);
        }

        public bool Delete(int request_leave_id)
        {
            return _requestleaverepo.Delete(request_leave_id);
        }
        public RequestLeaveEntity GetByID(int id) {
            return _requestleaverepo.GetByID(id);
        }
        public bool Disapprove(long request_leave_id, string approver_remark, int approver_id)
        {
            return _requestleaverepo.Disapprove(request_leave_id, approver_remark, approver_id);
        }

        public IEnumerable<RequestLeaveEntity> GetApprovedLeave(int employee_id, DateTime shiftdate)
        {
            return _requestleaverepo.GetApprovedLeave( employee_id,  shiftdate);
        }

        public IEnumerable<RequestLeaveEntity> GetForApproval(int employee_id)
        {
            return _requestleaverepo.GetForApproval(employee_id);
        }

        public bool GetApprovedLeave(int employee_id, DateTime leave_date, out decimal leave, out int ref_leave_type_id)
        {
            return _requestleaverepo.GetApprovedLeave(employee_id, leave_date, out leave, out ref_leave_type_id);
        }

        public RequestLeaveEntity GetApprovedLeave(int employee_id, DateTime shiftdate, RequestLeaveEntity data)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RequestLeaveEntity> GetList(int employee_id)
        {
            return _requestleaverepo.GetList(employee_id);

        }
        public IEnumerable<RequestLeaveEntity> GetList()
        {
            return _requestleaverepo.GetList();

        }
        public bool IsApproved(int employee_id, DateTime shiftdate, decimal leave)
        {
            throw new NotImplementedException();
        }

        public bool IsExist(int employee_id, DateTime shiftdate)
        {
            return _requestleaverepo.IsExist(employee_id, shiftdate);
        }

        public bool UpdateLeave(int employee_id, DateTime leave_date, decimal leave, int ref_leave_type_id)
        {
            throw new NotImplementedException();
        }

        public bool CreateOrUpdate(RequestLeaveEntity obj)
        {
            return _requestleaverepo.CreateOrUpdate(obj);
        }
    }
}
