using DemoJwt.Application.Core;
using Flunt.Validations;

namespace DemoJwt.Application.Requests
{
    public class ChangeUserPassword : Request<Response>
    {
        public string NewPassword { get; }
        public string NewPasswordConfirmation { get; }

        public ChangeUserPassword(string newPassword, string newPasswordConfirmation)
        {
            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrEmpty(newPassword, nameof(newPassword), "A senha não pode ficar vazia")
                .IsNotNullOrEmpty(newPasswordConfirmation, nameof(newPasswordConfirmation), "A confirmação de senha não pode ficar vazia")
                .AreEquals(newPassword, newPasswordConfirmation, "PasswordConfirmation", "As senhas não conferem")
            );

            NewPassword = newPassword;
            NewPasswordConfirmation = newPasswordConfirmation;
        }
    }
}