using System;

namespace DemoJwt.Application.Models
{
    public class User
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; }
        public string Email { get; }

        public User(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}