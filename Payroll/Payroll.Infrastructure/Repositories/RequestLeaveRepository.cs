using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Payroll.Core.Entities;
using Payroll.Core.Interface;
using Payroll.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Infrastructure.Repositories
{
    public class RequestLeaveRepository : IRepository<RequestLeaveEntity>, IRequestLeave
    {
        private PayrollDBContext db = new PayrollDBContext();
        private IMapper _mapper;

        public RequestLeaveRepository()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RequestLeaveEntity, request_leave>();
                cfg.CreateMap<request_leave, RequestLeaveEntity>();

                cfg.CreateMap<RefDepartmentEntity, ref_department>();
                cfg.CreateMap<ref_department, RefDepartmentEntity>();

                cfg.CreateMap<RefLeaveTypeEntity, ref_leave_type>();
                cfg.CreateMap<ref_leave_type, RefLeaveTypeEntity>();

                cfg.CreateMap<RefStatusEntity, ref_status>();
                cfg.CreateMap<ref_status, RefStatusEntity>();

                cfg.CreateMap<EmployeeEntity, employee>();
                cfg.CreateMap<employee, EmployeeEntity>();
            });
            _mapper = config.CreateMapper();
        }

        //public void Add(RequestLeaveEntity entity)
        //{
        //    throw new NotImplementedException();
        //}

        public void AddRange(IEnumerable<RequestLeaveEntity> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RequestLeaveEntity> Find(Expression<Func<RequestLeaveEntity, bool>> predecate)
        {
            throw new NotImplementedException();
        }

        public RequestLeaveEntity Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RequestLeaveEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RequestLeaveEntity> GetApprovedLeave(int employee_id, DateTime shiftdate)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<RequestLeaveEntity> GetForApproval(int employee_id)
        {
            var group_id = db.employee_group.Where(a => a.employee_id == employee_id).FirstOrDefault().group_id;

            IEnumerable<RequestLeaveEntity> record;
            var data = db.request_leave.Include(a => a.employee_).Include(a => a.ref_leave_type_).Include(a => a.ref_status_).Where(a => a.approver_id == employee_id && a.date_deleted == null && a.ref_status_id==1 && a.group_id == group_id).ToList();
            record = _mapper.Map<IEnumerable<request_leave>, IEnumerable<RequestLeaveEntity>>(data);

            return record;
        }
        public RequestLeaveEntity GetApprovedLeave(int employee_id, DateTime shiftdate, RequestLeaveEntity data)
        {
            throw new NotImplementedException();
        }

        public bool IsApproved(int employee_id, DateTime leave_date, out decimal leaveday)
        {
            leaveday = 0;

            try
            {

                if (IsExist(employee_id, leave_date))
                {
                    var record = db.request_leave.FirstOrDefault(a => a.approver_id == employee_id && a.leave_date == leave_date && a.ref_status_id == 2);
                    leaveday = record.leave_day;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool IsApproved(int employee_id, DateTime shiftdate, decimal leave)
        {
            throw new NotImplementedException();
        }

        public bool IsExist(int employee_id, DateTime leavedate)
        {
            try
            {
                var record = db.request_leave.FirstOrDefault(a => a.employee_id == employee_id && a.leave_date == leavedate && a.ref_status_id==2 && a.date_deleted == null);
                if (record != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void Remove()
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<RequestLeaveEntity> entities)
        {
            throw new NotImplementedException();
        }

        public bool UpdateLeave(int employee_id, DateTime leave_date, decimal leave, int ref_leave_type_id)
        {
            try
            {
                if (IsExist(employee_id, leave_date))
                {
                    var record = db.request_leave.FirstOrDefault(a => a.employee_id == employee_id && a.leave_date == leave_date);
                    record.leave_day = leave;
                    record.ref_leave_type_id = ref_leave_type_id;
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool GetApprovedLeave(int employee_id, DateTime leave_date,out decimal leave, out int ref_leave_type_id)
        {
            leave = 0;
            ref_leave_type_id = 0;
            try
            {
                if (IsExist(employee_id, leave_date))
                {
                    var record = db.request_leave.FirstOrDefault(a => a.employee_id == employee_id && a.leave_date == leave_date && a.ref_status_id==2);
                    leave = record.leave_day;
                    ref_leave_type_id = record.ref_leave_type_id;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool Add(RequestLeaveEntity entity)
        {
            try
            {
                if (!IsExist(entity.employee_id, entity.leave_date))
                {  
                    //Get Group
                    var group_id = db.employee_group.Where(a => a.employee_id == entity.employee_id).FirstOrDefault().group_id;
                    entity.group_id = group_id;

                    var dataEntity = _mapper.Map<RequestLeaveEntity, request_leave>(entity);
                    db.request_leave.Add(dataEntity);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                return false;
            }

        }

        public bool Update(RequestLeaveEntity entity)
        {
            try
            {
                var data = db.request_leave.Where(a => a.request_leave_id == entity.request_leave_id && a.date_deleted == null).FirstOrDefault();

                if (data != null)
                {
                    data.leave_date = entity.leave_date;
                    data.leave_day = entity.leave_day;
                    data.reason = entity.reason;
                    data.ref_leave_type_id = entity.ref_leave_type_id;
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                return false;
            }

        }

        void IRepository<RequestLeaveEntity>.Add(RequestLeaveEntity entity)
        {
            throw new NotImplementedException();
        }

        void IRepository<RequestLeaveEntity>.Update(RequestLeaveEntity entity)
        {
            throw new NotImplementedException();
        }

        public bool Approve(long request_leave_id, string approver_remark, int approver_id)
        {
            var data = db.request_leave.Where(a => a.request_leave_id == request_leave_id && a.date_deleted == null).FirstOrDefault();
            if (data != null)
            {
                //INSERT BALANCE in employee_balance
                var get_employee_leave_balance = db.employee_balance.
                    Where(a => a.ref_leave_type_id == data.ref_leave_type_id && a.acquire_date <= data.leave_date && a.expire_date >= data.leave_date).FirstOrDefault();

                if (get_employee_leave_balance != null)
                {
                    data.approver_remark = approver_remark;
                    data.approver_id = approver_id;
                    data.approver_date = DateTime.Now;
                    data.ref_status_id = 2;
                    db.SaveChanges();


                    employee_balance_transaction objtransact = new employee_balance_transaction();
                    objtransact.employee_balance_id = get_employee_leave_balance.employee_balance_id;
                    objtransact.employee_id = data.employee_id;
                    objtransact.quantity = data.leave_day;
                    objtransact.requested_date = data.leave_date;
                    objtransact.approved_date = (DateTime)data.approver_date;
                    db.employee_balance_transaction.Add(objtransact);
                    db.SaveChanges();

                    return true;
                }
            }
            return false;
        }

        public bool Disapprove(long request_leave_id, string approver_remark, int approver_id)
        {
            var data = db.request_leave.Where(a => a.request_leave_id == request_leave_id && a.date_deleted == null).FirstOrDefault();
            if (data != null)
            {
                data.approver_remark = approver_remark;
                data.approver_id = approver_id;
                data.approver_date = DateTime.Now;
                data.ref_status_id = 3;
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Delete(int request_leave_id)
        {
            var data = db.request_leave.Where(a => a.request_leave_id == request_leave_id).FirstOrDefault();
            if (data != null)
            {
                data.date_deleted = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public IEnumerable<RequestLeaveEntity> GetList()
        {
            IEnumerable<RequestLeaveEntity> record;
            var data = db.request_leave.Include(a => a.employee_).Include(a => a.ref_leave_type_).Include(a => a.ref_status_).
                Where(a => a.date_deleted == null && (a.ref_status_id==2 || a.ref_status_id == 1)).ToList();
            record = _mapper.Map<IEnumerable<request_leave>, IEnumerable<RequestLeaveEntity>>(data);

            return record;
        }
        public IEnumerable<RequestLeaveEntity> GetList(int employee_id)
        {
            IEnumerable<RequestLeaveEntity> record;
            var data = db.request_leave.Include(a=>a.ref_leave_type_).Include(a=>a.ref_status_).
                Where(a => a.employee_id == employee_id && a.date_deleted == null).ToList();
            record = _mapper.Map<IEnumerable<request_leave>, IEnumerable<RequestLeaveEntity>>(data);

            return record;
        }

        public RequestLeaveEntity GetByID(int id)
        {
            var data = db.request_leave.Include(a=>a.employee_).Where(a => a.request_leave_id == id && a.date_deleted == null).FirstOrDefault();
            return _mapper.Map<request_leave, RequestLeaveEntity>(data);
        }
        public bool CreateOrUpdate(RequestLeaveEntity obj)
        {
            bool blnReturn = true;
            if (obj.request_leave_id == 0)
            {
                blnReturn =Add(obj);
            }
            else
            {
                blnReturn = Update(obj);
            }
            return true;
        }
    }
}
