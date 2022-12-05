using System;
using System.Collections.Generic;
using System.Text;

namespace TPS.Infrastructure.DTO
{
    public class DTODtrCalendar
    {
        public string Title { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public string Content { get; set; }
        public bool Absent { get; set; }
        public string Color { get; set; }
    }
}
