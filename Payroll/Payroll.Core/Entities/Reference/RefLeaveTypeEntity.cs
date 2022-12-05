using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll.Core.Entities
{
    //ref_leave_type
    public class RefLeaveTypeEntity
    {
        public int ref_leave_type_id { get; set; }
        public string ref_leave_type_name { get; set; }
        public string ref_leave_type_code { get; set; }
        public bool with_pay { get; set; }
    }
}
