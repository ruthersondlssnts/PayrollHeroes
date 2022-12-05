using Newtonsoft.Json;
using System;
using System.Net;
using TPS.Infrastructure.Enums;

namespace TPS.Infrastructure
{
    public class ApiResponse<T>
    {
        public StatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
    }
}
