using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;
using TPS.Infrastructure;
using TPS.Infrastructure.Enums;
using TPS.Infrastructure.Models;
using TPS.Services.Interfaces;

namespace TPS.Services.Services
{
    public class RefShiftService : IRefShiftService
    {
        private readonly IDBService<RefShift> _data;
        public RefShiftService(IDBService<RefShift> data)
        {
            _data = data;
        }

        public async Task<ApiResponse<StatusCode>> Create(RefShift entity)
        {
            var verifyDups = _data.FindOne(x => x.ShiftName == entity.ShiftName);

            if (verifyDups!= null)
            {
                return new ApiResponse<StatusCode>
                {
                    StatusCode = StatusCode.Conflict,
                    Message = "Shift name already exist"
                };
            }

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

        public async Task<ApiResponse<RefShift>> FindById(string id)
        {
            return new ApiResponse<RefShift>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString(),
                Result = _data.FindById(id)
            };
        }

        public ApiResponse<IEnumerable<RefShift>> GetAll()
        {
            return new ApiResponse<IEnumerable<RefShift>>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString(),
                Result = _data.FilterBy(x => x.DateDeleted == null)
            };
        }

        public async Task<ApiResponse<StatusCode>> Update(RefShift entity)
        {
            await _data.ReplaceOneAsync(entity);
            return new ApiResponse<StatusCode>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString()
            };
        }
    }
}
