using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DemoJwt.Application.Models
{
    public sealed class Password : Notifiable
    {
        public string Encoded { get; }

        public Password(string password)
        {
            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrEmpty(password, nameof(password), "A senha não pode ficar vazia")
            );

            Encoded = EncodePassword(password);
        }

        public static implicit operator Password(string password) => new Password(password);

        private static string EncodePassword(string password)
        {
            string result;
            var bytes = Encoding.Unicode.GetBytes(password);

            using (var stream = new MemoryStream())
            {
                stream.WriteByte(0);

                using (var sha256 = new SHA256Managed())
                {
                    var hash = sha256.ComputeHash(bytes);
                    stream.Write(hash, 0, hash.Length);

                    bytes = stream.ToArray();
                    result = Convert.ToBase64String(bytes);
                }

            }
            return result;
        }
    }
}