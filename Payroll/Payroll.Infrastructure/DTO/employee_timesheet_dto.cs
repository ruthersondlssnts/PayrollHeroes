using System;
using System.Collections.Generic;

namespace Payroll.Infrastructure.Models
{
    public partial class employee_timesheet_dto
    {
        public long employee_timesheet_id { get; set; }

        public string date_type_code { get; set; }

        public string shift_name { get; set; }

        public string first_name { get; set; }

        public string last_name { get; set; }
        public int employee_id { get; set; }
        public DateTime? shift_date { get; set; }
        public int ref_shift_id { get; set; }
        public int ref_day_type_id { get; set; }
        public int? holiday_day_type_id { get; set; }
        public decimal? required_hour { get; set; }
        public decimal? rendered_hour { get; set; }
        public string time_in1 { get; set; }
        public string time_out1 { get; set; }
        public string time_in2 { get; set; }
        public string time_out2 { get; set; }
        public string ot_in { get; set; }
        public string ot_out { get; set; }
        public decimal? ot { get; set; }
        public decimal? ot8 { get; set; }
        public decimal? night_dif { get; set; }
        public decimal? night_dif8 { get; set; }
        public decimal? absent { get; set; }
        public decimal? late { get; set; }
        public decimal? undertime { get; set; }
        public decimal? approve_leave { get; set; }
        public int? ref_leave_type_id { get; set; }
        public bool payroll_process { get; set; }

        public employee employee_ { get; set; }
        public ref_day_type ref_day_type_ { get; set; }
        public ref_shift ref_shift_ { get; set; }
    }
}
