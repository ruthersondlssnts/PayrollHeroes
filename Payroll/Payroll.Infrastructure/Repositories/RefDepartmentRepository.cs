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
using Newtonsoft.Json;

namespace Payroll.Infrastructure.Repositories 
{
    public class RefDepartmentRepository : IRefDepartment
    {
        private PayrollDBContext db = new PayrollDBContext();
        private IMapper _mapper;


        public RefDepartmentRepository()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<RefDepartmentEntity, ref_department>();
                cfg.CreateMap<ref_department, RefDepartmentEntity>();

                cfg.CreateMap<RefDepartmentApproverEntity, ref_department_approver>();
                cfg.CreateMap<ref_department_approver, RefDepartmentApproverEntity>();

                cfg.CreateMap<EmployeeEntity, employee>();
                cfg.CreateMap<employee, EmployeeEntity>();
            });
            _mapper = config.CreateMapper();
        }
        public bool Add(RefDepartmentEntity entity)
        {
            bool blnReturn = true;
            var detail = JsonConvert.DeserializeObject<List<RefDepartmentApproverEntity>>(entity.ref_department_approver_.ToString());
            int count = 1;
            foreach (var row in detail)
            {
                row.ordering = count;
                count++;
            }
            entity.ref_department_approver = detail;

            var dataEntity = _mapper.Map<RefDepartmentEntity, ref_department>(entity);
            
            db.ref_department.Add(dataEntity);
            db.SaveChanges();
            return blnReturn;
        }

        public bool Update(RefDepartmentEntity entity)
        {
            bool blnReturn = true;
            var detail = JsonConvert.DeserializeObject<List<RefDepartmentApproverEntity>>(entity.ref_department_approver_.ToString());

            //Remove existing
            var remove = db.ref_department_approver.Where(a => a.ref_department_id == entity.ref_department_id).ToList();
            foreach (var row in remove)
            {
                db.Remove(row);
                db.SaveChanges();
            }

            //Insert new record
            int count = 1;
            foreach (var row in detail)
            {
                row.ordering = count;
                count++;
            }
            entity.ref_department_approver = detail;

            var dataEntity = _mapper.Map<RefDepartmentEntity, ref_department>(entity);
            db.ref_department.Update(dataEntity);
            db.SaveChanges();
            return blnReturn;
        }

        public IEnumerable<RefDepartmentEntity> GetList()
        {
            var data = db.ref_department.ToList();
            var dataEntity = _mapper.Map<IEnumerable<ref_department>, IEnumerable<RefDepartmentEntity>>(data);
            return dataEntity;
        }
        public IEnumerable<RefDepartmentApproverEntity> GetApproverList(int deptId)
        {
            if (deptId== 0)
            {
                return null;
            }

            var data = db.ref_department_approver.
                Include(a=>a.ref_department_).
                Include(a => a.employee_).
                Where(a=>a.ref_department_id == deptId).ToList();
            var dataEntity = _mapper.Map<IEnumerable<ref_department_approver>, IEnumerable<RefDepartmentApproverEntity>>(data);
            return dataEntity;
        }
        public bool CreateOrUpdate(RefDepartmentEntity obj)
        {
            bool blnReturn = true;

            if (obj.ref_department_id == 0)
            {
                blnReturn = Add(obj);
            }
            else
            {
                blnReturn = Update(obj);
            }
            return true;
        }

        public RefDepartmentEntity GetByID(int id)
        {
            var data = db.ref_department.Where(a => a.ref_department_id == id).FirstOrDefault();
            return _mapper.Map<ref_department, RefDepartmentEntity>(data);
        }

        public bool Delete(int id)
        {
            var data = db.ref_department.Where(a => a.ref_department_id == id).FirstOrDefault();
            if (data != null)
            {
                db.SaveChanges();
                return true;
            }
            return false;
        }

    }
}