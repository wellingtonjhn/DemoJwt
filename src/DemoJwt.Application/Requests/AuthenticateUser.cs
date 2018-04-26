using DemoJwt.Application.Core;
using DemoJwt.Application.Models;
using Flunt.Validations;

namespace DemoJwt.Application.Requests
{
    public class AuthenticateUser : Request<Response>
    {
        public string Email { get; }
        public string Password { get; }

        public AuthenticateUser(string email, string password)
        {
            AddNotifications(new Contract()
                .Requires()
                .IsEmail(email, nameof(email), "E-mail inválido")
                .IsNotNullOrEmpty(password, nameof(password), "A senha não pode ficar vazia"));

            Email = email;
            Password = password;

        }
    }
}