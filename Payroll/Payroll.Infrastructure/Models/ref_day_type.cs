using System;
using System.Collections.Generic;

namespace Payroll.Infrastructure.Models
{
    public partial class ref_day_type
    {
        public ref_day_type()
        {
            employee_timesheet = new HashSet<employee_timesheet>();
            ref_calendar_activity = new HashSet<ref_calendar_activity>();
        }

        public int ref_day_type_id { get; set; }
        public string date_type_code { get; set; }
        public string date_type_name { get; set; }
        public decimal ot_multiplier { get; set; }
        public decimal ot8_multiplier { get; set; }
        public decimal nightdif_multiplier1 { get; set; }
        public decimal nightdif_multiplier2 { get; set; }

        public ICollection<employee_timesheet> employee_timesheet { get; set; }
        public ICollection<ref_calendar_activity> ref_calendar_activity { get; set; }
    }
}
