using System;
using System.Collections.Generic;

namespace Payroll.Infrastructure.Models
{
    public partial class ref_shift
    {
        public ref_shift()
        {
            employee = new HashSet<employee>();
            employee_timesheet = new HashSet<employee_timesheet>();
            ref_shift_detail = new HashSet<ref_shift_detail>();
            request_dtr = new HashSet<request_dtr>();
        }

        public int ref_shift_id { get; set; }
        public string shift_name { get; set; }
        public string shift_in { get; set; }
        public string shift_out { get; set; }
        public string break_in { get; set; }
        public string break_out { get; set; }
        public decimal? break_hour { get; set; }
        public decimal? required_hour { get; set; }
        public string nd_start { get; set; }
        public string nd_end { get; set; }
        public string nd_start2 { get; set; }
        public string nd_end2 { get; set; }
        public decimal grace_period { get; set; }
        public bool include_grace_period { get; set; }
        public bool? is_auto_ot { get; set; }
        public bool is_nd { get; set; }
        public DateTime? date_deleted { get; set; }

        public ICollection<employee> employee { get; set; }
        public ICollection<employee_timesheet> employee_timesheet { get; set; }
        public ICollection<ref_shift_detail> ref_shift_detail { get; set; }
        public ICollection<request_dtr> request_dtr { get; set; }
    }
}
