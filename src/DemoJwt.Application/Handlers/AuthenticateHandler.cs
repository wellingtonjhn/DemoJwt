using DemoJwt.Application.Contracts;
using DemoJwt.Application.Core;
using DemoJwt.Application.Models;
using DemoJwt.Application.Requests;
using Flunt.Notifications;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DemoJwt.Application.Handlers
{
    public class AuthenticateHandler : IRequestHandler<Authenticate, Response>
    {
        private readonly IJwtService _jwtService;
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        private Response _response;

        public AuthenticateHandler(
            IJwtService jwtService,
            IUserRepository userRepository,
            IRefreshTokenRepository refreshTokenRepository)
        {
            _jwtService = jwtService;
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<Response> Handle(Authenticate request, CancellationToken cancellationToken)
        {
            _response = new Response();

            User user = null;

            if (request.GrantType.Equals("password"))
            {
                user = await HandleUserAuthentication(request);
            }
            else if (request.GrantType.Equals("refresh_token"))
            {
                user = await HandleRefreshToken(request);
            }

            if (_response.HasMessages || user == null)
            {
                return _response;
            }

            await HandleJwt(user);

            return _response;
        }

        private async Task HandleJwt(User user)
        {
            var jwt = _jwtService.CreateJsonWebToken(user);
            await _refreshTokenRepository.Save(jwt.RefreshToken);

            _response.AddValue(new
            {
                access_token = jwt.AccessToken,
                refresh_token = jwt.RefreshToken.Token,
                token_type = jwt.TokenType,
                expires_in = jwt.ExpiresIn
            });
        }

        private async Task<User> HandleUserAuthentication(Authenticate request)
        {
            var encodedPassword = new Password(request.Password).Encoded;
            var user = await _userRepository.Authenticate(request.Email, encodedPassword);

            if (user == null)
            {
                _response.AddNotification(new Notification("user", "Usuário ou senha inválidos"));
            }

            return user;
        }

        private async Task<User> HandleRefreshToken(Authenticate request)
        {
            var token = await _refreshTokenRepository.Get(request.RefreshToken);

            if (token == null)
            {
                _response.AddNotification(new Notification(nameof(request.RefreshToken), "Refresh Token inválido"));
            }
            else if (token.ExpirationDate < DateTime.Now)
            {
                _response.AddNotification(new Notification(nameof(request.RefreshToken), "Refresh Token expirado"));
            }

            if (_response.HasMessages)
            {
                return null;
            }

            return await _userRepository.Get(token.Username);
        }
    }
}