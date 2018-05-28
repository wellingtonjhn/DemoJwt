using System.Threading;
using System.Threading.Tasks;
using DemoJwt.Application.Contracts;
using DemoJwt.Application.Core;
using DemoJwt.Application.Requests;
using Flunt.Notifications;
using MediatR;

namespace DemoJwt.Application.Handlers
{
    public class RemoveUserHandler : IRequestHandler<RemoveAccount, Response>
    {
        private readonly IUserRepository _repository;

        public RemoveUserHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response> Handle(RemoveAccount request, CancellationToken cancellationToken)
        {
            var response = new Response();
            var user = await _repository.Get(request.Id);

            if (user == null)
            {
                response.AddNotification(new Notification("user", "Usuário não encontrado"));
                return response;
            }

            await _repository.Remove(user);

            return response;
        }
    }
}