namespace TPS.Frontend.Infrastructure
{
    public class RefLeaveType : Document
    {
        public string LeaveTypeName { get; set; }
        public string LeaveTypeCode { get; set; }
        public bool IsWithpay { get; set; }

        //Get from employee balance
        public bool IsCheckBalance { get; set; }
    }
}
