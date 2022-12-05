using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Core.Entities
{
    public class EmployeeBalanceTransactionEntity
    {
        public int employee_balance_transaction_id { get; set; }
        public int employee_id { get; set; }
        public int employee_balance_id { get; set; }
        public DateTime requested_date { get; set; }
        public DateTime approved_date { get; set; }
        public decimal quantity { get; set; }
        public DateTime? date_deleted { get; set; }

        public EmployeeEntity employee_ { get; set; }
        public EmployeeBalanceEntity employee_balance_ { get; set; }
    }
}
