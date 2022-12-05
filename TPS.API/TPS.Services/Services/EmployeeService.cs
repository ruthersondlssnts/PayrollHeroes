using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPS.Infrastructure;
using TPS.Infrastructure.Enums;
using TPS.Infrastructure.Models;
using TPS.Infrastructure.Pagination;
using TPS.Services.Interfaces;
using System.Linq.Dynamic.Core;
using TPS.Infrastructure.DTO;

namespace TPS.Services.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IDBService<Employee> _data;
        private readonly IDBService<RefShift> _dataShift;
        private readonly ICommonService _common;

        public EmployeeService(IDBService<Employee> data, ConfigurationSettings configuration, ICommonService common, IDBService<RefShift> dataShift)
        {
            _data = data;
            _common = common;
            _dataShift = dataShift;
        }

        public async Task<ApiResponse<StatusCode>> Create(Employee entity)
        {
            string ErrorMessage = "";

            if (string.IsNullOrEmpty(entity.Password))
            {
                ErrorMessage += "<li>Password is required</li>";
            }

            if (!string.IsNullOrEmpty(entity.EmployeeSerial))
            {
                var checkDuplicateEmployeeSerial = _data.FindOne(x => x.EmployeeSerial == entity.EmployeeSerial && x.DateDeleted == null);
                if (checkDuplicateEmployeeSerial != null)
                {
                    ErrorMessage += "<li>Duplicate employee serial</li>";
                }
            }


            var checkDuplicateElectronicId = _data.FindOne(x => x.ElectronicId == entity.ElectronicId && x.DateDeleted == null);
            if (checkDuplicateElectronicId != null)
            {
                ErrorMessage += "<li>Duplicate Bio Id</li>";
            }

            if (string.IsNullOrEmpty(entity.Sss) || string.IsNullOrEmpty(entity.Pagibig) || string.IsNullOrEmpty(entity.Philhealth) || string.IsNullOrEmpty(entity.TaxNumber))
            {
                ErrorMessage += "<li>Fill-up all government number</li>";
            }

            if (ErrorMessage.Length > 0)
            {
                return new ApiResponse<StatusCode>
                {
                    StatusCode = StatusCode.Conflict,
                    Message = ErrorMessage
                };
            }

            entity.Password = _common.EncryptString(entity.Password);

            var getShift = _dataShift.FindOne(x => x.ShiftName == entity.RefShift.ShiftName);

            entity.RefShift.ShiftIn = getShift.ShiftIn;
            entity.RefShift.ShiftOut = getShift.ShiftOut;
            entity.RefShift.BreakIn = getShift.BreakIn;
            entity.RefShift.BreakOut = getShift.BreakOut;
            entity.RefShift.BreakHour = getShift.BreakHour;
            entity.RefShift.NDStart = getShift.NDStart;
            entity.RefShift.NDEnd = getShift.NDEnd;
            entity.RefShift.NDStart2 = getShift.NDStart2;
            entity.RefShift.NDEnd2 = getShift.NDEnd2;
            entity.RefShift.GracePeriod = getShift.GracePeriod;
            entity.RefShift.IncludeGracePeriod = getShift.IncludeGracePeriod;
            entity.RefShift.IsAutoOvertime = getShift.IsAutoOvertime;
            entity.RefShift.IsNightDiff = getShift.IsNightDiff;
            entity.RefShift.RefShiftDetails = getShift.RefShiftDetails;

            await _data.InsertOneAsync(entity);
            return new ApiResponse<StatusCode>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString()
            };
        }

        public async Task<ApiResponse<StatusCode>> Delete(string id)
        {
            await _data.DeleteOneAsync(id);
            return new ApiResponse<StatusCode>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString()
            };
        }

        public async Task<ApiResponse<Employee>> FindById(string id)
        {

            var data = _data.FindById(id);
            data.Password = _common.DecryptStirng(data.Password);
            return new ApiResponse<Employee>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString(),
                Result = data
            };
        }

        public ApiResponse<IEnumerable<Employee>> GetAll()
        {
            return new ApiResponse<IEnumerable<Employee>>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString(),
                Result = _data.FilterBy(x => x.DateDeleted == null)
            };
        }

        public ApiResponse<IEnumerable<DTOEmployee>> GetFullNames()
        {
            return new ApiResponse<IEnumerable<DTOEmployee>>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString(),
                Result = _data.FilterBy(x => x.DateDeleted == null)
                              .Select(e => new DTOEmployee { FullName = e.FirstName + " " + e.LastName, Id = e.Id.ToString(), Roles = e.Roles })
            };
        }

        public PaginatedReturn<Employee> GetEmployees(PaginatedEmployees param)
        {
            var returnObject = new PaginatedReturn<Employee>();

            // default sort column and type
            returnObject.OrderByColumn = param.OrderByColumn ?? "FirstName";
            returnObject.OrderByASCOrDESC = param.OrderByASCOrDESC ?? "ASC";
            returnObject.CurrentPage = param.PageNumber;

            var data = _data.FilterBy(x => x.DateDeleted == null).ToList();

            // filter by search keyword
            if (!String.IsNullOrWhiteSpace(param.SearchKeyword))
            {
                var filterdata = data.Where(a => a.LastName.ToUpper().Contains(param.SearchKeyword.ToUpper()) || a.FirstName.ToUpper().Contains(param.SearchKeyword.ToUpper())).ToList();

                if (filterdata.Count() > 0)
                {
                    data = filterdata;
                }
                else
                {
                    return returnObject;
                }
            }

            returnObject.TotalRecord = data.Count();
            returnObject.TotalPage = (int)Math.Ceiling((decimal)returnObject.TotalRecord / (decimal)param.PageSize);
            data = data.AsQueryable().OrderBy(returnObject.OrderByColumn + " " + returnObject.OrderByASCOrDESC.ToUpper())
                .Skip((param.PageNumber - 1) * param.PageSize)
                .Take(param.PageSize)
                .ToList();

            returnObject.DataList = data;
            returnObject.StatusCode = StatusCode.Success;
            returnObject.Message = StatusCode.Success.ToString();

            return returnObject;
        }

        public async Task<ApiResponse<StatusCode>> Update(Employee entity)
        {
            string ErrorMessage = "";
            if (string.IsNullOrEmpty(entity.Password))
            {
                ErrorMessage += "<li>Password is required</li>";
            }

            entity.Password = _common.EncryptString(entity.Password);


            if (!string.IsNullOrEmpty(entity.EmployeeSerial))
            {
                var checkDuplicateEmployeeSerial = _data.FindOne(x => x.EmployeeSerial == entity.EmployeeSerial && x.DateDeleted == null && entity.Id != x.Id);
                if (checkDuplicateEmployeeSerial != null)
                {
                    ErrorMessage += "<li>Duplicate employee serial</li>";
                }
            }


            var checkDuplicateElectronicId = _data.FindOne(x => x.ElectronicId == entity.ElectronicId && x.DateDeleted == null && entity.Id != x.Id);
            if (checkDuplicateElectronicId != null)
            {
                ErrorMessage += "<li>Duplicate Bio Id</li>";
            }

            if (string.IsNullOrEmpty(entity.Sss) || string.IsNullOrEmpty(entity.Pagibig) || string.IsNullOrEmpty(entity.Philhealth) || string.IsNullOrEmpty(entity.TaxNumber))
            {
                ErrorMessage += "<li>Fill-up all government number</li>";
            }

            if (ErrorMessage.Length > 0)
            {
                return new ApiResponse<StatusCode>
                {
                    StatusCode = StatusCode.Conflict,
                    Message = ErrorMessage
                };
            }

            var getShift = _dataShift.FindOne(x => x.ShiftName == entity.RefShift.ShiftName);

            entity.RefShift.ShiftIn = getShift.ShiftIn;
            entity.RefShift.ShiftOut = getShift.ShiftOut;
            entity.RefShift.BreakIn = getShift.BreakIn;
            entity.RefShift.BreakOut = getShift.BreakOut;
            entity.RefShift.BreakHour = getShift.BreakHour;
            entity.RefShift.NDStart = getShift.NDStart;
            entity.RefShift.NDEnd = getShift.NDEnd;
            entity.RefShift.NDStart2 = getShift.NDStart2;
            entity.RefShift.NDEnd2 = getShift.NDEnd2;
            entity.RefShift.GracePeriod = getShift.GracePeriod;
            entity.RefShift.IncludeGracePeriod = getShift.IncludeGracePeriod;
            entity.RefShift.IsAutoOvertime = getShift.IsAutoOvertime;
            entity.RefShift.IsNightDiff = getShift.IsNightDiff;
            entity.RefShift.RefShiftDetails = getShift.RefShiftDetails;

            await _data.ReplaceOneAsync(entity);
            return new ApiResponse<StatusCode>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString()
            };
        }

    }
}

