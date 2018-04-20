using DemoJwt.Application.Contracts;
using DemoJwt.Application.Core;
using DemoJwt.Application.Services;
using DemoJwt.Repository;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DemoJwt.IoC
{
    public static class Container
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddRepository();
            services.AddJwt();
            services.AddMediatr();

            return services;
        }

        private static void AddRepository(this IServiceCollection services)
        {
            services.AddSingleton<InMemoryDatabaseContext>(); 
            services.AddScoped<IUserRepository, UserRepository>();
        }

        private static void AddMediatr(this IServiceCollection services)
        {
            var assembly = AppDomain.CurrentDomain.Load("DemoJwt.Application");

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestsValidationMiddleware<,>));
            services.AddMediatR(assembly);
        }

        private static void AddJwt(this IServiceCollection services)
        {
            services.AddSingleton<JwtSettings>();
            services.AddScoped<IJwtService, JwtService>();
        }

    }
}