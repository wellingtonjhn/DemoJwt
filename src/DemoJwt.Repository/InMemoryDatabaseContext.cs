using DemoJwt.Application.Models;
using System.Collections.Generic;

namespace DemoJwt.Repository
{
    public class InMemoryDatabaseContext
    {
        public ISet<User> Users { get; } = new HashSet<User>();
        public ISet<RefreshToken> RefreshTokens { get; } = new HashSet<RefreshToken>();
    }
}