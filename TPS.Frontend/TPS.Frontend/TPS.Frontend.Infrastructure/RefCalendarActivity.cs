using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TPS.Frontend.Infrastructure
{
    public class RefCalendarActivity : Document
    {
        public DateTime WorkDate { get; set; }
        public string description { get; set; }
        public int HolidayTypeId { get; set; }
        public string HolidayTypeName { get; set; }
    }
}
