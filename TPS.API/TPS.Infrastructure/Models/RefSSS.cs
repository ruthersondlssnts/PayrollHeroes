using System;
using System.Collections.Generic;
using System.Text;

namespace TPS.Infrastructure.Models
{
    [BsonCollection("refSSS")]
    public class RefSSS : Document
    {
        public string Name { get; set; }
        public decimal SalaryFrom { get; set; }
        public decimal SalaryTo { get; set; }
        public decimal MonthlySalaryCredit { get; set; }
        public decimal EmployeeShare { get; set; }
        public decimal EmployerShare { get; set; }
    }
}
