using Flunt.Notifications;
using Flunt.Validations;
using Newtonsoft.Json;
using System;

namespace DemoJwt.Application.Models
{
    public class User : Notifiable
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; }
        public string Email { get; }

        [JsonIgnore]
        public Password Password { get; private set; }

        protected User() { }

        public User(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = new Password(password);
        }

        public void ChangePassword(string newPassword, string newPasswordConfirmation)
        {
            AddNotifications(new Contract()
                .Requires()
                .AreEquals(newPassword, newPasswordConfirmation, "PasswordConfirmation", "As senhas não conferem")
            );

            Password = new Password(newPassword);
        }
    }
}