using DemoJwt.Application.Core;
using Flunt.Validations;

namespace DemoJwt.Application.Requests
{
    public class CreateUser : Request<Response>
    {
        public string Name { get; }
        public string Email { get; }
        public string Password { get; }

        public CreateUser(string name, string email, string password)
        {
            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrEmpty(name, nameof(name), "O nome não pode ficar vazio")
                .IsNotNullOrEmpty(password, nameof(password), "A senha não pode ficar vazia")
                .IsEmail(email, nameof(email), "E-mail inválido"));

            Name = name;
            Email = email;
            Password = password;
        }
    }
}