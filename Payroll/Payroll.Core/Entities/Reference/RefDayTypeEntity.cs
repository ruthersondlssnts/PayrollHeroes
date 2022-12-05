using System;
using System.Collections.Generic;

namespace Payroll.Core.Entities
{
    public partial class RefDayTypeEntity
    {
        //RWD
        //RD


        public int ref_day_type_id { get; set; }
        public string date_type_code { get; set; }
        public string date_type_name { get; set; }
        public decimal ot_multiplier { get; set; }
        public decimal ot8_multiplier { get; set; }
        public decimal nightdif_multiplier1 { get; set; }
        public decimal nightdif_multiplier2 { get; set; }
        public virtual ICollection<EmployeeTimesheetEntity> employee_timesheet { get; set; }
    }
}