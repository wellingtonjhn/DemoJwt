using DemoJwt.Application.Contracts;
using DemoJwt.Application.Core;
using DemoJwt.Application.Requests;
using Flunt.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using DemoJwt.Application.Models;

namespace DemoJwt.Application.Handlers
{
    public class AuthenticateUserHandler : IRequestHandler<AuthenticateUser, Response>
    {
        private readonly IJwtService _jwtService;
        private readonly IUserRepository _repository;

        public AuthenticateUserHandler(IJwtService jwtService, IUserRepository repository)
        {
            _jwtService = jwtService;
            _repository = repository;
        }

        public async Task<Response> Handle(AuthenticateUser request, CancellationToken cancellationToken)
        {
            var response = new Response();
            var encodedPassword = new Password(request.Password).Encoded;
            var user = await _repository.Authenticate(request.Email, encodedPassword);

            if (user == null)
            {
                response.AddNotification(new Notification("user", "Usuário ou senha inválidos"));
                return response;
            }

            var jwt = _jwtService.CreateJwtToken(user);
            response.AddValue(jwt);

            return response;
        }
    }
}