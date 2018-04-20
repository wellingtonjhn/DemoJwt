using DemoJwt.Application.Core;
using DemoJwt.Application.Requests;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DemoJwt.Application.Handlers
{
    public class QueryUserProfileHandler : IRequestHandler<QueryUserProfile, Response>
    {
        public Task<Response> Handle(QueryUserProfile request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}