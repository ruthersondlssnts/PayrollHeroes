using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TPS.Infrastructure;
using TPS.Infrastructure.Enums;
using TPS.Infrastructure.Models;
using TPS.Services.Interfaces;

namespace TPS.Services.Services
{
    public class BiometricsService : IBiometricsService
    {
        private readonly IDBService<BiometricsData> _data;
        public BiometricsService(IDBService<BiometricsData> data)
        {
            _data = data;
        }

        public async Task<ApiResponse<StatusCode>> Create(BiometricsData entity)
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

        public async Task<ApiResponse<BiometricsData>> FindById(string id)
        {
            return new ApiResponse<BiometricsData>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString(),
                Result = _data.FindById(id)
            };
        }



        public ApiResponse<IEnumerable<BiometricsData>> GetAll()
        {
            return new ApiResponse<IEnumerable<BiometricsData>>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString(),
                Result = _data.FilterBy(x => x.DateDeleted == null)
            };
        }

        public async Task<ApiResponse<StatusCode>> Update(BiometricsData entity)
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

