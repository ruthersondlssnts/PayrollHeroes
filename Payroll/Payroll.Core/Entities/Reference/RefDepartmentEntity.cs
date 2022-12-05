using System;
using System.Collections.Generic;

namespace Payroll.Core.Entities
{
    public class RefDepartmentEntity
    {

        public int ref_department_id { get; set; }
        public string department_name { get; set; }
        public object ref_department_approver_ { get; set; }
        public IEnumerable<RefDepartmentApproverEntity> ref_department_approver { get; set; }
    }
}
