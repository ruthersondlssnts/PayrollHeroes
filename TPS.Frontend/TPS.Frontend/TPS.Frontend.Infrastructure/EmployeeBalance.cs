using System;

namespace TPS.Frontend.Infrastructure
{
    public class EmployeeBalance : Document
    {
        public string EmployeeId { get; set; }
        public string FullName { get; set; }
        public string LeaveTypeId { get; set; }
        public string LeaveTypeName { get; set; }
        public DateTime AcquireDate { get; set; }
        public DateTime? ExpireDate { get; set; }
        public decimal Quantity { get; set; }
        public string Remarks { get; set; }
    }
}
