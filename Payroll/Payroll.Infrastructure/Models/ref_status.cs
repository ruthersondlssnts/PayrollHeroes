using System;
using System.Collections.Generic;

namespace Payroll.Infrastructure.Models
{
    public partial class ref_status
    {
        public ref_status()
        {
            request_dtr = new HashSet<request_dtr>();
            request_leave = new HashSet<request_leave>();
            request_overtime = new HashSet<request_overtime>();
        }

        public int ref_status_id { get; set; }
        public string ref_status_name { get; set; }

        public ICollection<request_dtr> request_dtr { get; set; }
        public ICollection<request_leave> request_leave { get; set; }
        public ICollection<request_overtime> request_overtime { get; set; }
    }
}
