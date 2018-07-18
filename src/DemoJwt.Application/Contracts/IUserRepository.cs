using DemoJwt.Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoJwt.Application.Contracts
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> Get(Guid id);
        Task<User> Get(string email);
        Task<User> Authenticate(string email, string password);
        Task<bool> ExistsUser(string email);
        Task Save(User user);
        Task Remove(User user);
    }

    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> Get(string username);
        Task Save(RefreshToken refreshToken);
    }
}