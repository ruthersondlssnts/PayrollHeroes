using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Core.Entities
{
    public class EmployeeBalanceEntity
    {
        public int employee_balance_id { get; set; }
        public int employee_id { get; set; }
        public int ref_leave_type_id { get; set; }
        public DateTime acquire_date { get; set; }
        public DateTime expire_date { get; set; }
        public decimal quantity { get; set; }
        public string remarks { get; set; }
        public DateTime? date_deleted { get; set; }

        public EmployeeEntity employee_ { get; set; }
        public RefLeaveTypeEntity ref_leave_type_ { get; set; }
    }
}
