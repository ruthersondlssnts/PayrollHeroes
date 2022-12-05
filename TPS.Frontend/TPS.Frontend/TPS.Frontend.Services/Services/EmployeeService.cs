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
    public class EmployeeService : IEmployeeService
    {
        private HttpClient _client;
        public EmployeeService(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri("http://localhost:9001");
            _client.DefaultRequestHeaders.Clear();
        }

        public async Task<ApiResponse<StatusCode>> AddOrEditEmployeeAsync(CancellationToken cancellationToken, string accessToken, Employee model)
        {
            var memoryContentStream = new MemoryStream();
            memoryContentStream.SerializeToJsonAndWrite(model,
                new UTF8Encoding(), 1024, true);

            memoryContentStream.Seek(0, SeekOrigin.Begin);

            if (model.Id == Guid.Empty)
            {
                using (var request = new HttpRequestMessage(
                            HttpMethod.Post,
                            $"api/v1/employee"))
                {
                    request.Headers.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    using (var streamContent = new StreamContent(memoryContentStream))
                    {
                        request.Content = streamContent;
                        request.Content.Headers.ContentType =
                          new MediaTypeHeaderValue("application/json");
                        request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                        request.Headers.Add("Authorization", "Bearer " + accessToken);

                        using (var response = await _client.SendAsync(request,
                   HttpCompletionOption.ResponseHeadersRead, cancellationToken))
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
                          $"api/v1/employee"))
                {
                    request.Headers.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    using (var streamContent = new StreamContent(memoryContentStream))
                    {
                        request.Content = streamContent;
                        request.Content.Headers.ContentType =
                          new MediaTypeHeaderValue("application/json");
                        request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                        request.Headers.Add("Authorization", "Bearer " + accessToken);


                        using (var response = await _client.SendAsync(request,
                   HttpCompletionOption.ResponseHeadersRead, cancellationToken))
                        {
                            var stream = await response.Content.ReadAsStreamAsync();
                            return stream.ReadAndDeserializeFromJson<ApiResponse<StatusCode>>();
                        }
                    }
                }
            }

        }

        public async Task<ApiResponse<StatusCode>> DeleteEmployeeAsync(CancellationToken cancellationToken, string accessToken, string id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete,
                $"api/v1/employee?id={id}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            request.Headers.Add("Authorization", "Bearer " + accessToken);

            using (var response = await _client.SendAsync(request,
              HttpCompletionOption.ResponseHeadersRead, cancellationToken))
            {
                var stream = await response.Content.ReadAsStreamAsync();
                return stream.ReadAndDeserializeFromJson<ApiResponse<StatusCode>>();
            }
        }

        public async Task<PaginatedReturn<Employee>> GetEmployeesAsync(CancellationToken cancellationToken, string accessToken, PaginatedEmployees param)
        {

            var request = new HttpRequestMessage(
          HttpMethod.Get,
         $"api/v1/employee/getEmployees?PageNumber={param.PageNumber}&PageSize={param.PageSize}&SearchKeyword={param.SearchKeyword}&OrderByColumn={param.OrderByColumn}&OrderByASCOrDESC={param.OrderByASCOrDESC}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            request.Headers.Add("Authorization", "Bearer " + accessToken);

            using (var response = await _client.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead, cancellationToken))
            {
                var stream = await response.Content.ReadAsStreamAsync();
                return stream.ReadAndDeserializeFromJson<PaginatedReturn<Employee>>();
            }
        }

        public async Task<ApiResponse<Employee>> FindEmployeeAsync(CancellationToken cancellationToken, string accessToken, string id)
        {
            var request = new HttpRequestMessage(
                 HttpMethod.Get,
                 $"api/v1/employee/getEmployee?id={id}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            request.Headers.Add("Authorization", "Bearer " + accessToken);

            using (var response = await _client.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead, cancellationToken))
            {
                var stream = await response.Content.ReadAsStreamAsync();
                return stream.ReadAndDeserializeFromJson<ApiResponse<Employee>>();
            }

        }

        public async Task<ApiResponse<IEnumerable<DTOEmployee>>> GetFullNamesAsync(CancellationToken cancellationToken, string accessToken)
        {
            var request = new HttpRequestMessage(
             HttpMethod.Get,
            "/api/v1/employee/getFullNames");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            request.Headers.Add("Authorization", "Bearer " + accessToken);
            using (var response = await _client.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead, cancellationToken))
            {
                var stream = await response.Content.ReadAsStreamAsync();
                return stream.ReadAndDeserializeFromJson<ApiResponse<IEnumerable<DTOEmployee>>>();
            }
        }
    }
}
