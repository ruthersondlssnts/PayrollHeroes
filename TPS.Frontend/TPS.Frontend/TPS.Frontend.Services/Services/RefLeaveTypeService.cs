﻿using Marvin.StreamExtensions;
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


    public class RefLeaveTypeService : IRefLeaveTypeService
    {
        private HttpClient _client;
        public RefLeaveTypeService(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri("http://localhost:9001");
            _client.DefaultRequestHeaders.Clear();
        }
        public async Task<ApiResponse<IEnumerable<RefLeaveType>>> GetLeaveTypesAsync(CancellationToken cancellationToken, string accessToken)
        {

            var request = new HttpRequestMessage(
              HttpMethod.Get,
             "/api/v1/refleavetype");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            request.Headers.Add("Authorization", "Bearer " + accessToken);
            using (var response = await _client.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead, cancellationToken))
            {

                var stream = await response.Content.ReadAsStreamAsync();
                var result = stream.ReadAndDeserializeFromJson<ApiResponse<IEnumerable<RefLeaveType>>>();
                return result;
            }

        }

        public async Task<ApiResponse<StatusCode>> AddOrEditLeaveTypeAsync(CancellationToken cancellationToken, string accessToken, RefLeaveType model)
        {
            var memoryContentStream = new MemoryStream();
            memoryContentStream.SerializeToJsonAndWrite(model,
                new UTF8Encoding(), 1024, true);

            memoryContentStream.Seek(0, SeekOrigin.Begin);

            if (model.Id == Guid.Empty)
            {
                using (var request = new HttpRequestMessage(
                            HttpMethod.Post,
                            $"api/v1/refleavetype"))
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
                          $"api/v1/refleavetype"))
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
                   HttpCompletionOption.ResponseHeadersRead, cancellationToken))
                        {

                            var stream = await response.Content.ReadAsStreamAsync();
                            return stream.ReadAndDeserializeFromJson<ApiResponse<StatusCode>>();
                        }
                    }
                }
            }

        }

        public async Task<ApiResponse<StatusCode>> DeleteLeaveTypeAsync(CancellationToken cancellationToken, string accessToken, string id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete,
                $"api/v1/refleavetype?id={id}");
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

        public async Task<ApiResponse<RefLeaveType>> FindLeaveTypeAsync(CancellationToken cancellationToken, string accessToken, string id)
        {
            var request = new HttpRequestMessage(
                 HttpMethod.Get,
                 $"api/v1/refleavetype/getLeaveType?id={id}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            request.Headers.Add("Authorization", "Bearer " + accessToken);
            using (var response = await _client.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead, cancellationToken))
            {
                var stream = await response.Content.ReadAsStreamAsync();
                return stream.ReadAndDeserializeFromJson<ApiResponse<RefLeaveType>>();
            }
        }
    }
}
