using Payroll.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll.Service.Inferface
{
    public interface IPayroll
    {
        IEnumerable<PayrollDetailsEntity> GetList(int employee_id, int ref_payroll_cutoff_id);
    }
}
