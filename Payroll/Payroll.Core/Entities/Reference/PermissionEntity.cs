using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Core.Entities
{
   public class PermissionEntity
    {
        public int permission_id { get; set; }
        public string permission_name { get; set; }
        public string permission_code { get; set; }
        public bool is_enable { get; set; }
    }
}
