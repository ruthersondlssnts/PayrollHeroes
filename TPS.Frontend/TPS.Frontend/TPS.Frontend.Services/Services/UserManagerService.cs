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
using TPS.Frontend.Infrastructure.Models;
using TPS.Frontend.Services.Interfaces;

namespace TPS.Frontend.Services.Services
{
    public class UserManagerService : IUserManagerService
    {
        private HttpClient _client;

        public UserManagerService(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri("http://localhost:9001");
            _client.DefaultRequestHeaders.Clear();
        }

        public async Task<DTOAuthenticationResponse> LoginAsync(CancellationToken ct, LoginModel model)
        {
            var memoryContentStream = new MemoryStream();
            memoryContentStream.SerializeToJsonAndWrite(model, new UTF8Encoding(), 1024, true);
            memoryContentStream.Seek(0, SeekOrigin.Begin);

            using (var request = new HttpRequestMessage(
                           HttpMethod.Post,
                           $"api/v1/authentication/login"))
            {
                request.Headers.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                using (var streamContent = new StreamContent(memoryContentStream))
                {
                    request.Content = streamContent;
                    request.Content.Headers.ContentType =
                      new MediaTypeHeaderValue("application/json");

                    using (var response = await _client.SendAsync(request,
                        HttpCompletionOption.ResponseHeadersRead, ct))
                    {
                        var stream = await response.Content.ReadAsStreamAsync();
                        var result = stream.ReadAndDeserializeFromJson<DTOAuthenticationResponse>();
                        return result;
                    }
                }
            }
        }
        public async Task<DTOTokenResponse> VerifyTokenAsync(CancellationToken ct, DTOTokenVerification model)
        {
            var memoryContentStream = new MemoryStream();
            memoryContentStream.SerializeToJsonAndWrite(model, new UTF8Encoding(), 1024, true);
            memoryContentStream.Seek(0, SeekOrigin.Begin);

            using (var request = new HttpRequestMessage(
                           HttpMethod.Post,
                           $"api/v1/authentication/verifyToken"))
            {
                request.Headers.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                using (var streamContent = new StreamContent(memoryContentStream))
                {
                    request.Content = streamContent;
                    request.Content.Headers.ContentType =
                      new MediaTypeHeaderValue("application/json");

                    using (var response = await _client.SendAsync(request,
                        HttpCompletionOption.ResponseHeadersRead, ct))
                    {
                        var stream = await response.Content.ReadAsStreamAsync();
                        var result = stream.ReadAndDeserializeFromJson<DTOTokenResponse>();
                        return result;
                    }
                }
            }
        }

    }
}
