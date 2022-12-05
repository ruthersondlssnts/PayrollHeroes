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
using Microsoft.EntityFrameworkCore;

namespace Payroll.Infrastructure.Repositories 
{
    public class EmployeeGroupRepository : IEmployeeGroup
    {
        private PayrollDBContext db = new PayrollDBContext();
        private IMapper _mapper;


        public EmployeeGroupRepository()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<EmployeeGroupEntity, employee_group>();
                cfg.CreateMap<employee_group, EmployeeGroupEntity>();

                cfg.CreateMap<EmployeeEntity, employee>();
                cfg.CreateMap<employee, EmployeeEntity>();

                cfg.CreateMap<RefGroupEntity, ref_group>();
                cfg.CreateMap<ref_group, RefGroupEntity>();
            });
            _mapper = config.CreateMapper();
        }
        public bool Add(EmployeeGroupEntity entity)
        {
            bool blnReturn = true;
            var dataEntity = _mapper.Map<EmployeeGroupEntity, employee_group>(entity);
            db.employee_group.Add(dataEntity);
            db.SaveChanges();
            return blnReturn;
        }

        public bool Update(EmployeeGroupEntity entity)
        {
            bool blnReturn = true;
            var dataEntity = db.employee_group.Where(a => a.employee_id == entity.employee_id).FirstOrDefault();
            dataEntity.group_id = entity.group_id;
            db.SaveChanges();
            return blnReturn;
        }

        public bool UpdateApproval(int employee_id, int type, bool value)
        {
            bool blnReturn = true;
            var dataEntity = db.employee_group.Where(a => a.employee_id == employee_id).FirstOrDefault();

            //approver_all
            if (type == 1) { dataEntity.approver_all = value; }

            //ot_approver
            if (type == 2) { dataEntity.ot_approver = value; }

            //ob_approver
            if (type == 3) { dataEntity.ob_approver = value; }

            //leave_approver
            if (type == 4) { dataEntity.leave_approver = value; }

            //dtr_approver
            if (type == 5) { dataEntity.dtr_approver = value; }

            db.SaveChanges();
            return blnReturn;
        }

        public IEnumerable<EmployeeGroupEntity> GetList(int id)
        {
            var data = db.employee_group.Include(a => a.employee_).Include(a => a.group_).Where(a=>a.group_id==id);
            var dataEntity = _mapper.Map<IEnumerable<employee_group>, IEnumerable<EmployeeGroupEntity>>(data);
            return dataEntity;
        }

        public bool CreateOrUpdate(EmployeeGroupEntity obj)
        {
            bool blnReturn = true;
            if (Validate(obj.employee_id) == "")
            {
                blnReturn = Add(obj);
            }
            else
            {
                blnReturn = Update(obj);
            }
            return true;
        }

       
        public bool Delete(int id)
        {
            var data = db.employee_group.Where(a => a.employee_id == id).FirstOrDefault();
            if (data != null)
            {
                db.employee_group.Remove(data);
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public string Validate(int id)
        {
            var data = db.employee_group.Include(a=>a.group_).Where(a => a.employee_id == id).FirstOrDefault();
            if (data != null)
            {
                return data.group_.name;
            }
            return "";
        }


    }
}