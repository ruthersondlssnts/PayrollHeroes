using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Payroll.Core.Entities;
using Payroll.Service;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Text.RegularExpressions;

namespace Payroll.Web.Controllers
{
    public class EmployeeTimesheetController : Controller
    {
        EmployeeTimesheetService repo;
        RefPayrollCutoffService repopaycut;
        RefGroupService reporefgroup;
        private IHostingEnvironment _hostingEnvironment;
        public IActionResult Index()
        {
            //Get Session data     
            return View();
        }

        public IActionResult TimesheetReport()
        {

            return View();
        }

        public EmployeeTimesheetController(IHostingEnvironment hostingEnvironment)
        {
            repo = new EmployeeTimesheetService();
            repopaycut = new RefPayrollCutoffService();
            reporefgroup = new RefGroupService();
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        public JsonResult GetData(int id)
        {
            int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid).Value);
            var result = repo.GetAllEmployeeTimeSheet(UserId, id);
            return Json(result);
        }

        //id is group_id
        [HttpGet]
        public JsonResult GetAllEmployeeTimeSheetDTR( int group_id,  int ref_payroll_cutoff_id)
        {
            int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid).Value);
            var result = repo.GetAllEmployeeTimeSheetDTR(group_id, ref_payroll_cutoff_id);

            return Json(result);
        }

        [HttpGet]
        public JsonResult GetGroup(int level,int group_id)
        {
            var result = reporefgroup.GetAllList();
            if (level == 1)
            {
                result = result.Where(a => a.current_level == level);
            }

            if (level == 2)
            {
                result = result.Where(a => a.current_level == level && a.ancestor1==group_id);
            }

            if (level == 3)
            {
                result = result.Where(a => a.current_level == level && a.ancestor1 == group_id);
            }
            result.Select(x => new
            {
                group_id = x.group_id,
                name = x.name
            }).ToList();
            return Json(result);

        }

        [HttpGet]
        public JsonResult GetDataSum(int id)
        {
            int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid).Value);
            var result = repo.GetAllEmployeeTimeSheetSummarized(UserId, id);
            List<EmployeeTimesheetEntity> objReturn = new List<EmployeeTimesheetEntity>();
            EmployeeTimesheetEntity objNew =new EmployeeTimesheetEntity();
            objNew = result;

            objReturn.Add(objNew);
            return Json(objReturn);
        }

        public IActionResult Process()
        {
            return View();
        }
        [HttpGet]
        public JsonResult ProcessTimesheet(int id)
        {
            int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid).Value);
            var result = repo.ProcessTimesheet(UserId, id);
            if (result)
            { return Json("Success"); }
            else
            { return Json("Failed"); }
            
        }

        public IActionResult InsertInOut()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetbyID(int id)
        {
            var result = repo.GetByID(id);
            return Json(result);
        }
        [HttpPost]
        public JsonResult UpdateTimesheet([FromBody] EmployeeTimesheetTemp emp)
        {
            var data = repo.UpdateTimesheet(emp);
            return Json("");
        }
        [HttpGet]
        public JsonResult ProcessAllEmployee(int id)
        {
            var result = repo.ProcessTimesheet(id);
            if (result)
            { return Json("Success"); }
            else
            { return Json("Failed"); }
        }
        [HttpGet]
        public JsonResult GenerateAllEmployee(int id)
        {
            var result = repo.GenerateNewTimesheet(id);
            if (result)
            { return Json("Success"); }
            else
            { return Json("Failed"); }
        }

        [HttpGet]
        public JsonResult GetCutoff()
        {
            var result = repopaycut.
            GetList().Select(x => new
            {
                ref_payroll_cutoff_id = x.ref_payroll_cutoff_id,
                ref_payroll_cutoff_name = x.cutoff_date_start.ToString("MM/dd/yyyy") + " - " + x.cutoff_date_end.ToString("MM/dd/yyyy")
            }).ToList();
            return Json(result);
        }

        public ActionResult OnPostUpload(List<IFormFile> files)
        {
            string folderName = "Upload";
            string webRootPath = _hostingEnvironment.WebRootPath;
            string newPath = Path.Combine(webRootPath, folderName);
            //StringBuilder sb = new StringBuilder();
            if (files != null && files.Count > 0)
            {
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                foreach (IFormFile item in files)
                {
                    if (item.Length > 0)
                    {
                        ISheet sheet;
                        string errormessage = "";
                        string fileName = ContentDispositionHeaderValue.Parse(item.ContentDisposition).FileName.Trim('"');
                        string fullPath = Path.Combine(newPath, fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            item.CopyTo(stream);
                            stream.Position = 0;
                            if (fileName.ToLower().Contains(".xls"))
                            {
                                XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read the Excel 97-2000 formats  
                                sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook  
                            }
                            else
                            {
                                XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format  
                                sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
                            }
                            IRow headerRow = sheet.GetRow(0); //Get Header Row
                            int cellCount = headerRow.LastCellNum;
                          

                            //GET THE HEADER
                            for (int j = 0; j < cellCount; j++)
                            {
                                NPOI.SS.UserModel.ICell cell = headerRow.GetCell(j);
                                if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                               
                            }

                            List<EmployeeTimesheetTempDevice> deviceList = new List<EmployeeTimesheetTempDevice>();
                            //Get THE DATA
                            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
                            {
                                IRow row = sheet.GetRow(i);
                                if (row == null) continue;
                                if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                                //for (int j = row.FirstCellNum; j < cellCount; j++)
                                //{

                                //}
                                string rowError = "";
                                if (!ValidateDate(row.GetCell(1).ToString()))
                                {
                                    rowError += "Invalid date| ";
                                }
                                if (!ValidateTime(row.GetCell(2).ToString()))
                                {
                                    rowError += "Invalid TimeIn| ";
                                }
                                if (!ValidateTime(row.GetCell(3).ToString()))
                                {
                                    rowError += "Invalid TimeOut| ";
                                }

                                if (rowError.Length > 0)
                                {
                                    rowError = rowError.Remove(rowError.Length - 2);
                                    errormessage += "Error on line: " + (i+1) + " (" + rowError + ")<br/>";
                                }
                                else
                                {
                                    EmployeeTimesheetTempDevice newRec = new EmployeeTimesheetTempDevice();
                                    newRec.empid = row.GetCell(0).ToString();
                                    newRec.shiftDate = DateTime.Parse(row.GetCell(1).ToString());
                                    newRec.timein = row.GetCell(2).ToString();
                                    newRec.timeout = row.GetCell(3).ToString();
                                    deviceList.Add(newRec);
                                }

                            }
                            
                        }
                        if (errormessage.Length > 0)
                        {
                            return Json(new { status = false, message ="<br/>"+ errormessage });
                        }
                        else
                        {
                            return Json(new { status = true, message = "Success!" });
                        }
                    }
                }
                
            }
            return Json(new { status = false, message = "File not exist" });
        }

        public bool ValidateDate(string dt)
        {
            try
            {

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ValidateTime(string thetime)
        {
            Regex checktime = new Regex("^(?:0?[0-9]|1[0-9]|2[0-9]|3[0-9]|4[0-8]):[0-5][0-9]$");
            //VALID: 48:59
            if (checktime.IsMatch(thetime))
            {
                return true;
            }

            return false;
        }

        public IActionResult ExportDTR(int group_id, int ref_payroll_cutoff_id) {

            var result = repo.GetAllEmployeeTimeSheetDTR(group_id, ref_payroll_cutoff_id)
                .Select(a=> new {
                    a.last_name,
                    a.first_name,
                    a.shift_date,
                    a.shift_name,
                    a.date_type_code,
                    a.required_hour,
                    a.rendered_hour,
                    time_in1  ="=\"\"" + a.time_in1 + "\"\"",
                    time_out1 = "=\"\"" + a.time_out1 + "\"\"",
                    ot_in = "=\"\"" + a.ot_in + "\"\"",
                    ot_out = "=\"\"" + a.ot_out + "\"\"",
                    a.late,
                    a.undertime,
                    a.ot,
                    a.ot8,
                    a.night_dif,
                    a.absent

                });

            string csv = ListToCSV(result.ToList(), "claim");
            string currentDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "ExportDTR_" + currentDate + ".csv");
        }

        private string ListToCSV<T>(IEnumerable<T> list, string report)
        {
            StringBuilder sList = new StringBuilder();

            Type type = typeof(T);
            var props = type.GetProperties();

            //GET COLUMNS
            sList.Append("\"Item\",");
            sList.Append("\"");

            sList.Append(string.Join("\",\"", props.Select(p => p.Name)));
            sList.Append("\"");

            if (report == "dtr")
            {
                sList = ReplaceColumn(sList);
            }

            sList.Append(Environment.NewLine);
            int intCount = 1;
            foreach (var element in list)
            {
                sList.Append("\"" + intCount + "\",");
                sList.Append("\"");
                sList.Append(string.Join("\",\"", props.Select(p => p.GetValue(element, null))));
                sList.Append("\"");
                sList.Append(Environment.NewLine);
                intCount++;
            }



            return sList.ToString();
        }

        public StringBuilder ReplaceColumn(StringBuilder val)
        {
            val.Replace("\"last_name\"", "\"Last Name\"");
            val.Replace("\"first_name\"", "\"First Name\"");
            val.Replace("\"shift_date\"", "\"Date\"");
            val.Replace("\"shift_name\"", "\"Shift\"");
            val.Replace("\"date_type_code\"", "\"Type\"");
            val.Replace("\"required_hour\"", "\"Required Hr\"");
            val.Replace("\"rendered_hour\"", "\"Rendered Hr\"");
            val.Replace("\"time_in1\"", "\"In\"");
            val.Replace("\"time_out1\"", "\"Out\"");
            val.Replace("\"ot_in\"", "\"OtIn\"");
            val.Replace("\"ot_out\"", "\"OtOut\"");
            val.Replace("\"late\"", "\"Late\"");
            val.Replace("\"undertime\"", "\"Undertime\"");
            val.Replace("\"ot\"", "\"OT\"");
            val.Replace("\"ot8\"", "\"OT8\"");
            val.Replace("\"night_dif\"", "\"Night Diff\"");
            val.Replace("\"absent\"", "\"Absent\"");


            return val;
        }
    }
}