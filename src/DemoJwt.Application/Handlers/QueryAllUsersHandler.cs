using DemoJwt.Application.Contracts;
using DemoJwt.Application.Core;
using DemoJwt.Application.Requests;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DemoJwt.Application.Handlers
{
    public class QueryAllUsersHandler : IRequestHandler<QueryAllUsers, Response>
    {
        private readonly IUserRepository _repository;

        public QueryAllUsersHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response> Handle(QueryAllUsers request, CancellationToken cancellationToken)
        {
            var response = new Response();
            var users = await _repository.GetUsers();
                
            var result = users.Select(a=> new
            {
                a.Id,
                a.Name,
                a.Email,
                a.Roles,
                a.Permissions
            });

            response.AddValue(result);

            return response;
        }
    }
}