
using Payroll.Core.Interface;
using Payroll.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Payroll.Core.Interface
{
    public interface IEmployeeBalance
    {
        IEnumerable<EmployeeBalanceEntity> GetList(int employee_id);

        IEnumerable<EmployeeBalanceEntity> GetList(DateTime dt, int employee_id);

        bool CreateOrUpdate(EmployeeBalanceEntity obj);

        EmployeeBalanceEntity GetByID(int id);

        decimal GetBalance(DateTime dt, int leave_type_id, int empid);
    }
}