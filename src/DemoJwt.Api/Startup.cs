using DemoJwt.Api.Extensions;
using DemoJwt.IoC;
using GlobalExceptionHandler.WebApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace DemoJwt.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);
            services.AddLogging();
            services.AddSwagger();

            services.AddApplicationServices();
            services.AddAuthorizedMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.RoutePrefix = "swagger";
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo JWT Api");
                });
            }

            loggerFactory.AddConsole();

            app.UseAuthentication();
            UseExceptionHandling(app, loggerFactory);

            app.UseMvc();
        }

        private static void UseExceptionHandling(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseExceptionHandler().WithConventions(config =>
            {
                config.ContentType = "application/json";
                config.MessageFormatter(s => JsonConvert.SerializeObject(new
                {
                    Message = "An error occurred whilst processing your request"
                }));

                config.OnError((exception, httpContext) =>
                {
                    var logger = loggerFactory.CreateLogger("GlobalExceptionHandler");
                    logger.LogError(exception, exception.Message);
                    return Task.CompletedTask;
                });
            });
        }
    }
}
