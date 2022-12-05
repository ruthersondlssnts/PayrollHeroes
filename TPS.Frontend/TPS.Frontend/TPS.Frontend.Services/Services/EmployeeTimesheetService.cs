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
using TPS.Frontend.Services.Interface;

namespace TPS.Frontend.Services.Services
{
    public class EmployeeTimesheetService : IEmployeeTimesheetService
    {
        private HttpClient _client;
        public EmployeeTimesheetService(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri("http://localhost:9001");
            _client.DefaultRequestHeaders.Clear();
        }
        public async Task<ApiResponse<List<EmployeeTimesheet>>> GetTimesheetByEmployeeId(CancellationToken cancellationToken, string accessToken, string cutoffId, string employeeId)
        {

            try
            {
                var request = new HttpRequestMessage(
          HttpMethod.Get,
           $"/api/v1/employeetimesheet/findByCutoffEmployeeId?cutoff={cutoffId}&employeeId={employeeId}");
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                request.Headers.Add("Authorization", "Bearer " + accessToken);
                using (var response = await _client.SendAsync(request,
                    HttpCompletionOption.ResponseHeadersRead, cancellationToken))
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    var result = stream.ReadAndDeserializeFromJson<ApiResponse<List<EmployeeTimesheet>>>();
                    return result;
                }
            }
            catch (Exception)
            {

            }

            return null;
        }

        public async Task<ApiResponse<List<DTODtrCalendar>>> FindDtrEmployeeByDate(CancellationToken cancellationToken, string accessToken, string employeeId, DateTime start, DateTime end)
        {

            try
            {
                var request = new HttpRequestMessage(
          HttpMethod.Get,
           $"/api/v1/employeetimesheet/findDtrEmployeeByDate?employeeId={employeeId}&start={start}&end={end}");
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                request.Headers.Add("Authorization", "Bearer " + accessToken);
                using (var response = await _client.SendAsync(request,
                    HttpCompletionOption.ResponseHeadersRead, cancellationToken))
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    var result = stream.ReadAndDeserializeFromJson<ApiResponse<List<DTODtrCalendar>>>();
                    return result;
                }
            }
            catch (Exception)
            {

            }

            return null;
        }

        public async Task GenerateTimesheet(CancellationToken cancellationToken, string accessToken, DTOPayrollCutoffRequest model)
        {
            var memoryContentStream = new MemoryStream();
            memoryContentStream.SerializeToJsonAndWrite(model, new UTF8Encoding(), 1024, true);

            memoryContentStream.Seek(0, SeekOrigin.Begin);
            using (var request = new HttpRequestMessage(HttpMethod.Post, $"/api/v1/employeetimesheet/GenerateTimesheet"))
            {
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                using (var streamContent = new StreamContent(memoryContentStream))
                {
                    request.Content = streamContent;
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                    request.Headers.Add("Authorization", "Bearer " + accessToken);
                    using (var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
                    {
                        var stream = await response.Content.ReadAsStreamAsync();
                        var result = stream.ReadAndDeserializeFromJson<ApiResponse<StatusCode>>();
                    }
                }
            }
        }

        public async Task UploadBiometricsFile(CancellationToken cancellationToken, string accessToken, string filename)
        {
            var memoryContentStream = new MemoryStream();
            DTOBiometricUpload payload = new DTOBiometricUpload();
            payload.Filename = filename;
            memoryContentStream.SerializeToJsonAndWrite(payload, new UTF8Encoding(), 1024, true);

            memoryContentStream.Seek(0, SeekOrigin.Begin);
            using (var request = new HttpRequestMessage(HttpMethod.Post, $"/api/v1/employeetimesheet/UploadBiometricsFile"))
            {
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                using (var streamContent = new StreamContent(memoryContentStream))
                {
                    request.Content = streamContent;
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                    request.Headers.Add("Authorization", "Bearer " + accessToken);
                    using (var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
                    {
                        var stream = await response.Content.ReadAsStreamAsync();
                        var result = stream.ReadAndDeserializeFromJson<ApiResponse<StatusCode>>();
                    }
                }
            }
        }
        public async Task GenerateTimesheetOthers(CancellationToken cancellationToken, string accessToken, DTOPayrollCutoffRequest model)
        {
            var memoryContentStream = new MemoryStream();
            memoryContentStream.SerializeToJsonAndWrite(model, new UTF8Encoding(), 1024, true);

            memoryContentStream.Seek(0, SeekOrigin.Begin);
            using (var request = new HttpRequestMessage(HttpMethod.Post, $"/api/v1/employeetimesheet/GenerateTimesheetOthers"))
            {
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                using (var streamContent = new StreamContent(memoryContentStream))
                {
                    request.Content = streamContent;
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                    request.Headers.Add("Authorization", "Bearer " + accessToken);
                    using (var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
                    {
                        var stream = await response.Content.ReadAsStreamAsync();
                        var result = stream.ReadAndDeserializeFromJson<ApiResponse<StatusCode>>();
                    }
                }
            }
        }

    }
}
