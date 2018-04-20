using DemoJwt.Application.Core;
using DemoJwt.Application.Models;
using DemoJwt.Application.Requests;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DemoJwt.Application.Handlers
{
    public class AuthenticateUserHandler : IRequestHandler<AuthenticateUser, Response>
    {
        public async Task<Response> Handle(AuthenticateUser request, CancellationToken cancellationToken)
        {
            var user = new User("Wellington Nascimento", "wellington.jhn@gmail.com", string.Empty);
            var response = new Response(user);
           
            return await Task.FromResult(response);
        }
    }
}