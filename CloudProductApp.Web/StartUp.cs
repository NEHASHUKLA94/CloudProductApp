using CloudProductApp.Web.Infrastructure.API.Interfaces;
using Infrastructure.API;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace CloudProductApp.Web
{
    public class Startup
    {
        public class AppSettings
        {
            public string ApiKey { get; set; }
        }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup));
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            // Other service registrations

            services.AddHttpClient<CloudApiClient>(client =>
            {
                client.BaseAddress = new Uri("http://alltheclouds.com.au/");
            });

            // Mediator setup
            services.AddScoped<IMediator, Mediator>();
            services.AddTransient<ServiceFactory>(sp => t => sp.GetService(t)); // Add this line
            services.AddScoped<ICloudApiClient, CloudApiClient>(); // Replace CloudApiClient with the actual implementation class
            services.AddHttpClient<ICloudApiClient, CloudApiClient>(client =>
            {
                client.BaseAddress = new Uri("http://alltheclouds.com.au/");
            });
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
            // API versioning and controllers
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });

            services.AddControllers();

            // Add Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cloud Product API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Other configurations
            app.UseCors();

            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cloud Product API v1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


    }
}
