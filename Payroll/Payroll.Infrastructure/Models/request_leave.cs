using System;
using System.Collections.Generic;

namespace Payroll.Infrastructure.Models
{
    public partial class request_leave
    {
        public int request_leave_id { get; set; }
        public int employee_id { get; set; }
        public DateTime leave_date { get; set; }
        public int ref_leave_type_id { get; set; }
        public decimal leave_day { get; set; }
        public int ref_department_id { get; set; }
        public string reason { get; set; }
        public int ref_status_id { get; set; }
        public int? approver_id { get; set; }
        public DateTime? approver_date { get; set; }
        public string approver_remark { get; set; }
        public DateTime? date_deleted { get; set; }
        public int group_id { get; set; }

        public employee employee_ { get; set; }
        public ref_department ref_department_ { get; set; }
        public ref_leave_type ref_leave_type_ { get; set; }
        public ref_status ref_status_ { get; set; }
    }
}
