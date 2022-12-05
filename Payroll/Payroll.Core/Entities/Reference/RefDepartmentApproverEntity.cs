using System;
using System.Collections.Generic;

namespace Payroll.Core.Entities
{
    public class RefDepartmentApproverEntity
    {

        public int ref_department_approver_id { get; set; }
        public int ref_department_id { get; set; }
        public int employee_id { get; set; }
        public int ordering { get; set; }
        public DateTime? date_deleted { get; set; }

        public EmployeeEntity employee_ { get; set; }
        public RefDepartmentEntity ref_department_ { get; set; }
    }
}
