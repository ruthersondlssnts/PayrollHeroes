using System;
using System.Collections.Generic;

namespace Payroll.Infrastructure.Models
{
    public partial class request_overtime
    {
        public int request_overtime_id { get; set; }
        public int employee_id { get; set; }
        public DateTime overtime_date { get; set; }
        public int ref_overtime_type_id { get; set; }
        public string time_in { get; set; }
        public string time_out { get; set; }
        public int ref_department_id { get; set; }
        public string reason { get; set; }
        public int ref_status_id { get; set; }
        public string approver_remark { get; set; }
        public int approver_id { get; set; }
        public DateTime? approver_date { get; set; }
        public DateTime? date_deleted { get; set; }
        public int group_id { get; set; }

        public employee employee_ { get; set; }
        public ref_department ref_department_ { get; set; }
        public ref_overtime_type ref_overtime_type_ { get; set; }
        public ref_status ref_status_ { get; set; }
    }
}
