using System;
using System.Collections.Generic;

namespace Payroll.Infrastructure.Models
{
    public partial class employee_balance_transaction
    {
        public int employee_balance_transaction_id { get; set; }
        public int employee_id { get; set; }
        public int employee_balance_id { get; set; }
        public DateTime requested_date { get; set; }
        public DateTime approved_date { get; set; }
        public decimal quantity { get; set; }
        public DateTime? date_deleted { get; set; }

        public employee employee_ { get; set; }
        public employee_balance employee_balance_ { get; set; }
    }
}
