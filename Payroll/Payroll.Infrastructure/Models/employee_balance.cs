using System;
using System.Collections.Generic;

namespace Payroll.Infrastructure.Models
{
    public partial class employee_balance
    {
        public employee_balance()
        {
            employee_balance_transaction = new HashSet<employee_balance_transaction>();
        }

        public int employee_balance_id { get; set; }
        public int employee_id { get; set; }
        public int ref_leave_type_id { get; set; }
        public DateTime acquire_date { get; set; }
        public DateTime expire_date { get; set; }
        public decimal quantity { get; set; }
        public string remarks { get; set; }
        public DateTime? date_deleted { get; set; }

        public employee employee_ { get; set; }
        public ref_leave_type ref_leave_type_ { get; set; }
        public ICollection<employee_balance_transaction> employee_balance_transaction { get; set; }
    }
}
