using DemoJwt.Api.Settings;
using DemoJwt.Application.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace DemoJwt.Api.Extensions
{
    public static class SecurityExtensions
    {
        /// <summary>
        /// Converte a data em fomato UNIX
        /// </summary>
        /// <param name="date">Objeto de Data e Hora</param>
        /// <returns></returns>
        public static long ToUnixEpochDate(this DateTime date)
        {
            return (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
        }

        /// <summary>
        /// Converte a Data e Hora em formato Unix para String
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToUnixEpochDateToString(this DateTime date)
        {
            return $"{ToUnixEpochDate(date)}";
        }

        /// <summary>
        /// Cria um novo token JWT válido
        /// </summary>
        /// <param name="identity">Identidade do usuário</param>
        /// <param name="jwtSettings">Configurações do token JWT</param>
        /// <param name="signingSettings">Credênciais para assinatura do token</param>
        /// <returns>Um token JWT válido</returns>
        public static object CreateJwtToken(this ClaimsIdentity identity, JwtSettings jwtSettings, SigningSettings signingSettings)
        {
            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = identity,
                Issuer = jwtSettings.Issuer,
                Audience = jwtSettings.Audience,
                IssuedAt = jwtSettings.IssuedAt,
                NotBefore = jwtSettings.NotBefore,
                Expires = jwtSettings.Expiration,
                SigningCredentials = signingSettings.SigningCredentials
            });

            var encodedJwt = handler.WriteToken(securityToken);

            return new
            {
                access_token = encodedJwt,
                token_type = JwtBearerDefaults.AuthenticationScheme.ToLower(),
                expires_in = (int)jwtSettings.ValidFor.TotalSeconds,
            };
        }

        public static ClaimsIdentity GetClaimsIdentity(User user, JwtSettings jwtSettings)
        {
            return new ClaimsIdentity
            (
                new GenericIdentity(user.Id.ToString(), "Login"),
                new[] {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                    new Claim(JwtRegisteredClaimNames.Iat, jwtSettings.IssuedAt.ToUnixEpochDateToString(), ClaimValueTypes.Integer64),
                }
            );
        }
    }
}