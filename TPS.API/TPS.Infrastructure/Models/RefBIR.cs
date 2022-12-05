namespace TPS.Infrastructure.Models
{
    [BsonCollection("refBIR")]
    public class RefBIR : Document
    {
        public string PayType { get; set; }
        public decimal SalaryFrom { get; set; }
        public decimal SalaryTo { get; set; }
        public decimal AddTax { get; set; }
        public decimal SubtractTaxOver { get; set; }
        public decimal Multiplier { get; set; }
    }
}
