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
    public class EmployeeBalanceTransactionRepository : IEmployeeBalanceTransaction
    {
        private PayrollDBContext db = new PayrollDBContext();
        private IMapper _mapper;


        public EmployeeBalanceTransactionRepository()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<EmployeeBalanceTransactionEntity, employee_balance_transaction>();
                cfg.CreateMap<employee_balance_transaction, EmployeeBalanceTransactionEntity>();

                cfg.CreateMap<EmployeeBalanceEntity, employee_balance>();
                cfg.CreateMap<employee_balance, EmployeeBalanceEntity>();

                cfg.CreateMap<EmployeeEntity, employee>();
                cfg.CreateMap<employee, EmployeeEntity>();
            });
            _mapper = config.CreateMapper();
        }
        public bool Add(EmployeeBalanceTransactionEntity entity)
        {
            bool blnReturn = true;
            var dataEntity = _mapper.Map<EmployeeBalanceTransactionEntity, employee_balance_transaction>(entity);
            db.employee_balance_transaction.Add(dataEntity);
            db.SaveChanges();
            return blnReturn;
        }

        public bool Update(EmployeeBalanceTransactionEntity entity)
        {
            bool blnReturn = true;
            var dataEntity = _mapper.Map<EmployeeBalanceTransactionEntity, employee_balance_transaction>(entity);
            db.employee_balance_transaction.Update(dataEntity);
            db.SaveChanges();
            return blnReturn;
        }
        public IEnumerable<EmployeeBalanceTransactionEntity> GetList()
        {
            var data = db.employee_balance_transaction.
                Include(a => a.employee_).
                Where(a => a.date_deleted == null).ToList();
            var dataEntity = _mapper.Map<IEnumerable<employee_balance_transaction>, IEnumerable<EmployeeBalanceTransactionEntity>>(data);
            return dataEntity;
        }
        public IEnumerable<EmployeeBalanceTransactionEntity> GetList(int employee_id)
        {
            var data = db.employee_balance_transaction.
                Include(a=>a.employee_).
                Where(a=>a.date_deleted == null && a.employee_id== employee_id).ToList();
            var dataEntity = _mapper.Map<IEnumerable<employee_balance_transaction>, IEnumerable<EmployeeBalanceTransactionEntity>>(data);
            return dataEntity;
        }
        public IEnumerable<EmployeeBalanceTransactionEntity> GetList(DateTime dt,int employee_id)
        {
            var data = db.employee_balance_transaction.
                Include(a => a.employee_).
                Where(a => a.date_deleted == null && a.employee_id == employee_id).ToList();
            var dataEntity = _mapper.Map<IEnumerable<employee_balance_transaction>, IEnumerable<EmployeeBalanceTransactionEntity>>(data);
            return dataEntity;
        }
        public bool CreateOrUpdate(EmployeeBalanceTransactionEntity obj)
        {
            bool blnReturn = true;

            if (obj.employee_balance_transaction_id == 0)
            {
                blnReturn = Add(obj);
            }
            else
            {
                blnReturn = Update(obj);
            }
            return true;
        }

        public EmployeeBalanceTransactionEntity GetByID(int id)
        {
            var data = db.employee_balance_transaction.Where(a => a.employee_balance_transaction_id == id).FirstOrDefault();
            return _mapper.Map<employee_balance_transaction, EmployeeBalanceTransactionEntity>(data);
        }

        public bool Delete(int id)
        {
            var data = db.employee_balance_transaction.Where(a => a.employee_balance_transaction_id == id).FirstOrDefault();
            if (data != null)
            {
                db.SaveChanges();
                return true;
            }
            return false;
        }

    }
}