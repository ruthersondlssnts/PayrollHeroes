using AutoMapper;
using Payroll.Core.Interface;
using Payroll.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using Payroll.Infrastructure.Models;

namespace Payroll.Infrastructure.Repositories 
{
    public class RefLeaveTypeRepository : IRefLeaveType
    {
        private PayrollDBContext db = new PayrollDBContext();
        private IMapper _mapper;


        public RefLeaveTypeRepository()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<RefLeaveTypeEntity, ref_leave_type>();
                cfg.CreateMap<ref_leave_type, RefLeaveTypeEntity>();
            });
            _mapper = config.CreateMapper();
        }
        public bool Add(RefLeaveTypeEntity entity)
        {
            bool blnReturn = true;
            var dataEntity = _mapper.Map<RefLeaveTypeEntity, ref_leave_type>(entity);
            db.ref_leave_type.Add(dataEntity);
            db.SaveChanges();
            return blnReturn;
        }

        public bool Update(RefLeaveTypeEntity entity)
        {
            bool blnReturn = true;
            var dataEntity = _mapper.Map<RefLeaveTypeEntity, ref_leave_type>(entity);
            db.ref_leave_type.Update(dataEntity);
            db.SaveChanges();
            return blnReturn;
        }

        public IEnumerable<RefLeaveTypeEntity> GetList()
        {
            var data = db.ref_leave_type.ToList();
            var dataEntity = _mapper.Map<IEnumerable<ref_leave_type>, IEnumerable<RefLeaveTypeEntity>>(data);
            return dataEntity;
        }
        public IEnumerable<RefLeaveTypeEntity> GetList(int employeeId)
        {
            DateTime dtNow = DateTime.Now;
            var leaves = db.employee_balance.
                Where(a=>a.employee_id == employeeId && a.expire_date >= dtNow).
                Select(a=>a.ref_leave_type_id);
            var data = db.ref_leave_type.Where(a=>leaves.Contains(a.ref_leave_type_id)).ToList();
            var dataEntity = _mapper.Map<IEnumerable<ref_leave_type>, IEnumerable<RefLeaveTypeEntity>>(data);
            return dataEntity;
        }
        public bool CreateOrUpdate(RefLeaveTypeEntity obj)
        {
            bool blnReturn = true;

            if (obj.ref_leave_type_id == 0)
            {
                blnReturn = Add(obj);
            }
            else
            {
                blnReturn = Update(obj);
            }
            return true;
        }

        public RefLeaveTypeEntity GetByID(int id)
        {
            var data = db.ref_leave_type.Where(a => a.ref_leave_type_id == id).FirstOrDefault();
            return _mapper.Map<ref_leave_type, RefLeaveTypeEntity>(data);
        }

        public bool Delete(int id)
        {
            var data = db.ref_leave_type.Where(a => a.ref_leave_type_id == id).FirstOrDefault();
            if (data != null)
            {
                db.SaveChanges();
                return true;
            }
            return false;
        }

    }
}