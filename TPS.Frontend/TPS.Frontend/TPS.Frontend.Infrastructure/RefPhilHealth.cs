namespace TPS.Frontend.Infrastructure
{
    public class RefPhilHealth : Document
    {
        public string Name { get; set; }
        public decimal SalaryFrom { get; set; }
        public decimal SalaryTo { get; set; }
        public decimal EmployeeContribution { get; set; }
        public decimal EmployerContribution { get; set; }
        public bool FlatRate { get; set; }
    }

}
