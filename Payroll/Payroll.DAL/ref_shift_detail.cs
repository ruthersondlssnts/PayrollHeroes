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
    
    public partial class ref_shift_detail
    {
        public int ref_shift_detail_id { get; set; }
        public int ref_shift_id { get; set; }
        public string day { get; set; }
        public Nullable<decimal> required_hour { get; set; }
        public bool rest_day { get; set; }
    
        public virtual ref_shift ref_shift { get; set; }
    }
}
