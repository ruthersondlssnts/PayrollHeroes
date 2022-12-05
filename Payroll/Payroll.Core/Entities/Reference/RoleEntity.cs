using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Core.Entities
{
   public class RoleEntity
    {
        public int role_id { get; set; }
        public string role_name { get; set; }
        public DateTime? date_deleted { get; set; }

        public object role_permission_ { get; set; }
        public IEnumerable<RolePermissionEntity> role_permission { get; set; }
    }
}
