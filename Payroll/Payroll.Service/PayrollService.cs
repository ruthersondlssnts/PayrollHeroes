using Payroll.Core.Entities;
using Payroll.Service.Inferface;
using System;
using System.Collections.Generic;
using System.Text;
using Payroll.Infrastructure.Repositories;

namespace Payroll.Service
{
    public class PayrollService : IPayroll
    {
        PayrollRepository _repo;

        public PayrollService()
        {
            _repo = new PayrollRepository();
        }


        public IEnumerable<PayrollDetailsEntity> GetList(int employee_id, int ref_payroll_cutoff_id)
        {
            return _repo.GetList(employee_id, ref_payroll_cutoff_id);
        }
    }
}
