using System;
using System.Collections.Generic;

namespace Payroll.Core.Entities
{
    public partial class ref_pagibig
    {
        public int ref_pagibig_id { get; set; }
        public string name { get; set; }
        public decimal? salary_from { get; set; }
        public decimal? salary_to { get; set; }
        public string computation { get; set; }
        public decimal? employee_contribution { get; set; }
        public decimal? employer_contribution { get; set; }
        public bool flat_rate { get; set; }
        public DateTime? date_deleted { get; set; }
    }
}