using System.Collections.Generic;
using System.Threading.Tasks;
using TPS.Infrastructure;
using TPS.Infrastructure.Enums;
using TPS.Infrastructure.Models;
using TPS.Services.Interfaces;

namespace TPS.Services.Services
{
    public class RefBIRService : IRefBIRService
    {
        private readonly IDBService<RefBIR> _data;
        public RefBIRService(IDBService<RefBIR> data)
        {
            _data = data;
        }

        public async Task<ApiResponse<StatusCode>> Create(RefBIR entity)
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

        public async Task<ApiResponse<RefBIR>> FindById(string id)
        {
            return new ApiResponse<RefBIR>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString(),
                Result = _data.FindById(id)
            };
        }

        public ApiResponse<IEnumerable<RefBIR>> GetAll()
        {
            return new ApiResponse<IEnumerable<RefBIR>>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString(),
                Result = _data.FilterBy(x => x.DateDeleted == null)
            };
        }

        public async Task<ApiResponse<StatusCode>> Update(RefBIR entity)
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
