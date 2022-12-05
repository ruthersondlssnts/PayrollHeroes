using System;
using System.Collections.Generic;
using System.Text;

namespace TPS.Infrastructure.Models
{
    [BsonCollection("refEmployeeRank")]
    public class RefEmployeeRank : Document
    {
        public string Grade { get; set; }
        public string Position { get; set; }
        public string MinimumPay { get; set; }
        public string MaximumPay { get; set; }
    }
}
