using System;
using System.Collections.Generic;
using System.Text;

namespace TPS.Infrastructure.Models
{
    [BsonCollection("tpsConfiguration")]
    public class TpsConfiguration : Document
    {
        public string ConfigurationName { get; set; }
        public string ConfigurationValue { get; set; }
    }
}
