using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Core.Entities
{
    public class EmployeeGroupEntity
    {
        public int employee_id { get; set; }
        public int group_id { get; set; }

        public bool approver_all { get; set; }
        public bool ot_approver { get; set; }
        public bool ob_approver { get; set; }
        public bool leave_approver { get; set; }
        public bool dtr_approver { get; set; }
        public EmployeeEntity employee_ { get; set; }
        public RefGroupEntity group_ { get; set; }
    }
}
