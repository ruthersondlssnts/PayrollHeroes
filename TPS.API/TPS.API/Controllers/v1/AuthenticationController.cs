using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TPS.Infrastructure.DTO;
using TPS.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TPS.API.Controllers.v1
{
    [ApiVersion("1")]
    [ApiVersion("2")]
    [ApiController]
    [Route("api/v{version:apiVersion}/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _service;
        public AuthenticationController(IAuthenticationService service)
        {
            _service = service;
        }

        /// <summary>
        /// Login for authentication
        /// </summary>
        /// <returns>Authentication response</returns>
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPost("login")]
        [MapToApiVersion("1")]
        public ActionResult Login(DTOAuthentication parameter)
        {
            var data = _service.Login(parameter);
            return Ok(data);

        }

        /// <summary>
        /// verify token
        /// </summary>
        /// <returns>Authentication token response</returns>
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPost("verifyToken")]
        [MapToApiVersion("1")]
        public ActionResult<DTOAuthenticationResponse> VerifyToken(DTOTokenVerification parameter)
        {
            var data = _service.VerifyToken(parameter);
            return Ok(data);
        }
    }
}
