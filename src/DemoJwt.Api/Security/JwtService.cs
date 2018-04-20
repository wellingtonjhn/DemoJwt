using DemoJwt.Application.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace DemoJwt.Api.Security
{
    public class JwtService : IJwtService
    {
        private readonly JwtSettings _settings;

        public JwtService(JwtSettings settings)
        {
            _settings = settings;
        }

        public object CreateJwtToken(User user)
        {
            var identity = GetClaimsIdentity(user);
            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = identity,
                Issuer = _settings.Issuer,
                Audience = _settings.Audience,
                IssuedAt = _settings.IssuedAt,
                NotBefore = _settings.NotBefore,
                Expires = _settings.Expiration,
                SigningCredentials = _settings.SigningCredentials
            });

            var encodedJwt = handler.WriteToken(securityToken);

            return new
            {
                access_token = encodedJwt,
                token_type = JwtBearerDefaults.AuthenticationScheme.ToLower(),
                expires_in = (int)_settings.ValidFor.TotalSeconds,
            };
        }

        private ClaimsIdentity GetClaimsIdentity(User user)
        {
            return new ClaimsIdentity
            (
                new GenericIdentity(user.Email),
                new[] {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                    new Claim(JwtRegisteredClaimNames.Iat, $"{ToUnixEpochDate(_settings.IssuedAt)}", ClaimValueTypes.Integer64)
                }
            );
        }

        /// <summary>
        /// Converte a data em fomato UNIX
        /// </summary>
        /// <param name="date">Objeto de Data e Hora</param>
        /// <returns></returns>
        private static long ToUnixEpochDate(DateTime date)
        {
            return (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
        }
    }
}