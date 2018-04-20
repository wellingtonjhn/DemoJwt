using Flunt.Notifications;
using MediatR;

namespace DemoJwt.Application.Core
{
    public abstract class Request<TResponse> : Notifiable, IRequest<TResponse>
    {

    }
}