using DemoJwt.Application.Core;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DemoJwt.IoC
{
    public static class Container
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            AddMediatr(services);

            return services;
        }

        private static void AddMediatr(IServiceCollection services)
        {
            var assembly = AppDomain.CurrentDomain.Load("DemoJwt.Application");

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestsValidationMiddleware<,>));
            services.AddMediatR(assembly);
        }
        
    }
}