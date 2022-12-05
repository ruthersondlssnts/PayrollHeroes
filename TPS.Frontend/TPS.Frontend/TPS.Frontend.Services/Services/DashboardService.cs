using Marvin.StreamExtensions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TPS.Frontend.Infrastructure;
using TPS.Frontend.Infrastructure.DTO;
using TPS.Frontend.Services.Interface;

namespace TPS.Frontend.Services.Services
{


    public class DashboardService : IDashboardService
    {
        private HttpClient _client;

        public DashboardService(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri("http://localhost:9001");
            _client.DefaultRequestHeaders.Clear();
        }

        public async Task<ApiResponse<IEnumerable<DTODashboardForApproval>>> ForApprovalsAsync(CancellationToken cancellationToken, string accessToken, string userId)
        {

            var request = new HttpRequestMessage(
              HttpMethod.Get,
             $"/api/v1/dashboard?employeeid={userId}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            request.Headers.Add("Authorization", "Bearer " + accessToken);
            using (var response = await _client.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead, cancellationToken))
            {
                var stream = await response.Content.ReadAsStreamAsync();
                return stream.ReadAndDeserializeFromJson<ApiResponse<IEnumerable<DTODashboardForApproval>>>();
            }

        }
    }
}
