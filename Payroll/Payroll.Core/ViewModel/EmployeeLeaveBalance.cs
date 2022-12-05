using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Core.ViewModel
{
    public class EmployeeLeaveBalanceViewModel
    {
        public int employee_balance_id { get; set; }
        public string leave_type_name { get; set; }
        public decimal earned { get; set; }
        public decimal used { get; set; }
        public decimal balance  { get; set; }
    }
}
