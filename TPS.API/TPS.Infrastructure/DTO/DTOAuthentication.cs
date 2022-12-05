using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace TPS.Infrastructure.DTO
{
    public class DTOAuthentication
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class DTOTokenVerification
    {
        public string Username { get; set; }
        public string Token { get; set; }
    }

    public class DTOTokenResponse
    {
        public string Token { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public string Message { get; set; }
    }

    public class DTOAuthenticationResponse
    {
        public string Username { get; set; }
        public Guid UserId { get; set; }
        public string Position { get; set; }
        public string Roles { get; set; }
        public string Token { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }

    }
}
