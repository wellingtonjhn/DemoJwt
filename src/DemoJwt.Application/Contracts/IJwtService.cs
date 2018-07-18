using DemoJwt.Application.Models;

namespace DemoJwt.Application.Contracts
{
    public interface IJwtService
    {
        JsonWebToken CreateJsonWebToken(User user);
    }
}