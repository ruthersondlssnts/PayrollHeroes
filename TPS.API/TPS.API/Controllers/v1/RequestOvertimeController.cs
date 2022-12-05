using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TPS.Infrastructure.DTO;
using TPS.Infrastructure.Models;
using TPS.Infrastructure.Pagination;
using TPS.Services.Interfaces;

namespace TPS.API.Controllers.v1
{
    [ApiVersion("1")]
    [ApiVersion("2")]
    [ApiController]
    [Route("api/v{version:apiVersion}/requestovertime")]
    public class RequestOvertimeController : ControllerBase
    {
        private readonly IRequestOvertimeService _service;
        public RequestOvertimeController(IRequestOvertimeService service)
        {
            _service = service;
        }

        /// <summary>
        /// This is a method to get all
        /// </summary>
        /// <returns>List</returns>
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpGet("getRequestOvertime")]
        //[Authorize]
        public ActionResult GetRequestOvertime(string id)
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
        public ActionResult Create(RequestOvertime data)
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
        public ActionResult Update(RequestOvertime data)
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

        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpGet("getAllByApproverId")]
        //[Authorize]
        public ActionResult GetAllByApproverId(string userId)
        {
            var result = _service.GetAllByApproverId(userId);
            switch (result.StatusCode)
            {
                case Infrastructure.Enums.StatusCode.Success:
                    return Ok(result);
                default:
                    return BadRequest(result);
            }
        }

        /// <summary>
        /// This is a method to Approvaal
        /// </summary>
        /// <param name="data"></param> 
        /// <response code="200">Returns success</response>
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPost("processApproval")]
        //[Authorize]
        public ActionResult ProcessApproval(DTOApproval data)
        {
            var result = _service.ProcessApproval(data.Id, data.ApproverUserId, data.Remarks,data.ApprovalStatus).Result;
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
        [HttpGet("getPaginatedList")]
        //[Authorize]
        public ActionResult GetPaginatedList([FromQuery] PaginatedRequest param)
        {

            var result = _service.GetPaginatedList(param);
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
