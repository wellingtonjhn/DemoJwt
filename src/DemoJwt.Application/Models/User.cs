using Flunt.Notifications;
using Flunt.Validations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DemoJwt.Application.Models
{
    public class User : Notifiable
    {
        private readonly IList<string> _roles = new List<string>();
        private readonly IList<string> _permissions = new List<string>();

        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; }
        public string Email { get; }
        public IEnumerable<string> Roles => _roles;
        public IEnumerable<string> Permissions => _permissions;

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
                .IsNotNullOrEmpty(newPassword, nameof(newPassword), "A senha não pode ficar vazia")
                .IsNotNullOrEmpty(newPasswordConfirmation, nameof(newPasswordConfirmation), "A confirmação de senha não pode ficar vazia")
                .AreEquals(newPassword, newPasswordConfirmation, "PasswordConfirmation", "As senhas não conferem")
            );

            Password = new Password(newPassword);
        }

        public void AddRole(string role)
        {
            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrEmpty(role, nameof(role), "A role não pode ficar vazia")
            );

            if (Roles.Contains(role))
            {
                AddNotification(nameof(role), $"A role {role} já existe para este usuário");
                return;
            }

            _roles.Add(role);
        }

        public void AddPermission(string permission)
        {
            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrEmpty(permission, nameof(permission), "A permission não pode ficar vazia")
            );

            if (Permissions.Contains(permission))
            {
                AddNotification(nameof(permission), $"A permission {permission} já existe para este usuário");
                return;
            }

            _permissions.Add(permission);
        }
    }
}