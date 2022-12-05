using Microsoft.SqlServer.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payroll.Core.Entities
{
    public partial class RefGroupEntity
    {
        public int group_id { get; set; }
        public string name { get; set; }

        public SqlHierarchyId level { get; set; }

        public bool is_enable { get; set; }
        public DateTime? date_deleted { get; set; }

        [NotMapped]
        public int currentval { get; set; }

        [NotMapped]
        public int current_level { get; set; }

        [NotMapped]
        public int? ancestor1 { get; set; }

        [NotMapped]
        public int? ancestor2 { get; set; }

        [NotMapped]
        public int? nextval { get; set; }
    }
}
