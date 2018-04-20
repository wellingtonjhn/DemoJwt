using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;

namespace DemoJwt.Api.Extensions
{
    /// <summary>
    /// Representa extensões para configurar o Swagger para gerar documentação dos endpoints da API
    /// </summary>
    public static class SwaggerExtensions
    {
        /// <summary>
        /// Registra o Swagger no injetor de dependências
        /// </summary>
        /// <param name="services">Instância do injetor de dependências</param>
        public static void AddSwagger(this IServiceCollection services)
        {
            var applicationPath = PlatformServices.Default.Application.ApplicationBasePath;
            var applicationName = PlatformServices.Default.Application.ApplicationName;
            var xmlDocPath = Path.Combine(applicationPath, $"{applicationName}.xml");

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
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

                c.AddSecurityDefinition(
                    "bearer",
                    new ApiKeyScheme
                    {
                        In = "header",
                        Description = "Autenticação baseada em Json Web Token (JWT)",
                        Name = "Authorization",
                        Type = "apiKey"
                    });

                c.IncludeXmlComments(xmlDocPath);
            });
        }
    }
}