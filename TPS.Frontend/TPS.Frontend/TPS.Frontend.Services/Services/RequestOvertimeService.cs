using Marvin.StreamExtensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TPS.Frontend.Infrastructure;
using TPS.Frontend.Infrastructure.DTO;
using TPS.Frontend.Infrastructure.Pagination;
using TPS.Frontend.Services.Interface;

namespace TPS.Frontend.Services.Services
{
    public class RequestOvertimeService : IRequestOvertimeService
    {
        private HttpClient _client;
        public RequestOvertimeService(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri("http://localhost:9001");
            _client.DefaultRequestHeaders.Clear();
        }
        public async Task<ApiResponse<StatusCode>> ProcessApprovalAsync(CancellationToken cancellationToken, string accessToken, DTOApproval model)
        {
            var memoryContentStream = new MemoryStream();
            memoryContentStream.SerializeToJsonAndWrite(model,
                new UTF8Encoding(), 1024, true);

            memoryContentStream.Seek(0, SeekOrigin.Begin);


            using (var request = new HttpRequestMessage(
                      HttpMethod.Post,
                      $"api/v1/requestovertime/processApproval"))
            {
                request.Headers.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                request.Headers.Add("Authorization", "Bearer " + accessToken);
                using (var streamContent = new StreamContent(memoryContentStream))
                {
                    request.Content = streamContent;
                    request.Content.Headers.ContentType =
                      new MediaTypeHeaderValue("application/json");

                    using (var response = await _client.SendAsync(request,
               HttpCompletionOption.ResponseHeadersRead))
                    {
                        var stream = await response.Content.ReadAsStreamAsync();
                        return stream.ReadAndDeserializeFromJson<ApiResponse<StatusCode>>();
                    }
                }
            }
        }
        public async Task<ApiResponse<IEnumerable<RequestOvertime>>> GetUserRequestsOvertimeAsync(CancellationToken cancellationToken, string accessToken, string userId)
        {

            var request = new HttpRequestMessage(
              HttpMethod.Get,
             $"/api/v1/requestovertime/getByUser?userId={userId}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            request.Headers.Add("Authorization", "Bearer " + accessToken);
            using (var response = await _client.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead))
            {
                var stream = await response.Content.ReadAsStreamAsync();
                return stream.ReadAndDeserializeFromJson<ApiResponse<IEnumerable<RequestOvertime>>>();
            }

        }
        public async Task<ApiResponse<IEnumerable<RequestOvertime>>> GetAllRequestsOvertimeAsync(CancellationToken cancellationToken, string accessToken)
        {

            var request = new HttpRequestMessage(
              HttpMethod.Get,
             "/api/v1/requestovertime");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            request.Headers.Add("Authorization", "Bearer " + accessToken);
            using (var response = await _client.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead))
            {
                var stream = await response.Content.ReadAsStreamAsync();
                return stream.ReadAndDeserializeFromJson<ApiResponse<IEnumerable<RequestOvertime>>>();
            }

        }

        public async Task<PaginatedReturn<RequestOvertime>> GetPaginatedRequestOvertimeAsync(CancellationToken cancellationToken, string accessToken, PaginatedRequest param)
        {

            var request = new HttpRequestMessage(
          HttpMethod.Get,
         $"api/v1/requestovertime/getPaginatedList?PageNumber={param.PageNumber}&PageSize={param.PageSize}&SearchKeyword={param.SearchKeyword}&OrderByColumn={param.OrderByColumn}&OrderByASCOrDESC={param.OrderByASCOrDESC}&EmployeeId={param.EmployeeId}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            request.Headers.Add("Authorization", "Bearer " + accessToken);

            using (var response = await _client.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead, cancellationToken))
            {
                var stream = await response.Content.ReadAsStreamAsync();
                return stream.ReadAndDeserializeFromJson<PaginatedReturn<RequestOvertime>>();
            }
        }

        public async Task<ApiResponse<StatusCode>> AddOrEditRequestOvertimeAsync(CancellationToken cancellationToken, string accessToken, RequestOvertime model)
        {
            var memoryContentStream = new MemoryStream();
            memoryContentStream.SerializeToJsonAndWrite(model,
                new UTF8Encoding(), 1024, true);

            memoryContentStream.Seek(0, SeekOrigin.Begin);

            if (model.Id == Guid.Empty)
            {
                using (var request = new HttpRequestMessage(
                            HttpMethod.Post,
                            $"api/v1/requestovertime"))
                {
                    request.Headers.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                    request.Headers.Add("Authorization", "Bearer " + accessToken);
                    using (var streamContent = new StreamContent(memoryContentStream))
                    {
                        request.Content = streamContent;
                        request.Content.Headers.ContentType =
                          new MediaTypeHeaderValue("application/json");

                        using (var response = await _client.SendAsync(request,
                   HttpCompletionOption.ResponseHeadersRead))
                        {

                            var stream = await response.Content.ReadAsStreamAsync();
                            return stream.ReadAndDeserializeFromJson<ApiResponse<StatusCode>>();
                        }
                    }
                }
            }
            else
            {
                using (var request = new HttpRequestMessage(
                          HttpMethod.Put,
                          $"api/v1/requestovertime"))
                {
                    request.Headers.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                    request.Headers.Add("Authorization", "Bearer " + accessToken);
                    using (var streamContent = new StreamContent(memoryContentStream))
                    {
                        request.Content = streamContent;
                        request.Content.Headers.ContentType =
                          new MediaTypeHeaderValue("application/json");

                        using (var response = await _client.SendAsync(request,
                   HttpCompletionOption.ResponseHeadersRead))
                        {

                            var stream = await response.Content.ReadAsStreamAsync();
                            return stream.ReadAndDeserializeFromJson<ApiResponse<StatusCode>>();
                        }
                    }
                }
            }

        }
        public async Task<ApiResponse<StatusCode>> DeleteRequestOvertimeAsync(CancellationToken cancellationToken, string accessToken, string id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete,
                $"api/v1/requestovertime?id={id}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            request.Headers.Add("Authorization", "Bearer " + accessToken);
            using (var response = await _client.SendAsync(request,
              HttpCompletionOption.ResponseHeadersRead))
            {
                var stream = await response.Content.ReadAsStreamAsync();
                return stream.ReadAndDeserializeFromJson<ApiResponse<StatusCode>>();
            }
        }
        public async Task<ApiResponse<RequestOvertime>> FindRequestOvertimeAsync(CancellationToken cancellationToken, string accessToken, string id)
        {
            var request = new HttpRequestMessage(
                 HttpMethod.Get,
                 $"api/v1/requestovertime/getRequestOvertime?id={id}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            request.Headers.Add("Authorization", "Bearer " + accessToken);
            using (var response = await _client.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead))
            {
                var stream = await response.Content.ReadAsStreamAsync();
                return stream.ReadAndDeserializeFromJson<ApiResponse<RequestOvertime>>();
            }

        }

        public async Task<ApiResponse<IEnumerable<RequestOvertime>>> GetOvertimeApprovalsAsync(CancellationToken token, string accessToken, string userId)
        {
            var request = new HttpRequestMessage(
                  HttpMethod.Get,
                 $"api/v1/requestovertime/getAllByApproverId?userId={userId}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            request.Headers.Add("Authorization", "Bearer " + accessToken);
            using (var response = await _client.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead))
            {
                var stream = await response.Content.ReadAsStreamAsync();
                return stream.ReadAndDeserializeFromJson<ApiResponse<IEnumerable<RequestOvertime>>>();
            }
        }
    }
}
