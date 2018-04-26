using System.Threading;
using System.Threading.Tasks;
using DemoJwt.Application.Contracts;
using DemoJwt.Application.Core;
using DemoJwt.Application.Requests;
using MediatR;

namespace DemoJwt.Application.Handlers
{
    public class ChangeUserPasswordHandler : IRequestHandler<ChangeUserPassword, Response>
    {
        private readonly IUserRepository _repository;
        private readonly AuthenticatedUser _user;

        public ChangeUserPasswordHandler(IUserRepository repository, AuthenticatedUser user)
        {
            _repository = repository;
            _user = user;
        }

        public async Task<Response> Handle(ChangeUserPassword request, CancellationToken cancellationToken)
        {
            var response = new Response();

            var user = await _repository.Get(_user.Email);

            user.ChangePassword(request.NewPassword, request.NewPasswordConfirmation);

            if (user.Invalid)
            {
                response.AddNotifications(user.Notifications);
                return response;
            }

            await _repository.Save(user);

            return response;
        }
    }
}