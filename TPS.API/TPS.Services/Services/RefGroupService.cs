using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TPS.Infrastructure;
using TPS.Infrastructure.Enums;
using TPS.Infrastructure.Models;
using TPS.Services.Interfaces;

namespace TPS.Services.Services
{
    public class RefGroupService : IRefGroupService
    {
        private readonly IDBService<RefGroup> _data;
        public RefGroupService(IDBService<RefGroup> data)
        {
            _data = data;
        }

        public async Task<ApiResponse<StatusCode>> Create(RefGroup entity)
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
            await _data.DeleteManyAsync(z => z.PathToNode.Contains(id));
            return new ApiResponse<StatusCode>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString()
            };
        }

        public async Task<ApiResponse<RefGroup>> FindById(string id)
        {
            return new ApiResponse<RefGroup>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString(),
                Result = _data.FindById(id)
            };
        }

        public ApiResponse<IEnumerable<RefGroup>> GetAll()
        {
            return new ApiResponse<IEnumerable<RefGroup>>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString(),
                Result = _data.FilterBy(x => x.DateDeleted == null)
            };
        }

        public ApiResponse<IEnumerable<RefGroup>> GetDescendants(string pathToNode)
        {
            if (String.IsNullOrWhiteSpace(pathToNode))
            {
                return new ApiResponse<IEnumerable<RefGroup>>
                {
                    StatusCode = StatusCode.Success,
                    Message = StatusCode.Success.ToString(),
                    Result = _data.FilterBy(x => x.PathToNode == null && x.DateDeleted == null)
                };
            }
            return new ApiResponse<IEnumerable<RefGroup>>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString(),
                Result = _data.FilterBy(x => x.PathToNode == pathToNode && x.DateDeleted == null)
            };
        }

        public async Task<ApiResponse<StatusCode>> Update(RefGroup entity)
        {
            await _data.ReplaceOneAsync(entity);
            return new ApiResponse<StatusCode>
            {
                StatusCode = StatusCode.Success,
                Message = StatusCode.Success.ToString()
            };
        }

        public async Task<ApiResponse<List<GroupApprover>>> GetApprovers(string groupId, ApproverType type)
        {
            Guid id = new Guid(groupId);

            if (type == ApproverType.LEAVE)
            {
                return new ApiResponse<List<GroupApprover>>
                {
                    StatusCode = StatusCode.Success,
                    Message = StatusCode.Success.ToString(),
                    Result = _data.FindOne(x => x.Id == id).LeaveApprover
                };
            }
            if (type == ApproverType.OVERTIME)
            {
                return new ApiResponse<List<GroupApprover>>
                {
                    StatusCode = StatusCode.Success,
                    Message = StatusCode.Success.ToString(),
                    Result = _data.FindOne(x => x.Id == id).OvertimeApprover
                };
            }
            if (type == ApproverType.DTR)
            {
                return new ApiResponse<List<GroupApprover>>
                {
                    StatusCode = StatusCode.Success,
                    Message = StatusCode.Success.ToString(),
                    Result = _data.FindOne(x => x.Id == id).DTRApprover
                };
            }

            return new ApiResponse<List<GroupApprover>>
            {
                StatusCode = StatusCode.Conflict,
                Message = "No data found"
            };
        }
    }
}
