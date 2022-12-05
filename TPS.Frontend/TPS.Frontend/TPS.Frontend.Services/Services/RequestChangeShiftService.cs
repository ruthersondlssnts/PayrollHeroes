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
using TPS.Frontend.Services.Interface;

namespace TPS.Frontend.Services.Services
{
    public class RequestChangeShiftService : IRequestChangeShiftService
    {
        private HttpClient _client;
        public RequestChangeShiftService(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri("http://localhost:9001");
            _client.DefaultRequestHeaders.Clear();
        }

        public async Task<ApiResponse<StatusCode>> AddOrEditRequestChangeShiftAsync(CancellationToken cancellationToken, string accessToken, RequestChangeShift model)
        {
            var memoryContentStream = new MemoryStream();
            memoryContentStream.SerializeToJsonAndWrite(model,
                new UTF8Encoding(), 1024, true);

            memoryContentStream.Seek(0, SeekOrigin.Begin);

            if (model.Id == Guid.Empty)
            {
                using (var request = new HttpRequestMessage(
                            HttpMethod.Post,
                            $"api/v1/requestChangeShift"))
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
                          $"api/v1/requestChangeShift"))
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
                            response.EnsureSuccessStatusCode();

                            var stream = await response.Content.ReadAsStreamAsync();
                            return stream.ReadAndDeserializeFromJson<ApiResponse<StatusCode>>();
                        }
                    }
                }
            }
        }

        public async Task<ApiResponse<StatusCode>> DeleteRequestChangeShiftAsync(CancellationToken cancellationToken, string accessToken, string id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete,
              $"api/v1/requestChangeShift?id={id}");
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

        public async Task<ApiResponse<RequestChangeShift>> FindRequestChangeShiftAsync(CancellationToken cancellationToken, string accessToken, string id)
        {
            var request = new HttpRequestMessage(
                 HttpMethod.Get,
                 $"api/v1/requestChangeShift/getrequestChangeShift?id={id}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            request.Headers.Add("Authorization", "Bearer " + accessToken);
            using (var response = await _client.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead))
            {
                response.EnsureSuccessStatusCode();

                var stream = await response.Content.ReadAsStreamAsync();
                return stream.ReadAndDeserializeFromJson<ApiResponse<RequestChangeShift>>();
            }
        }

        public async Task<ApiResponse<IEnumerable<RequestChangeShift>>> GetAllRequestsChangeShiftAsync(CancellationToken cancellationToken, string accessToken)
        {
            var request = new HttpRequestMessage(
             HttpMethod.Get,
            "/api/v1/requestChangeShift");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            request.Headers.Add("Authorization", "Bearer " + accessToken);
            using (var response = await _client.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead))
            {
                response.EnsureSuccessStatusCode();

                var stream = await response.Content.ReadAsStreamAsync();
                return stream.ReadAndDeserializeFromJson<ApiResponse<IEnumerable<RequestChangeShift>>>();
            }
        }

        public async Task<ApiResponse<IEnumerable<RequestChangeShift>>> GetChangeShiftApprovalsAsync(CancellationToken token, string accessToken, string userId)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Get,
               $"api/v1/requestChangeShift/getAllByApproverId?userId={userId}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            request.Headers.Add("Authorization", "Bearer " + accessToken);
            using (var response = await _client.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead))
            {
                var stream = await response.Content.ReadAsStreamAsync();
                return stream.ReadAndDeserializeFromJson<ApiResponse<IEnumerable<RequestChangeShift>>>();
            }
        }

        public async Task<ApiResponse<IEnumerable<RequestChangeShift>>> GetUserRequestsChangeShiftAsync(CancellationToken cancellationToken, string accessToken, string userId)
        {
            var request = new HttpRequestMessage(
            HttpMethod.Get,
           $"/api/v1/requestChangeShift/getByUser?userId={userId}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            request.Headers.Add("Authorization", "Bearer " + accessToken);
            using (var response = await _client.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead))
            {
                response.EnsureSuccessStatusCode();

                var stream = await response.Content.ReadAsStreamAsync();
                return stream.ReadAndDeserializeFromJson<ApiResponse<IEnumerable<RequestChangeShift>>>();
            }
        }
    }
}
