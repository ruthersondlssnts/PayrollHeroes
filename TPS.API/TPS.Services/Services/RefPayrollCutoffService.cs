using System.Collections.Generic;
using System.Threading.Tasks;
using TPS.Infrastructure;
using TPS.Infrastructure.Enums;
using TPS.Infrastructure.Models;
using TPS.Services.Interfaces;

namespace TPS.Services.Services
{
    public class RefPayrollCutoffService : IRefPayrollCutoffService
    {
        private readonly IDBService<RefPayrollCutoff> _data;
        public RefPayrollCutoffService(IDBService<RefPayrollCutoff> data)
        {
            _data = data;
        }

        public async Task<ApiResponse<StatusCode>> Create(RefPayrollCutoff entity)
        {
            entity.PayrollDate = entity.PayrollDate.Date;
            entity.CutoffStartDate = entity.CutoffStartDate.Date;
            entity.CutoffEndDate = entity.CutoffEndDate.Date;
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

        public async Task<ApiResponse<RefPayrollCutoff>> FindById(string id)
        {
            return new ApiResponse<RefPayrollCutoff>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString(),
                Result = _data.FindById(id)
            };
        }

        public ApiResponse<IEnumerable<RefPayrollCutoff>> GetAll()
        {
            return new ApiResponse<IEnumerable<RefPayrollCutoff>>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString(),
                Result = _data.FilterBy(x => x.DateDeleted == null)
            };
        }

        public async Task<ApiResponse<StatusCode>> Update(RefPayrollCutoff entity)
        {
            entity.PayrollDate = entity.PayrollDate.Date;
            entity.CutoffStartDate = entity.CutoffStartDate.Date;
            entity.CutoffEndDate = entity.CutoffEndDate.Date;
            await _data.ReplaceOneAsync(entity);
            return new ApiResponse<StatusCode>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString()
            };
        }
    }
}
