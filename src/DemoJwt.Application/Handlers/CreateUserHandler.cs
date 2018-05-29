using DemoJwt.Application.Contracts;
using DemoJwt.Application.Core;
using DemoJwt.Application.Models;
using DemoJwt.Application.Requests;
using Flunt.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DemoJwt.Application.Handlers
{
    public class CreateUserHandler : IRequestHandler<CreateUser, Response>
    {
        private readonly IUserRepository _repository;

        public CreateUserHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response> Handle(CreateUser request, CancellationToken cancellationToken)
        {
            var response = new Response();

            var existsUser = await _repository.ExistsUser(request.Email);

            if (existsUser)
            {
                response.AddNotification(new Notification("user", "Jà existe um usuário com esse e-mail"));
                return response;
            }

            var user = new User(request.Name, request.Email, request.Password);

            foreach (var role in request.Roles)
            {
                user.AddRole(role);
            }

            foreach (var policy in request.Permissions)
            {
                user.AddPermission(policy);
            }

            if (user.Invalid)
            {
                response.AddNotifications(user.Notifications);
                return response;
            }

            await _repository.Save(user);

            response.AddValue(new
            {
                user.Id,
                user.Name,
                user.Email,
                user.Roles,
                user.Permissions
            });

            return response;
        }
    }
}