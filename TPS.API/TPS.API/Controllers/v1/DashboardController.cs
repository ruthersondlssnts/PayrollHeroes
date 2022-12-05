using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TPS.Services.Interfaces;

namespace TPS.API.Controllers.v1
{
    [ApiVersion("1")]
    [ApiVersion("2")]
    [ApiController]
    [Route("api/v{version:apiVersion}/dashboard")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _service;
        public DashboardController(IDashboardService service)
        {
            _service = service;
        }

        /// <summary>
        /// This is a method to get all for approval
        /// </summary>
        /// <returns>List</returns>
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpGet]
        public ActionResult ForApprovals(string employeeid)
        {
            var result = _service.ForApprovals(employeeid).Result;
            switch (result.StatusCode)
            {
                case Infrastructure.Enums.StatusCode.Success:
                    return Ok(result);
                default:
                    return BadRequest(result);
            }
        }
    }
}
