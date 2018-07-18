using DemoJwt.Application.Contracts;
using DemoJwt.Application.Models;
using System.Linq;
using System.Threading.Tasks;

namespace DemoJwt.Repository
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly InMemoryDatabaseContext _context;

        public RefreshTokenRepository(InMemoryDatabaseContext context)
        {
            _context = context;
        }

        public async Task<RefreshToken> Get(string refreshToken)
        {
            var user = _context.RefreshTokens
                .FirstOrDefault(a => a.Token.Equals(refreshToken));

            return await Task.FromResult(user);
        }

        public async Task Save(RefreshToken refreshToken)
        {
            var currentRefreshToken = _context.RefreshTokens
                .FirstOrDefault(a => a.Username.Equals(refreshToken.Username));

            if (currentRefreshToken != null)
            {
                _context.RefreshTokens.Remove(currentRefreshToken);
            }

            _context.RefreshTokens.Add(refreshToken);

            await Task.CompletedTask;
        }
    }
}