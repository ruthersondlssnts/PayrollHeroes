using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TPS.Infrastructure.Models;
using TPS.Services.Interfaces;

namespace TPS.API.Controllers.v1
{
    [ApiVersion("1")]
    [ApiVersion("2")]
    [ApiController]
    [Route("api/v{version:apiVersion}/requestChangeShift")]
    public class RequestChangeShiftController : ControllerBase
    {
        private readonly IRequestChangeShiftService _service;
        public RequestChangeShiftController(IRequestChangeShiftService service)
        {
            _service = service;
        }

        /// <summary>
        /// This is a method to get all
        /// </summary>
        /// <returns>List</returns>
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpGet("getRequestChangeShift")]
        //[Authorize]
        public ActionResult GetRequestChangeShift(string id)
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
        [HttpGet("getByUser")]
        //[Authorize]
        public ActionResult GetByUser(string userId)
        {
            var result = _service.GetAllByUserId(userId);
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
        public ActionResult Create(RequestChangeShift data)
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
        public ActionResult Update(RequestChangeShift data)
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
