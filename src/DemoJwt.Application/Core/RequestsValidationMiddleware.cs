using Flunt.Notifications;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DemoJwt.Application.Core
{
    public class RequestsValidationMiddleware<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : Request<Response>
        where TResponse : Response
    {
        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            return request.Invalid
                ? Errors(request.Notifications)
                : next();
        }

        private static Task<TResponse> Errors(IEnumerable<Notification> notifications)
        {
            var response = new Response();
            response.AddNotifications(notifications);

            return Task.FromResult(response as TResponse);
        }
    }
}
