using DemoJwt.Application.Models;

namespace DemoJwt.Application.Contracts
{
    public interface IJwtService
    {
        object CreateJwtToken(User user);
    }
}