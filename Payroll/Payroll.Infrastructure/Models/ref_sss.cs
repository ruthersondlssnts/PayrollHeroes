using System;
using System.Collections.Generic;

namespace Payroll.Infrastructure.Models
{
    public partial class ref_sss
    {
        public int ref_sss_id { get; set; }
        public string name { get; set; }
        public decimal? salary_from { get; set; }
        public decimal? salary_to { get; set; }
        public decimal? monthly_salary_credit { get; set; }
        public decimal? employee_share { get; set; }
        public decimal? employer_share { get; set; }
        public DateTime? date_deleted { get; set; }
    }
}
