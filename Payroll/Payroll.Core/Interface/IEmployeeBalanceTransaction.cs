
using Payroll.Core.Interface;
using Payroll.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Payroll.Core.Interface
{
    public interface IEmployeeBalanceTransaction
    {
        IEnumerable<EmployeeBalanceTransactionEntity> GetList(int employee_id);

        IEnumerable<EmployeeBalanceTransactionEntity> GetList(DateTime dt, int employee_id);

        bool CreateOrUpdate(EmployeeBalanceTransactionEntity obj);

        EmployeeBalanceTransactionEntity GetByID(int id);

    }
}