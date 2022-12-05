using System;
using System.Collections.Generic;

namespace Payroll.Core.Entities
{
    public partial class ref_philhealth
    {
        public int ref_philhealth_id { get; set; }
        public string name { get; set; }
        public decimal? salary_from { get; set; }
        public decimal? salary_to { get; set; }
        public string computation { get; set; }
        public DateTime? date_deleted { get; set; }
    }
}