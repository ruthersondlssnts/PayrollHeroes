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
using Payroll.Core.ViewModel;

namespace Payroll.Infrastructure.Repositories 
{
    public class EmployeeBalanceRepository : IEmployeeBalance
    {
        private PayrollDBContext db = new PayrollDBContext();
        private IMapper _mapper;


        public EmployeeBalanceRepository()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<EmployeeBalanceEntity, employee_balance>();
                cfg.CreateMap<employee_balance, EmployeeBalanceEntity>();

                cfg.CreateMap<RefLeaveTypeEntity, ref_leave_type>();
                cfg.CreateMap<ref_leave_type, RefLeaveTypeEntity>();

                cfg.CreateMap<EmployeeEntity, employee>();
                cfg.CreateMap<employee, EmployeeEntity>();
            });
            _mapper = config.CreateMapper();
        }
        public bool Add(EmployeeBalanceEntity entity)
        {
            bool blnReturn = true;
            var dataEntity = _mapper.Map<EmployeeBalanceEntity, employee_balance>(entity);
            db.employee_balance.Add(dataEntity);
            db.SaveChanges();
            return blnReturn;
        }

        public bool Update(EmployeeBalanceEntity entity)
        {
            bool blnReturn = true;
            var dataEntity = _mapper.Map<EmployeeBalanceEntity, employee_balance>(entity);
            db.employee_balance.Update(dataEntity);
            db.SaveChanges();
            return blnReturn;
        }
        public IEnumerable<EmployeeBalanceEntity> GetList()
        {
            var data = db.employee_balance.
                Include(a => a.employee_).
                Include(a => a.ref_leave_type_).
                Where(a => a.date_deleted == null).ToList();
            var dataEntity = _mapper.Map<IEnumerable<employee_balance>, IEnumerable<EmployeeBalanceEntity>>(data);
            return dataEntity;
        }
        public IEnumerable<EmployeeBalanceEntity> GetList(int employee_id)
        {
            var data = db.employee_balance.
                Include(a=>a.employee_).
                Include(a=>a.ref_leave_type_).
                Where(a=>a.date_deleted == null && a.employee_id== employee_id).ToList();
            var dataEntity = _mapper.Map<IEnumerable<employee_balance>, IEnumerable<EmployeeBalanceEntity>>(data);
            return dataEntity;
        }
        public IEnumerable<EmployeeBalanceEntity> GetList(DateTime dt,int employee_id)
        {
            var data = db.employee_balance.
                Include(a => a.employee_).
                Include(a => a.ref_leave_type_).
                Where(a => a.date_deleted == null && a.employee_id == employee_id && a.acquire_date <= dt && a.expire_date >= dt && a.date_deleted == null).ToList();
            var dataEntity = _mapper.Map<IEnumerable<employee_balance>, IEnumerable<EmployeeBalanceEntity>>(data);
            return dataEntity;
        }
        public bool CreateOrUpdate(EmployeeBalanceEntity obj)
        {
            bool blnReturn = true;

            if (obj.employee_balance_id == 0)
            {
                blnReturn = Add(obj);
            }
            else
            {
                blnReturn = Update(obj);
            }
            return true;
        }

        public EmployeeBalanceEntity GetByID(int id)
        {
            var data = db.employee_balance.Where(a => a.employee_balance_id == id).FirstOrDefault();
            return _mapper.Map<employee_balance, EmployeeBalanceEntity>(data);
        }

        public bool Delete(int id)
        {
            var data = db.employee_balance.Where(a => a.employee_balance_id == id).FirstOrDefault();
            if (data != null)
            {
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public decimal GetBalance(DateTime dt, int leave_type_id, int empid)
        {
            var check = db.ref_leave_type.Where(a => a.ref_leave_type_id == leave_type_id).FirstOrDefault();
            if (check.with_pay == true)
            {
                var employee_bal = db.employee_balance.
                Where(a => a.ref_leave_type_id  == leave_type_id && 
                a.employee_id == empid && 
                (a.acquire_date <=dt && 
                a.expire_date >= dt)).FirstOrDefault();

                var balance = employee_bal.quantity;


                var transaction = db.employee_balance_transaction.
                Where(a => a.employee_balance_id == employee_bal.employee_balance_id && a.date_deleted == null).
                Select(a => a.quantity).Sum();

                return balance - transaction;
            }
            else
            {
                return 99;
            }

        }

        public List<EmployeeLeaveBalanceViewModel> GetEmployeeBalance(DateTime dt, int employee_id)
        {
            List<EmployeeLeaveBalanceViewModel> returnData = new List<EmployeeLeaveBalanceViewModel>();
            var data = db.employee_balance.
                Include(a => a.employee_).
                Include(a => a.ref_leave_type_).
                Where(a => a.date_deleted == null && a.employee_id == employee_id && 
                a.acquire_date <= dt &&
                a.expire_date >= dt && a.ref_leave_type_.with_pay).ToList();

            foreach (var row in data)
            {
                EmployeeLeaveBalanceViewModel newRec = new EmployeeLeaveBalanceViewModel();
                newRec.leave_type_name = row.ref_leave_type_.ref_leave_type_name;
                newRec.earned = row.quantity;
                
                //GET TRANSACTIONS
                var transaction = db.employee_balance_transaction.
                Where(a => a.employee_balance_id == row.employee_balance_id && a.date_deleted == null).
                Select(a => a.quantity).Sum();

                newRec.used = transaction;
                newRec.employee_balance_id = row.employee_balance_id;
                newRec.balance = newRec.earned - newRec.used;
                returnData.Add(newRec);
            }
            return returnData;
        }
    }
}