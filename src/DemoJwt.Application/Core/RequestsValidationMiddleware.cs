using Flunt.Notifications;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoJwt.Application.Core
{
    /// <summary>
    /// Middleware responsável por validar os comandos antes de seus respectivos Handlers serem executados (fail-fast)
    /// </summary>
    /// <typeparam name="TRequest">Tipo do comando</typeparam>
    /// <typeparam name="TResponse">Tipo do retorno do comando</typeparam>
    public class RequestsValidationMiddleware<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : Request<TResponse> where TResponse : Response
    {

        /// <summary>
        /// Executa o middleware
        /// </summary>
        /// <param name="request">Comando a ser validado</param>
        /// <param name="next">Próximo middleware do pipeline do MediatR</param>
        /// <returns>Retorna a resposta do comando ou os erros acusados pelo validador do comando</returns>
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next)
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
