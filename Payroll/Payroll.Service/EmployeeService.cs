using Payroll.Core.Entities;
using Payroll.Infrastructure.Repositories;
using Payroll.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Payroll.Service
{
    public class EmployeeService : IEmployee
    {
        EmployeeRepository _repo;
        RefShiftRepository _reposhift;
        public EmployeeService()
        {
            _repo = new EmployeeRepository();
        }

        public bool CreateOrUpdate(EmployeeEntity obj)
        {
           return _repo.CreateOrUpdate(obj);
        }

        public bool Delete(int id)
        {
            return _repo.Delete(id);
        }

        public EmployeeEntity GetByID(int id)
        {
            return _repo.GetByID(id);
        }

        public IEnumerable<EmployeeEntity> GetList()
        {
            return _repo.GetList();
        }
        public IEnumerable<EmployeeEntity> GetList(int  deptID)
        {
            return _repo.GetList(deptID);
        }

        public bool Exist(string name)
        {
            return _repo.Exist(name);
        }
        public EmployeeEntity Login(string username, string password, out string access)
        {
            access = "";
            return _repo.Login(username, password, out access);
        }
    }
}
