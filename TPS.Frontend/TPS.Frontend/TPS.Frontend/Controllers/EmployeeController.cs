using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using TPS.Frontend.Infrastructure;
using TPS.Frontend.Infrastructure.Pagination;
using TPS.Frontend.Models;
using TPS.Frontend.Services.Interface;

namespace TPS.Frontend.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private readonly IEmployeeService _client;
        public EmployeeController(IEmployeeService client)
        {
            _client = client;
        }

        public async Task<IActionResult> GetEmployees()
        {
            //Server Side Parameter
            var draw = Request.Form["draw"].FirstOrDefault(); //The Number of times the API is called for the current datatable.
            var start = Request.Form["start"].FirstOrDefault(); //The count of records to skip. This will be used while Paging in EFCore
            var length = Request.Form["length"].FirstOrDefault(); //Essentially the page size. You can see the Top Dropdown in the Jquery Datatable that says, ‘Showing n entries’. n is the page size.
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault(); //The Column that is set for sorting
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();//Ascending / Descending
            var searchValue = Request.Form["search[value]"].FirstOrDefault(); //The Value from the Search Box

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int pageNumber = skip / pageSize + 1;

            PaginatedEmployees paginatedEmp = new PaginatedEmployees
            {
                OrderByASCOrDESC = sortColumnDirection,
                OrderByColumn = sortColumn,
                SearchKeyword = searchValue,
                PageSize = pageSize,
                PageNumber = pageNumber
            };


            var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var response = await _client.GetEmployeesAsync(_cancellationTokenSource.Token, accessToken, paginatedEmp);
            switch (response.StatusCode)
            {
                case Infrastructure.StatusCode.Success:
                    return Json(new { draw = draw, recordsFiltered = response.TotalRecord, recordsTotal = response.TotalRecord, data = response.DataList });
                default:
                    return Json(new { draw = draw, recordsFiltered = 0, recordsTotal = 0, data = "" });
            }

        }
        public async Task<IActionResult> GetFullNames()
        {
            var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var response = await _client.GetFullNamesAsync(_cancellationTokenSource.Token, accessToken);
            switch (response.StatusCode)
            {
                case Infrastructure.StatusCode.Success:
                    return Json(response.Result);
                default:
                    return BadRequest(response);
            }

        }
        public async Task<IActionResult> GetEmployeePhotoPath()
        {
            var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var response = await _client.FindEmployeeAsync(_cancellationTokenSource.Token, accessToken, userId);
            switch (response.StatusCode)
            {
                case Infrastructure.StatusCode.Success:
                    return Json(response.Result.PhotoPath);
                default:
                    return BadRequest(response);
            }

        }
        // GET: EmployeeController
        public ActionResult Index()
        {
            return View();
        }



        // GET: EmployeeController/AddOrEdit
        public async Task<IActionResult> AddOrEdit(string id = "")
        {
            var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;

            if (id == "")
                return View(new Employee());
            else
            {
                var response = await _client.FindEmployeeAsync(_cancellationTokenSource.Token, accessToken, id);
                switch (response.StatusCode)
                {
                    case Infrastructure.StatusCode.Success:
                        if (response.Result == null)
                            return NotFound();
                        return View(response.Result);
                    default:
                        return BadRequest(response);
                }
            }
        }

        // POST: EmployeeController/AddOrEdit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(string id, Employee model)
        {
            if (model.Photo != null && model.Photo.Length > 0)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Photo.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\employeephotos", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Photo.CopyToAsync(fileStream);
                }
                model.PhotoPath = fileName;
            }

            var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            var response = await _client.AddOrEditEmployeeAsync(_cancellationTokenSource.Token, accessToken, model);

            switch (response.StatusCode)
            {
                case Infrastructure.StatusCode.Success:
                    return Json(new { isValid = true });
                case Infrastructure.StatusCode.Conflict:
                case Infrastructure.StatusCode.DUPLICATE:
                    return Json(new { isValid = false, html = Helper.Helper.RenderRazorViewToString(this, "AddOrEdit", model), error = response.Message });
                default:
                    return BadRequest(response);
            }
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var accessToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value;

                var response = await _client.DeleteEmployeeAsync(_cancellationTokenSource.Token, accessToken, id);
                switch (response.StatusCode)
                {
                    case Infrastructure.StatusCode.Success:
                        return RedirectToAction(nameof(Index));
                    default:
                        return BadRequest(response);
                }

            }
            catch
            {
                return View();
            }
        }
    }
}
