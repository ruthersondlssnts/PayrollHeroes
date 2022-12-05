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
    public class EmployeeRepository : IEmployee
    {
        private PayrollDBContext db = new PayrollDBContext();
        private IMapper _mapper;


        public EmployeeRepository()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EmployeeEntity, employee>();
                cfg.CreateMap<employee, EmployeeEntity>();

                cfg.CreateMap<RoleEntity, role>();
                cfg.CreateMap<role, RoleEntity>();
            });
            _mapper = config.CreateMapper();
        }
        public bool Add(EmployeeEntity entity)
        {
            bool blnReturn = true;
            var dataEntity = _mapper.Map<EmployeeEntity, employee>(entity);
            db.employee.Add(dataEntity);
            db.SaveChanges();
            return blnReturn;
        }

        public bool Update(EmployeeEntity entity)
        {
            bool blnReturn = true;
            var dataEntity = _mapper.Map<EmployeeEntity, employee>(entity);
            db.employee.Update(dataEntity);
            db.SaveChanges();
            return blnReturn;
        }

        public EmployeeEntity Login(string username, string password, out string access)
        {
            access = "";
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) { return null; }


            var data = db.employee.Where(a => a.date_deleted == null && a.username == username && a.password == password).FirstOrDefault();
            if (data == null) return null;

            var dataEntity = _mapper.Map<employee, EmployeeEntity>(data);

            //ROLE
            //THE ROLE is disabled for now since it is now handled by  role_menu
            //var rolepermission = db.role_permission.Include(a=>a.permission_).Where(a => a.role_id == data.role_id && a.is_enable).ToList();
            //access = string.Join(",", rolepermission.Select(a=>a.permission_.permission_code));
            access = "dummy_access";

            //Includes roles in Settings>Groupings
            var approver = db.employee_group.Where(a => a.employee_id == data.employee_id).FirstOrDefault();
            if (approver.approver_all) access += "," + Enums.approver.approver_all.GetDescription();
            if (approver.ot_approver) access += "," + Enums.approver.ot_approver.GetDescription();
            if (approver.ob_approver) access += "," + Enums.approver.ob_approver.GetDescription();
            if (approver.leave_approver) access += "," + Enums.approver.leave_approver.GetDescription();
            if (approver.dtr_approver) access += "," + Enums.approver.dtr_approver.GetDescription();
            return dataEntity;
        }

        public IEnumerable<EmployeeEntity> GetList()
        {
            var data = db.employee.Include(a => a.role_).Where(a => a.date_deleted == null);
            var dataEntity = _mapper.Map<IEnumerable<employee>, IEnumerable<EmployeeEntity>>(data);
            return dataEntity;
        }

        public IEnumerable<EmployeeEntity> GetList(int DeptId)
        {
            var approvers = db.ref_department_approver.Where(a => a.ref_department_id == DeptId).Select(a => a.employee_id).ToList();
            var data = db.employee.Include(a => a.role_).Where(a => a.date_deleted == null && !approvers.Contains(a.employee_id));
            var dataEntity = _mapper.Map<IEnumerable<employee>, IEnumerable<EmployeeEntity>>(data);
            return dataEntity;
        }

        public bool CreateOrUpdate(EmployeeEntity obj)
        {
            bool blnReturn = true;

            if (obj.employee_id == 0)
            {
                blnReturn = Add(obj);
            }
            else
            {
                blnReturn = Update(obj);
            }
            return true;
        }

        public EmployeeEntity GetByID(int id)
        {
            var data = db.employee.Where(a => a.employee_id == id && a.date_deleted == null).FirstOrDefault();
            return _mapper.Map<employee, EmployeeEntity>(data);
        }

        public bool Delete(int id)
        {
            var data = db.employee.Where(a => a.employee_id == id).FirstOrDefault();
            if (data != null)
            {
                data.date_deleted = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public bool Exist(string employeeSerial)
        {
            var data = db.employee.Where(a => a.employee_serial == employeeSerial).FirstOrDefault();
            if (data != null)
            {
                return true;
            }
            return false;
        }
    }
}