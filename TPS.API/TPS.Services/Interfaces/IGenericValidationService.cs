using System;
using System.Collections.Generic;
using System.Text;

namespace TPS.Services.Interfaces
{
    public interface IGenericValidationService
    {
        bool Email(string value);
        bool EmptyOrNull(string value);
        bool DateTime(string value);
    }
}
