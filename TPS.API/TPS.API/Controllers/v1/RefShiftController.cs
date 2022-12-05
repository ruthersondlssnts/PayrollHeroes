using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TPS.Infrastructure.Models;
using TPS.Services.Interfaces;
using TPS.Infrastructure.Enums;
namespace TPS.API.Controllers.v1
{
    [ApiVersion("1")]
    [ApiVersion("2")]
    [ApiController]
    [Route("api/v{version:apiVersion}/refshift")]
    public class RefShiftController : ControllerBase
    {
        private readonly IRefShiftService _service;
        public RefShiftController(IRefShiftService service)
        {
            _service = service;
        }

        /// <summary>
        /// This is a method to get all
        /// </summary>
        /// <returns>List</returns>
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpGet("getShift")]
        //[Authorize]
        public ActionResult GetShift(string id)
        {
            var result = _service.FindById(id).Result;
            switch (result.StatusCode)
            {
                case Infrastructure.Enums.StatusCode.Success:
                    return Ok(result);
                default:
                    return BadRequest(result);
            }
        }

        /// <summary>
        /// This is a method to get all
        /// </summary>
        /// <returns>List</returns>
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpGet]
        //[Authorize]
        public ActionResult Get()
        {
            var result = _service.GetAll();
            switch (result.StatusCode)
            {
                case Infrastructure.Enums.StatusCode.Success:
                    return Ok(result);
                default:
                    return BadRequest(result);
            }
        }

        /// <summary>
        /// This is a method to Create
        /// </summary>
        /// <param name="data"></param> 
        /// <response code="200">Returns success</response>
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPost]
        //[Authorize]
        public ActionResult Create(RefShift data)
        {
            var result = _service.Create(data).Result;
            switch (result.StatusCode)
            {
                case Infrastructure.Enums.StatusCode.Success:
                    return Ok(result);
                default:
                    return BadRequest(result);
            }
        }

        /// <summary>
        /// This is a method to Update
        /// </summary>
        /// <param name="data"></param> 
        [Consumes("application/json")]
        [HttpPut]
        //[Authorize]
        public ActionResult Update(RefShift data)
        {
            var result = _service.Update(data).Result;
            switch (result.StatusCode)
            {
                case Infrastructure.Enums.StatusCode.Success:
                    return Ok(result);
                default:
                    return BadRequest(result);
            }
        }

        /// <summary>
        /// This is a method to Delete
        /// </summary>
        /// <param name="id"></param> 
        [Consumes("application/json")]
        [HttpDelete]
        //[Authorize]
        public ActionResult Delete(string id)
        {
            var result = _service.Delete(id).Result;
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
