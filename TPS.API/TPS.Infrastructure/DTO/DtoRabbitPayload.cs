using System;
using System.Collections.Generic;
using System.Text;
using TPS.Infrastructure.Models;

namespace TPS.Infrastructure.DTO
{
    public class DtoRabbitPayload
    {
       public string PayrollCutoff { get; set; }
    }

    public class DTOPayload
    { 
        public Employee Employee { get; set; }
        public RefPayrollCutoff RefPayrollCutoff { get; set; }
    }
}
