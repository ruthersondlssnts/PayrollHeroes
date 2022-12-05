using System;
using System.Collections.Generic;
using System.Text;

namespace TPS.Infrastructure.Enums
{
    public enum StatusCode
    {
        Success =  200,
        Created = 201,
        Bad_Request = 400,
        Conflict = 409,



        //Request Validation status
        DUPLICATE = 7000,


    }
}
