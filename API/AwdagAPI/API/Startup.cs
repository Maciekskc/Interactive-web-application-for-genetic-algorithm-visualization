using API.Utilities.Middleware;
using Application.Infrastructure;
using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Domain.Models.Entities;
using Infrastructure;
using Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Persistence;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Validation.Models;
using Validation.Utilities;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            TokenValidationParametersDefaults.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AddDb(services);

            AddMapper(services);

            AddPrincipalJwt(services);

            AddApplicationServices(services);

            // register cors
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins("http://localhost:3000")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            AddMvc(services);

            AddSecurity(services);

            AddSwagger(services);

            ConfigureLog4Net();

            services.AddHttpContextAccessor();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var detailedErrorsDictionary = GetDetailedErrors(context.ModelState);
                    var result = new BadRequestObjectResult(detailedErrorsDictionary);
                    result.ContentTypes.Add(MediaTypeNames.Application.Json);
                    result.ContentTypes.Add(MediaTypeNames.Application.Xml);
                    return result;
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLog4Net();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
            });

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });
        }

        /// <summary>
        /// Konwertuje error message przesłane przez DataAnnotations na customowe errory
        /// </summary>
        /// <param name="modelDictionary"></param>
        /// <returns></returns>
        private Dictionary<string, List<DetailedError>> GetDetailedErrors(ModelStateDictionary modelDictionary)
        {
            var detailedErrorsDictionary = new Dictionary<string, List<DetailedError>>();

            foreach (var modelState in modelDictionary)
            {
                var errors = ErrorMatching.TranslateErrors(modelState.Value.Errors.Select(x => x.ErrorMessage));
                detailedErrorsDictionary.Add(modelState.Key.ToCamelCase(), errors);
            }

            detailedErrorsDictionary = detailedErrorsDictionary
                .Where(f => f.Value.Count > 0)
                .ToDictionary(x => x.Key, x => x.Value);

            return detailedErrorsDictionary;
        }

        private void ConfigureLog4Net()
        {
            XmlDocument log4NetConfig = new XmlDocument();
            // �adowanie konfiugracji

            log4NetConfig.Load(File.OpenRead("log4net.config"));
            var repo = log4net.LogManager.CreateRepository(Assembly.GetEntryAssembly(),
                typeof(log4net.Repository.Hierarchy.Hierarchy));

            // tworzenie finalnej konfiguracji
            log4net.Config.XmlConfigurator.Configure(repo, log4NetConfig["log4net"]);
        }

        private static void AddMapper(IServiceCollection services)
        {
            services.AddAutoMapper(c => c.AddProfile<AutoMapperProfile>(),
                typeof(Startup));
        }

        private static void AddPrincipalJwt(IServiceCollection services)
        {
            services.AddScoped<IUserAccessor, UserAccessor>();
            services.AddScoped<IJwtGenerator, JwtGenerator>();
        }

        private static void AddApplicationServices(IServiceCollection services)
        {
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IAdminService, AdminService>();
            services.AddTransient<ILogsService, LogsService>();
            services.AddTransient<IMaintenanceService, MaintenanceService>();
        }

        private static void AddMvc(IServiceCollection services)
        {
            services.AddMvc(option => option.EnableEndpointRouting = false)
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                });
        }

        private static void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi-Template", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });
        }

        private void AddSecurity(IServiceCollection services)
        {
            services.AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(opt =>
                {
                    opt.SaveToken = true;
                    opt.TokenValidationParameters = TokenValidationParametersDefaults.GetDefaultParameters();
                });

            services.AddAuthorization();
        }

        private void AddDb(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services
                .AddIdentity<ApplicationUser, IdentityRole>()
                .AddSignInManager<SignInManager<ApplicationUser>>()
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();
        }
    }
}