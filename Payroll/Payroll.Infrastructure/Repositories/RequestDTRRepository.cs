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
    public class RequestDTRRepository : IRepository<RequestDTREntity>, IRequestDTR, IRequest
    {
        private PayrollDBContext db = new PayrollDBContext();
        private IMapper _mapper;

        public RequestDTRRepository()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RequestDTREntity, request_dtr>();
                cfg.CreateMap<request_dtr, RequestDTREntity>();

                cfg.CreateMap<EmployeeEntity, employee>();
                cfg.CreateMap<employee, EmployeeEntity>();


                cfg.CreateMap<RefShiftEntity, ref_shift>();
                cfg.CreateMap<ref_shift, RefShiftEntity>();

                cfg.CreateMap<RefStatusEntity, ref_status>();
                cfg.CreateMap<ref_status, RefStatusEntity>();
            });
            _mapper = config.CreateMapper();
        }

        

        public void AddRange(IEnumerable<RequestDTREntity> entities)
        {
            throw new NotImplementedException();
        }

        public bool CreateorUpdate(RequestDTREntity obj)
        {
            try
            {
                //Get Group
                var group_id = db.employee_group.Where(a=>a.employee_id == obj.employee_id).FirstOrDefault().group_id;
                obj.group_id = group_id;
                //Return for Approval
                obj.ref_status_id = 1;
                obj.approver_date = null;

                if (IsExist(obj.employee_id, obj.shift_date))
                {
                    var record = db.request_dtr.FirstOrDefault(a => a.employee_id == obj.employee_id && a.shift_date == obj.shift_date);
                    record.reason = obj.reason;
                    record.ref_shift_id = obj.ref_shift_id;
                    record.shift_date = obj.shift_date;
                    record.time_in = obj.time_in;
                    record.time_out = obj.time_out;
                    record.ref_status_id = 1;
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    var dataEntity = _mapper.Map<RequestDTREntity, request_dtr>(obj);
                    db.request_dtr.Add(dataEntity);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool IsApproved(int employee_id, DateTime shiftdate, out string time_in, out string time_out, out int ref_shift_id)
        {
            time_in = null;
            time_out = null;
            ref_shift_id = 0;
            try
            {

                if (IsExist(employee_id, shiftdate))
                {
                    var record = db.request_dtr.FirstOrDefault(a => a.employee_id == employee_id && a.shift_date == shiftdate && a.ref_status_id == 2);
                    time_in = record.time_in;
                    time_out = record.time_out;
                    ref_shift_id = record.ref_shift_id;
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

        public bool IsExist(int employee_id, DateTime shiftdate)
        {
            try
            {
                var record = db.request_dtr.FirstOrDefault(a => a.employee_id == employee_id && a.shift_date == shiftdate && a.date_deleted == null);
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

        public bool UpdateDTR(int employee_id, DateTime shiftdate, string time_in, string time_out)
        {

            try
            {
                if (IsExist(employee_id, shiftdate))
                {
                    //var record = db.request_dtr.FirstOrDefault(a => a.employee_id == employee_id && a.shift_date == shiftdate);
                    //record.time_in = time_in;
                    //record.time_out = time_out;
                    //db.SaveChanges();
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

        void IRepository<RequestDTREntity>.Add(RequestDTREntity entity)
        {
            throw new NotImplementedException();
        }

        void IRepository<RequestDTREntity>.Update(RequestDTREntity entity)
        {
            throw new NotImplementedException();
        }

        public bool IsApproved(int employee_id, DateTime date)
        {
            try
            {
                var record = db.request_dtr.FirstOrDefault(a => a.employee_id == employee_id && a.shift_date == date && a.ref_status_id==2 && a.date_deleted == null);
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

        public RequestDTREntity Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RequestDTREntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RequestDTREntity> Find(Expression<Func<RequestDTREntity, bool>> predecate)
        {
            throw new NotImplementedException();
        }

        public void Remove()
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<RequestDTREntity> entities)
        {
            throw new NotImplementedException();
        }

        public RequestDTREntity GetApprovedDTR(int employee_id, DateTime shiftdate, RequestDTREntity data)
        {
            throw new NotImplementedException();
        }

        ////
        ///

        public IEnumerable<RequestDTREntity> GetList(int employee_id)
        {
            IEnumerable<RequestDTREntity> record;
            var data = db.request_dtr.
                Include(a => a.employee_).
                Include(a => a.ref_shift_).
                Include(a => a.ref_status_).
                Where(a => a.employee_id == employee_id && a.date_deleted == null).ToList();
            record = _mapper.Map<IEnumerable<request_dtr>, IEnumerable<RequestDTREntity>>(data);

            return record;
        }

        public IEnumerable<RequestDTREntity> GetForApproval(int employee_id)
        {
            var group_id = db.employee_group.Where(a => a.employee_id == employee_id).FirstOrDefault().group_id;
            IEnumerable<RequestDTREntity> record;
            var data = db.request_dtr.
                Include(a => a.employee_).
                Include(a => a.ref_shift_).
                Include(a => a.ref_status_).
                Where(a => a.date_deleted == null && a.ref_status_id == 1 && a.group_id== group_id).ToList();
            record = _mapper.Map<IEnumerable<request_dtr>, IEnumerable<RequestDTREntity>>(data);
            return record;
        }

        public bool Approve(long request_dtr_id, string approver_remark, int approver_id)
        {
            var data = db.request_dtr.Where(a => a.request_dtr_id == request_dtr_id && a.date_deleted == null).FirstOrDefault();
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

        public bool Disapprove(long request_dtr_id, string approver_remark, int approver_id)
        {
            var data = db.request_dtr.Where(a => a.request_dtr_id == request_dtr_id && a.date_deleted == null).FirstOrDefault();
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
        public bool CreateOrUpdate(RequestDTREntity obj)
        {
            bool blnReturn = true;
            if (obj.request_dtr_id == 0)
            {
                blnReturn = Add(obj);
            }
            else
            {
                blnReturn = Update(obj);
            }
            return true;
        }
        public bool Add(RequestDTREntity entity)
        {
            try
            {
                if (!IsExist(entity.employee_id, entity.shift_date))
                {
                    var group_id = db.employee_group.Where(a => a.employee_id == entity.employee_id).FirstOrDefault().group_id;
                    entity.group_id = group_id;
                    var dataEntity = _mapper.Map<RequestDTREntity, request_dtr>(entity);
                    db.request_dtr.Add(dataEntity);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

        }

        public bool Update(RequestDTREntity entity)
        {
            try
            {
                var data = db.request_dtr.Where(a => a.request_dtr_id == entity.request_dtr_id).FirstOrDefault();
                data.shift_date = entity.shift_date;
                data.ref_shift_id = entity.ref_shift_id;
                data.reason = entity.reason;
                data.time_in = entity.time_in;
                data.time_out = entity.time_out;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public RequestDTREntity GetByID(int id)
        {
            var data = db.request_dtr.
                Include(a => a.employee_).
                Include(a => a.ref_shift_).
                Include(a => a.ref_status_).
                Where(a => a.request_dtr_id == id && a.date_deleted == null).FirstOrDefault();
            return _mapper.Map<request_dtr, RequestDTREntity>(data);
        }

        public bool IsApproved(int employee_id, DateTime shiftdate, decimal leave)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int request_leave_id)
        {
            throw new NotImplementedException();
        }
    }
}
