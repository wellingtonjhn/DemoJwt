using DemoJwt.Application.Core;
using Flunt.Validations;
using System.Collections.Generic;
using System.Linq;

namespace DemoJwt.Application.Requests
{
    public class CreateUser : Request<Response>
    {
        private readonly List<string> _roles = new List<string>();
        private readonly List<string> _policies = new List<string>();

        public string Name { get; }
        public string Email { get; }
        public string Password { get; }
        public IEnumerable<string> Roles => _roles;
        public IEnumerable<string> Policies => _policies;

        public CreateUser(string name, string email, string password, IEnumerable<string> roles, IEnumerable<string> policies)
        {
            AddNotifications(new Contract()
                .Requires()
                .IsEmail(email, nameof(email), "E-mail inválido")
                .IsNotNullOrEmpty(name, nameof(name), "O nome não pode ficar vazio")
                .IsNotNullOrEmpty(password, nameof(password), "A senha não pode ficar vazia")
            );

            Name = name;
            Email = email;
            Password = password;

            if (roles != null)
            {
                _roles.AddRange(roles);
            }

            if (policies != null)
            {
                _policies.AddRange(policies);
            }
        }
    }
}