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
    public class RequestOvertimeRepository : IRepository<RequestOvertimeEntity>, IRequestOvertime, IRequest
    {
        private PayrollDBContext db = new PayrollDBContext();
        private IMapper _mapper;


        public RequestOvertimeRepository()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RequestOvertimeEntity, request_overtime>();
                cfg.CreateMap<request_overtime, RequestOvertimeEntity>();

                cfg.CreateMap<EmployeeEntity, employee>();
                cfg.CreateMap<employee, EmployeeEntity>();

                cfg.CreateMap<RefDepartmentEntity, ref_department>();
                cfg.CreateMap<ref_department, RefDepartmentEntity>();

                cfg.CreateMap<RefOtTypeEntity, ref_overtime_type>();
                cfg.CreateMap<ref_overtime_type, RefOtTypeEntity>();

                cfg.CreateMap<RefStatusEntity, ref_status>();
                cfg.CreateMap<ref_status, RefStatusEntity>();


            });
            _mapper = config.CreateMapper();
        }

        //public void Add(RequestOvertimeEntity entity)
        //{
        //    if (!IsExist(entity.employee_id, entity.overtime_date))
        //    {
        //        var dataEntity = _mapper.Map<RequestOvertimeEntity, request_overtime>(entity);
        //        db.request_overtime.Add(dataEntity);
        //        db.SaveChanges();
        //    }

        //}

        //public void Update(RequestOvertimeEntity entity)
        //{
        //    var data = db.request_overtime.Where(a => a.request_overtime_id == entity.request_overtime_id).FirstOrDefault();
        //    data = _mapper.Map<RequestOvertimeEntity, request_overtime>(entity);
        //    db.SaveChanges();
        //}

        public void AddRange(IEnumerable<RequestOvertimeEntity> entities)
        {
            throw new NotImplementedException();
        }

        public bool Approve(DateTime dtOt, int employee_id, string approver_remark, int approver_id)
        {
            var data = db.request_overtime.Where(a => a.employee_id == employee_id && a.overtime_date == dtOt && a.date_deleted == null).FirstOrDefault();
            if (data != null)
            {
                data.approver_remark = approver_remark;
                data.approver_id = approver_id;
                data.approver_date = DateTime.Now;
                data.ref_status_id = 2;
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Disapprove(DateTime dtOt, int employee_id, string approver_remark, int approver_id)
        {
            var data = db.request_overtime.Where(a => a.employee_id == employee_id && a.overtime_date == dtOt && a.date_deleted == null).FirstOrDefault();
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

        public int CountApprovedOvertime(int employee_id)
        {
            throw new NotImplementedException();
        }

        public int CountApprovedOvertime(int employee_id, DateTime dtStart, DateTime dtEnd)
        {
            throw new NotImplementedException();
        }

        public bool Create(DateTime dtOt, int employee_id, int ref_department_id, string time_in, string time_out, string reason)
        {
            throw new NotImplementedException();
        }



        public IEnumerable<RequestOvertimeEntity> Find(Expression<Func<RequestOvertimeEntity, bool>> predecate)
        {
            throw new NotImplementedException();
        }

        public RequestOvertimeEntity Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RequestOvertimeEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RequestOvertimeEntity> GetAll(int employee_id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RequestOvertimeEntity> GetAllForApproval()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RequestOvertimeEntity> GetAllForApproval(int employee_id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RequestOvertimeEntity> GetApproved(int employee_id)
        {
            throw new NotImplementedException();
        }

        public RequestOvertimeEntity GetOvertimeDetails(int employee_id, DateTime dtOt)
        {
            throw new NotImplementedException();
        }

        public bool IsApproved(int employee_id, DateTime dtOt, out string time_in, out string time_out)
        {
            time_out = null;
            time_in = null;
            var data = db.request_overtime.Where(a => a.employee_id == employee_id && a.overtime_date == dtOt && a.ref_status_id == 2 && a.date_deleted == null).FirstOrDefault();
            if (data != null)
            {
                time_out = data.time_out;
                time_in = data.time_in;
                return true;
            }

            return false;
        }

        public bool IsExist(int employee_id, DateTime dtOt)
        {
            var data = db.request_overtime.Where(a => a.employee_id == employee_id && a.overtime_date == dtOt && a.date_deleted == null).FirstOrDefault();
            if (data != null)
            {
                return true;
            }

            return false;
        }

        public void Remove()
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<RequestOvertimeEntity> entities)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int request_overtime_id)
        {
            var data = db.request_overtime.Where(a => a.request_overtime_id == request_overtime_id).FirstOrDefault();
            if (data != null)
            {
                data.date_deleted = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool BatchApprove(IEnumerable<RequestOvertimeEntity> list)
        {
            return false;
        }

        public bool BatchDisapprove(IEnumerable<RequestOvertimeEntity> list)
        {
            return false;
        }

        public bool IsApproved(int employee_id, DateTime date)
        {
            var data = db.request_overtime.Where(a => a.employee_id == employee_id && a.overtime_date == date && a.ref_status_id == 2 && a.date_deleted == null).FirstOrDefault();
            if (data != null)
            {
                return true;
            }

            return false;
        }

        public IEnumerable<RequestOvertimeEntity> GetList(int employee_id)
        {
            IEnumerable<RequestOvertimeEntity> record;
            var data = db.request_overtime.
                Include(a => a.employee_).
                Include(a => a.ref_department_).
                Include(a => a.ref_overtime_type_).
                Include(a => a.ref_status_).Where(a => a.employee_id == employee_id && a.date_deleted == null).ToList();
            record = _mapper.Map<IEnumerable<request_overtime>, IEnumerable<RequestOvertimeEntity>>(data);

            return record;
        }

        public IEnumerable<RequestOvertimeEntity> GetForApproval(int employee_id)
        {
            var group_id = db.employee_group.Where(a => a.employee_id == employee_id).FirstOrDefault().group_id;

            IEnumerable<RequestOvertimeEntity> record;
            var data = db.request_overtime.
                Include(a => a.employee_).
                //Include(a => a.approver_).
                Include(a => a.ref_department_).
                Include(a => a.ref_overtime_type_).
                Include(a => a.ref_status_).Where(a => a.approver_id == employee_id && a.date_deleted == null && a.ref_status_id == 1 && a.group_id == group_id).ToList();
            record = _mapper.Map<IEnumerable<request_overtime>, IEnumerable<RequestOvertimeEntity>>(data);

            return record;
        }

        public bool Approve(long request_overtime_id, string approver_remark, int approver_id)
        {
            var data = db.request_overtime.Where(a => a.request_overtime_id == request_overtime_id && a.date_deleted == null).FirstOrDefault();
            if (data != null)
            {
                data.approver_remark = approver_remark;
                data.approver_id = approver_id;
                data.approver_date = DateTime.Now;
                data.ref_status_id = 2;
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Disapprove(long request_overtime_id, string approver_remark, int approver_id)
        {
            var data = db.request_overtime.Where(a => a.request_overtime_id == request_overtime_id && a.date_deleted == null).FirstOrDefault();
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
        public bool CreateOrUpdate(RequestOvertimeEntity obj)
        {
            bool blnReturn = true;
            if (obj.request_overtime_id == 0)
            {
                blnReturn = Add(obj);
            }
            else
            {
                blnReturn = Update(obj);
            }
            return true;
        }
        public bool Add(RequestOvertimeEntity entity)
        {
            try
            {
                if (!IsExist(entity.employee_id, entity.overtime_date))
                {
                    //Get Group
                    var group_id = db.employee_group.Where(a => a.employee_id == entity.employee_id).FirstOrDefault().group_id;
                    entity.group_id = group_id;

                    var dataEntity = _mapper.Map<RequestOvertimeEntity, request_overtime>(entity);
                    db.request_overtime.Add(dataEntity);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool Update(RequestOvertimeEntity entity)
        {
            try
            {
                var data = db.request_overtime.Where(a => a.request_overtime_id == entity.request_overtime_id && a.date_deleted == null).FirstOrDefault();

                if (data != null)
                {
                    data.overtime_date = entity.overtime_date;
                    data.reason = entity.reason;
                    data.ref_overtime_type_id = entity.ref_overtime_type_id;
                    data.time_in = entity.time_in;
                    data.time_out = entity.time_out;
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public RequestOvertimeEntity GetByID(int id)
        {
            var data = db.request_overtime.Include(a => a.employee_).Where(a => a.request_overtime_id == id && a.date_deleted == null).FirstOrDefault();
            return _mapper.Map<request_overtime, RequestOvertimeEntity>(data);
        }

        void IRepository<RequestOvertimeEntity>.Add(RequestOvertimeEntity entity)
        {
            throw new NotImplementedException();
        }

        void IRepository<RequestOvertimeEntity>.Update(RequestOvertimeEntity entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RequestOvertimeEntity> GetApprovedOvertime(int employee_id, DateTime shiftdate)
        {
            throw new NotImplementedException();
        }

        IEnumerable<RequestOvertimeEntity> IRequestOvertime.GetForApproval(int employee_id)
        {
            throw new NotImplementedException();
        }

        public bool IsApproved(int employee_id, DateTime shiftdate, decimal leave)
        {
            throw new NotImplementedException();
        }
    }
}
