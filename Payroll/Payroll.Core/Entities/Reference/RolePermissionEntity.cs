using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Core.Entities
{
   public class RolePermissionEntity
    {
        public int role_permission_id { get; set; }
        
        public int role_id { get; set; }
        public int permission_id { get; set; }
        public bool is_enable { get; set; }
        public PermissionEntity permission_ { get; set; }
        public string permission_name { get; set; }
    }
}
