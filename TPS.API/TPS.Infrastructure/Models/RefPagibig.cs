using System;
using System.Collections.Generic;
using System.Text;

namespace TPS.Infrastructure.Models
{
    [BsonCollection("refPagibig")]
    public class RefPagibig : Document
    {
        public string Name { get; set; }
        public decimal SalaryFrom { get; set; }
        public decimal SalaryTo { get; set; }
        public decimal EmployeeContribution { get; set; }
        public decimal EmployerContribution { get; set; }
        public bool FlatRate { get; set; }

    }
}
