using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TPS.Frontend.Services.Interface;
using TPS.Frontend.Services.Interfaces;
using TPS.Frontend.Services.Services;

namespace TPS.Frontend.Extensions
{
    public static class HttpClientExtensions
    {
        public static void AddHttpClients(this IServiceCollection services)
        {
            services.AddHttpClient<IEmployeeService, EmployeeService>()
                .ConfigurePrimaryHttpMessageHandler(handler => new HttpClientHandler()
                {
                    AutomaticDecompression = System.Net.DecompressionMethods.GZip
                });
            services.AddHttpClient<IRefCalendarActivityService, RefCalendarActivityService>()
                .ConfigurePrimaryHttpMessageHandler(handler => new HttpClientHandler()
                {
                    AutomaticDecompression = System.Net.DecompressionMethods.GZip
                });
            services.AddHttpClient<IRefGroupService, RefGroupService>()
                .ConfigurePrimaryHttpMessageHandler(handler => new HttpClientHandler()
                {
                    AutomaticDecompression = System.Net.DecompressionMethods.GZip
                });
            services.AddHttpClient<IRefShiftService, RefShiftService>()
                .ConfigurePrimaryHttpMessageHandler(handler => new HttpClientHandler()
                {
                    AutomaticDecompression = System.Net.DecompressionMethods.GZip
                });
            services.AddHttpClient<IUserManagerService, UserManagerService>()
                .ConfigurePrimaryHttpMessageHandler(handler => new HttpClientHandler()
                {
                    AutomaticDecompression = System.Net.DecompressionMethods.GZip
                });
            services.AddHttpClient<IRefPayrollCutOffService, RefPayrollCutOffService>()
                .ConfigurePrimaryHttpMessageHandler(handler => new HttpClientHandler()
                {
                    AutomaticDecompression = System.Net.DecompressionMethods.GZip
                });
            services.AddHttpClient<IRefPayrollCutOffService, RefPayrollCutOffService>()
                .ConfigurePrimaryHttpMessageHandler(handler => new HttpClientHandler()
                {
                    AutomaticDecompression = System.Net.DecompressionMethods.GZip
                });
            services.AddHttpClient<IRefLeaveTypeService, RefLeaveTypeService>()
                .ConfigurePrimaryHttpMessageHandler(handler => new HttpClientHandler()
                {
                    AutomaticDecompression = System.Net.DecompressionMethods.GZip
                });
            services.AddHttpClient<IEmployeeTimesheetService, EmployeeTimesheetService>()
                .ConfigurePrimaryHttpMessageHandler(handler => new HttpClientHandler()
                {
                    AutomaticDecompression = System.Net.DecompressionMethods.GZip
                });
            services.AddHttpClient<IEmployeeBalanceService, EmployeeBalanceService>()
                .ConfigurePrimaryHttpMessageHandler(handler => new HttpClientHandler()
                {
                    AutomaticDecompression = System.Net.DecompressionMethods.GZip
                });
            services.AddHttpClient<IRequestOvertimeService, RequestOvertimeService>()
                .ConfigurePrimaryHttpMessageHandler(handler => new HttpClientHandler()
                {
                    AutomaticDecompression = System.Net.DecompressionMethods.GZip
                });
            services.AddHttpClient<IRequestDTRService, RequestDTRService>()
                .ConfigurePrimaryHttpMessageHandler(handler => new HttpClientHandler()
                {
                    AutomaticDecompression = System.Net.DecompressionMethods.GZip
                });
            services.AddHttpClient<IRequestLeaveService, RequestLeaveService>()
                .ConfigurePrimaryHttpMessageHandler(handler => new HttpClientHandler()
                {
                    AutomaticDecompression = System.Net.DecompressionMethods.GZip
                });
            services.AddHttpClient<IDashboardService, DashboardService>()
                .ConfigurePrimaryHttpMessageHandler(handler => new HttpClientHandler()
                {
                    AutomaticDecompression = System.Net.DecompressionMethods.GZip
                });
            services.AddHttpClient<IRefBIRService, RefBIRService>()
                .ConfigurePrimaryHttpMessageHandler(handler => new HttpClientHandler()
                {
                    AutomaticDecompression = System.Net.DecompressionMethods.GZip
                });
            services.AddHttpClient<IRefPagibigService, RefPagibigService>()
                .ConfigurePrimaryHttpMessageHandler(handler => new HttpClientHandler()
                {
                    AutomaticDecompression = System.Net.DecompressionMethods.GZip
                });
            services.AddHttpClient<IRefPhilHealthService, RefPhilHealthService>()
                .ConfigurePrimaryHttpMessageHandler(handler => new HttpClientHandler()
                {
                    AutomaticDecompression = System.Net.DecompressionMethods.GZip
                });
            services.AddHttpClient<IRefSSSService, RefSSSService>()
                .ConfigurePrimaryHttpMessageHandler(handler => new HttpClientHandler()
                {
                    AutomaticDecompression = System.Net.DecompressionMethods.GZip
                });
            services.AddHttpClient<IUserNotificationService, UserNotificationService>()
                .ConfigurePrimaryHttpMessageHandler(handler => new HttpClientHandler()
                {
                    AutomaticDecompression = System.Net.DecompressionMethods.GZip
                });

        }
    }
}
