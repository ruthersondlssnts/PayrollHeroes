using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Core.Entities
{ 
    public partial class RefEmployeeTypeEntity
    {
        public int ref_employee_type_id { get; set; }
        public string ref_employee_type_name { get; set; }
        public bool? date_deleted { get; set; }

        public ICollection<EmployeeEntity> employee { get; set; }
    }
}
