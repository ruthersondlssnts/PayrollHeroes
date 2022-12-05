using Microsoft.AspNetCore.Mvc;
using TPS.Infrastructure.Models;
using TPS.Services.Interfaces;

namespace TPS.API.Controllers.v1
{
    [ApiVersion("1")]
    [ApiVersion("2")]
    [ApiController]
    [Route("api/v{version:apiVersion}/refbir")]
    public class RefBIRController : ControllerBase
    {
        private readonly IRefBIRService _service;
        public RefBIRController(IRefBIRService service)
        {
            _service = service;
        }

        /// <summary>
        /// This is a method to get all
        /// </summary>
        /// <returns>List</returns>
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpGet("getBIR")]
        //[Authorize]
        public ActionResult GetBIR(string id)
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
        public ActionResult Create(RefBIR data)
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
        public ActionResult Update(RefBIR data)
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
