using System.Collections.Generic;
using System.Threading.Tasks;
using TPS.Infrastructure;
using TPS.Infrastructure.Enums;
using TPS.Infrastructure.Models;
using TPS.Services.Interfaces;

namespace TPS.Services.Services
{
    public class TpsConfigurationService : ITpsConfigurationService
    {
        private readonly IDBService<TpsConfiguration> _data;
        public TpsConfigurationService(IDBService<TpsConfiguration> data)
        {
            _data = data;
        }

        public async Task<ApiResponse<StatusCode>> Create(TpsConfiguration entity)
        {
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

        public async Task<ApiResponse<TpsConfiguration>> FindById(string id)
        {
            return new ApiResponse<TpsConfiguration>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString(),
                Result = _data.FindById(id)
            };
        }

        public ApiResponse<IEnumerable<TpsConfiguration>> GetAll()
        {
            return new ApiResponse<IEnumerable<TpsConfiguration>>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString(),
                Result = _data.FilterBy(x => x.DateDeleted == null)
            };
        }

        public async Task<ApiResponse<StatusCode>> Update(TpsConfiguration entity)
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
