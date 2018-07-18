using DemoJwt.Application.Core;
using Flunt.Notifications;
using Flunt.Validations;

namespace DemoJwt.Application.Requests
{
    public class Authenticate : Request<Response>
    {
        public string Email { get; }
        public string Password { get; }
        public string GrantType { get; }
        public string RefreshToken { get; }

        public Authenticate(string grantType, string email, string password, string refreshToken)
        {
            Validate(grantType, email, password, refreshToken);

            GrantType = grantType;
            Email = email;
            Password = password;
            RefreshToken = refreshToken;
        }

        private void Validate(string grantType, string email, string password, string refreshToken)
        {
            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrEmpty(grantType, nameof(grantType), "O tipo de autenticação não pode ficar vazio"));

            if (!string.IsNullOrEmpty(grantType))
            {
                if (grantType.Equals("password"))
                {
                    AddNotifications(new Contract()
                        .Requires()
                        .IsEmail(email, nameof(email), "E-mail inválido")
                        .IsNotNullOrEmpty(password, nameof(password), "A senha não pode ficar vazia"));

                }
                else if (grantType.Equals("refresh_token"))
                {
                    AddNotifications(new Contract()
                        .Requires()
                        .IsNotNullOrEmpty(refreshToken, nameof(refreshToken), "O refresh token não pode ficar vazio"));
                }
                else
                {
                    AddNotification(new Notification(nameof(grantType), "Tipo de autenticação inválido"));
                }
            }
        }
    }
}