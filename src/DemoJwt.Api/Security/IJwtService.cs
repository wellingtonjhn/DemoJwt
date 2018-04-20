using DemoJwt.Application.Models;

namespace DemoJwt.Api.Security
{
    public interface IJwtService
    {
        object CreateJwtToken(User user);
    }
}