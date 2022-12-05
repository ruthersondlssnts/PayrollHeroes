
using Payroll.Core.Interface;
using Payroll.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Payroll.Core.Interface
{
    public interface IEmployee
    {
        IEnumerable<EmployeeEntity> GetList();

        bool CreateOrUpdate(EmployeeEntity obj);

        EmployeeEntity GetByID(int id);
        bool Delete(int id);
    }
}