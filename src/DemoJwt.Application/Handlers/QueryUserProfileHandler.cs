using DemoJwt.Application.Contracts;
using DemoJwt.Application.Core;
using DemoJwt.Application.Requests;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DemoJwt.Application.Handlers
{
    public class QueryUserProfileHandler : IRequestHandler<QueryUserProfile, Response>
    {
        private readonly IUserRepository _repository;
        private readonly AuthenticatedUser _user;

        public QueryUserProfileHandler(IUserRepository repository, AuthenticatedUser user)
        {
            _repository = repository;
            _user = user;
        }

        public async Task<Response> Handle(QueryUserProfile request, CancellationToken cancellationToken)
        {
            var response = new Response();

            var user = await _repository.Get(_user.Email);

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