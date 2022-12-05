using Microsoft.SqlServer.Types;
using System;
using System.Collections.Generic;

namespace Payroll.Infrastructure.Models
{
    public partial class ref_group_dto
    {
        public int group_id { get; set; }
        public string name { get; set; }

        public SqlHierarchyId level { get; set; }
        public bool is_enable { get; set; }
        public DateTime? date_deleted { get; set; }
        public Int16 current_level { get; set; }
        public int? ancestor1 { get; set; }
        public int? ancestor2 { get; set; }
        public int? nextval { get; set; }
    }
}
