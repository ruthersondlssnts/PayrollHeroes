//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Payroll.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class employee_timesheet
    {
        public long employee_timesheet_id { get; set; }
        public int employee_id { get; set; }
        public Nullable<System.DateTime> shift_date { get; set; }
        public int ref_shift_id { get; set; }
        public int ref_day_type_id { get; set; }
        public Nullable<decimal> required_hour { get; set; }
        public Nullable<decimal> rendered_hour { get; set; }
        public Nullable<System.TimeSpan> time_in1 { get; set; }
        public Nullable<System.TimeSpan> time_out1 { get; set; }
        public Nullable<System.TimeSpan> time_in2 { get; set; }
        public Nullable<System.TimeSpan> time_out2 { get; set; }
        public Nullable<System.TimeSpan> ot_in { get; set; }
        public Nullable<System.TimeSpan> ot_out { get; set; }
        public Nullable<decimal> ot { get; set; }
        public Nullable<decimal> ot8 { get; set; }
        public Nullable<decimal> night_dif { get; set; }
        public Nullable<decimal> night_dif8 { get; set; }
        public bool payroll_process { get; set; }
    
        public virtual employee employee { get; set; }
        public virtual ref_day_type ref_day_type { get; set; }
        public virtual ref_shift ref_shift { get; set; }
    }
}