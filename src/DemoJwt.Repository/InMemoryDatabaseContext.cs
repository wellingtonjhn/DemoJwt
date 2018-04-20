using DemoJwt.Application.Models;
using System.Collections.Generic;

namespace DemoJwt.Repository
{
    public  class InMemoryDatabaseContext
    {
        public IList<User> Users { get; } = new List<User>();
    }
}