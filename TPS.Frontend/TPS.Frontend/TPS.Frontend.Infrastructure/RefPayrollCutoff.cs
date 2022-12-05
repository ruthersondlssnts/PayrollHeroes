using System;

namespace TPS.Frontend.Infrastructure
{
    public class RefPayrollCutoff : Document
    {
        public string Name { get; set; }
        public DateTime PayrollDate { get; set; }
        public DateTime? CutoffStartDate { get; set; }
        public DateTime? CutoffEndDate { get; set; }
        public bool IsProcessed { get; set; }
        public bool IsProcessGovernment { get; set; }
        public int CutoffCountPerMonth { get; set; }
    }
}
