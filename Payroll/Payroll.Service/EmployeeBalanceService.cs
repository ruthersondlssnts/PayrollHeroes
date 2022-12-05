using Payroll.Core.Entities;
using Payroll.Core.ViewModel;
using Payroll.Infrastructure.Repositories;
using Payroll.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Payroll.Service
{
    public class EmployeeBalanceService : IEmployeeBalance
    {
        EmployeeBalanceRepository _repo;

        public EmployeeBalanceService()
        {
            _repo = new EmployeeBalanceRepository();
        }

        public bool CreateOrUpdate(EmployeeBalanceEntity obj)
        {
            return _repo.CreateOrUpdate(obj);
        }

        public bool Delete(int id)
        {
            return _repo.Delete(id);
        }

        public decimal GetBalance(DateTime dt, int leave_type_id, int empid)
        {
            return _repo.GetBalance(dt, leave_type_id, empid);
        }

        public EmployeeBalanceEntity GetByID(int id)
        {
            return _repo.GetByID(id);
        }

        public IEnumerable<EmployeeBalanceEntity> GetList(int id)
        {
            return _repo.GetList(id);
        }
        public IEnumerable<EmployeeBalanceEntity> GetList()
        {
            return _repo.GetList();
        }
        public IEnumerable<EmployeeBalanceEntity> GetList(DateTime dt, int employee_id)
        {
            return _repo.GetList(dt, employee_id);
        }

        public List<EmployeeLeaveBalanceViewModel> GetEmployeeBalance(DateTime dt, int employee_id)
        {
            return _repo.GetEmployeeBalance(dt, employee_id);
        }
    }
}
