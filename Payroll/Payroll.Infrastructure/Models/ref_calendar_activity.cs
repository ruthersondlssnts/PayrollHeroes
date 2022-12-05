using System;
using System.Collections.Generic;

namespace Payroll.Infrastructure.Models
{
    public partial class ref_calendar_activity
    {
        public int ref_calendar_activity_id { get; set; }
        public DateTime work_date { get; set; }
        public int ref_day_type_id { get; set; }
        public string description { get; set; }
        public DateTime? date_deleted { get; set; }

        public ref_day_type ref_day_type_ { get; set; }
    }
}
