using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TPS.Infrastructure;
using TPS.Services.Interfaces;
using TPS.Services.Services;
using TPS.Services.Utility;

namespace TPS.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvcCore();
            // add support for compressing responses (eg gzip)
            services.AddResponseCompression();
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(
                     options =>
                     {
                         //The format of the version added to the route URL
                         options.GroupNameFormat = "'v'VVV";
                         //Tells swagger to replace the version in the controller route
                         options.SubstituteApiVersionInUrl = true;
                     });


            //Handles swagger documentation
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Timekeeping and Payroll API", Version = "v1" });
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "Timekeeping and Payroll API", Version = "v2" });
                c.EnableAnnotations();
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                //Used to add authorization in swagger
                //ref https://stackoverflow.com/questions/56234504/bearer-authentication-in-swagger-ui-when-migrating-to-swashbuckle-aspnetcore-ve
                c.AddSecurityDefinition("Bearer", //Name the security scheme
                new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Just omit the prefix Bearer when using. Just the token. Okay?",
                    Type = SecuritySchemeType.Http, //We set the scheme type to http since we're using bearer authentication
                    Scheme = "bearer" //The name of the HTTP Authorization scheme to be used in the Authorization header. In this case "bearer".
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference{
                                Id = "Bearer", //The name of the previously defined security scheme.
                                Type = ReferenceType.SecurityScheme
                            }
                        },new List<string>()
                    }
                });

            }
            );
            services.AddControllers().AddJsonOptions(options =>
             options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            //Used [Authorized] attribute which will request bearer token before accessing the API
            //Get the configuration in appsettings.json file
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["ConfigurationSettings:Audience"],
                    ValidIssuer = Configuration["ConfigurationSettings:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["ConfigurationSettings:Key"]))
                };
            });

            //Mapped ConfigurationSettings from appsettings.json
            services.AddSingleton(Configuration.GetSection(nameof(ConfigurationSettings)).Get<ConfigurationSettings>());
            services.AddScoped(typeof(IAuthenticationService), typeof(AuthenticationService));
            services.AddScoped(typeof(IConfigurationSettings), typeof(ConfigurationSettings));
            services.AddScoped(typeof(IDBService<>), typeof(DBService<>));
            services.AddScoped(typeof(IRefPagibigService), typeof(RefPagibigService));
            services.AddScoped(typeof(IRefCalendarActivitiesService), typeof(RefCalendarActivitiesService));
            services.AddScoped(typeof(IRequestOvertimeService), typeof(RequestOvertimeService));
            services.AddScoped(typeof(IRequestLeaveService), typeof(RequestLeaveService));
            services.AddScoped(typeof(IRequestDtrService), typeof(RequestDtrService));
            services.AddScoped(typeof(IRefShiftService), typeof(RefShiftService));
            services.AddScoped(typeof(IRefPayrollCutoffService), typeof(RefPayrollCutoffService));
            services.AddScoped(typeof(IRefLeaveTypeService), typeof(RefLeaveTypeService));
            services.AddScoped(typeof(IEmployeeService), typeof(EmployeeService));
            services.AddScoped(typeof(IEmployeeTimesheetService), typeof(EmployeeTimesheetService));
            services.AddScoped(typeof(IEmployeeBalanceService), typeof(EmployeeBalanceService));
            services.AddScoped(typeof(IRefGroupService), typeof(RefGroupService));
            services.AddScoped(typeof(IRefEmployeeRankService), typeof(RefEmployeeRankService));
            services.AddScoped(typeof(IRefDayTypeService), typeof(RefDayTypeService));
            services.AddScoped(typeof(ITpsConfigurationService), typeof(TpsConfigurationService));
            services.AddScoped(typeof(IBiometricsService), typeof(BiometricsService));
            services.AddScoped(typeof(IRequestChangeShiftService), typeof(RequestChangeShiftService));
            services.AddScoped(typeof(IDashboardService), typeof(DashboardService));
            services.AddScoped(typeof(ICommonService), typeof(CommonService));
            services.AddScoped(typeof(IMessageBroker), typeof(MessageBroker));
            services.AddScoped(typeof(IUserNotificationService), typeof(UserNotificationService));
            services.AddScoped(typeof(IRefPhilHealthService), typeof(RefPhilHealthService));
            services.AddScoped(typeof(IRefBIRService), typeof(RefBIRService));
            services.AddScoped(typeof(IRefSSSService), typeof(RefSSSService));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            app.UseCors(builder => builder
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .SetIsOriginAllowed((host) => true)
                  //.AllowCredentials()
                  );

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //Use to activate Swagger documentation
            app.UseSwagger();
            app.UseSwaggerUI(
            options =>
            {
                // build a swagger endpoint for each discovered API version  
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });
        }
    }
}
