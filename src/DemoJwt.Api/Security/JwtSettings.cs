using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;

namespace DemoJwt.Api.Security
{
    public class JwtSettings
    {
        public string Audience { get; }
        public string Issuer { get; }
        public int ValidForMinutes { get; }
        public SigningCredentials SigningCredentials { get; }

        public DateTime NotBefore => DateTime.UtcNow;
        public DateTime IssuedAt => DateTime.UtcNow;
        public TimeSpan ValidFor => TimeSpan.FromMinutes(ValidForMinutes);
        public DateTime Expiration => IssuedAt.AddMinutes(ValidFor.TotalMinutes);

        public Func<Task<string>> JtiGenerator => () => Task.FromResult(Guid.NewGuid().ToString());

        public JwtSettings(IConfiguration configuration)
        {

            //var symmetricSecurityKey = new SymmetricSecurityKey(WebEncoders.Base64UrlDecode(key));

            Issuer = configuration["JwtSettings:Issuer"];
            Audience = configuration["JwtSettings:Audience"];
            ValidForMinutes = Convert.ToInt32(configuration["JwtSettings:ValidForMinutes"]);

            var signingKey = configuration["JwtSettings:SigningKey"];
            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
            SigningCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256Signature);
        }

    }
}