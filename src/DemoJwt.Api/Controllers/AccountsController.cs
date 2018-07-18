using DemoJwt.Application.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DemoJwt.Api.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Cria um novo usuário
        /// </summary>
        /// <param name="command">Informações do usuário</param>
        /// <returns>Informações básicas do usuário recém criado</returns>
        [HttpPost, AllowAnonymous, Route("register")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUser command)
        {
            var response = await _mediator.Send(command);
            if (response.HasMessages)
            {
                return BadRequest(response.Messages);
            }

            return Created("accounts/profile", response.Value);
        }

        /// <summary>
        /// Autentica um usuário na api
        /// </summary>
        /// <param name="command">Informações de login</param>
        /// <returns>Token JWT</returns>
        [HttpPost, AllowAnonymous, Route("login")]
        public async Task<IActionResult> Authenticate([FromBody] Authenticate command)
        {
            var response = await _mediator.Send(command);
            if (response.HasMessages)
            {
                return BadRequest(response.Messages);
            }

            return Ok(response.Value);
        }

        /// <summary>
        /// Obtém os dados do usuário logado
        /// </summary>
        /// <returns>Dados do usuário logado</returns>
        [HttpGet, Route("profile")]
        public async Task<IActionResult> Get()
        {
            var response = await _mediator.Send(new QueryUserProfile());

            if (response == null)
            {
                return NoContent();
            }

            return Ok(response.Value);
        }

        /// <summary>
        /// Altera a senha do usuário logado
        /// </summary>
        /// <param name="command">Comando de alteração de senha</param>
        /// <returns>Mensagem de sucesso ou de erro</returns>
        [HttpPut, Route("profile/password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangeUserPassword command)
        {
            var response = await _mediator.Send(command);

            if (response.HasMessages)
            {
                return BadRequest(response.Messages);
            }

            return Ok(response.Value);
        }
    }
}
