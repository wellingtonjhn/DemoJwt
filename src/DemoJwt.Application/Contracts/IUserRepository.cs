using System.Threading.Tasks;
using DemoJwt.Application.Models;

namespace DemoJwt.Application.Contracts
{
    public interface IUserRepository
    {
        Task<User> Get(string email);
        Task<User> Authenticate(string email, string password);
        Task<bool> ExistsUser(string email);
        Task Save(User user);
    }
}