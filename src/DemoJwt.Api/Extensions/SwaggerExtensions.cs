using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.IO;

namespace DemoJwt.Api.Extensions
{
    public static class SwaggerExtensions
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SchemaFilter<SwaggerExcludePropertiesFilter>();

                options.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Demo Jwt",
                        Version = "v1",
                        Description = "Projeto de demonstração ASP.Net Core",
                        Contact = new Contact
                        {
                            Name = "Wellington Nascimento",
                            Url = "https://github.com/wellingtonjhn"
                        }
                    });

                options.AddSecurityDefinition(
                    "bearer",
                    new ApiKeyScheme
                    {
                        In = "header",
                        Description = "Autenticação baseada em Json Web Token (JWT)",
                        Name = "Authorization",
                        Type = "apiKey"
                    });

                SetXmlDocumentation(options);
            });
        }

        private static void SetXmlDocumentation(SwaggerGenOptions options)
        {
            var xmlDocumentPath = GetXmlDocumentPath();
            var existsXmlDocument = File.Exists(xmlDocumentPath);

            if (existsXmlDocument)
            {
                options.IncludeXmlComments(xmlDocumentPath);
            }
        }
        private static string GetXmlDocumentPath()
        {
            var applicationBasePath = PlatformServices.Default.Application.ApplicationBasePath;
            var applicationName = PlatformServices.Default.Application.ApplicationName;
            return Path.Combine(applicationBasePath, $"{applicationName}.xml");
        }
    }
}