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
    public class EmployeeGroupService : IEmployeeGroup
    {
        EmployeeGroupRepository _repo;

        public EmployeeGroupService()
        {
            _repo = new EmployeeGroupRepository();
        }

        public bool CreateOrUpdate(EmployeeGroupEntity obj)
        {
           return _repo.CreateOrUpdate(obj);
        }

        public bool Delete(int id)
        {
            return _repo.Delete(id);
        }
               
        public IEnumerable<EmployeeGroupEntity> GetList(int  deptID)
        {
            return _repo.GetList(deptID);
        }

        public bool UpdateApproval(int employee_id, int type, bool value)
        {
            return _repo.UpdateApproval(employee_id, type, value);
        }

        public string Validate(int id)
        {
            return _repo.Validate(id);
        }
    }
}
