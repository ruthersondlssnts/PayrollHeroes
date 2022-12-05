using System;
using System.Collections.Generic;

namespace Payroll.Infrastructure.Models
{
    public partial class timesheet_log
    {
        public long timesheet_log_id { get; set; }
        public string employee_serial { get; set; }
        public DateTime? datetime_in { get; set; }
        public DateTime? datetime_out { get; set; }
    }
}
