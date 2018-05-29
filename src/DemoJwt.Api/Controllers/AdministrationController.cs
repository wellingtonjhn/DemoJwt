using DemoJwt.Application.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DemoJwt.Api.Controllers
{
    [Route("api/[controller]"), Authorize(Roles = "Administrator")]
    public class AdministrationController : Controller
    {
        private readonly IMediator _mediator;

        public AdministrationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Lista todos os usuários cadastrados
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("accounts")]
        public async Task<IActionResult> ListUsers()
        {
            var command = new QueryAllUsers();
            var response = await _mediator.Send(command);

            return Ok(response.Value);
        }

        /// <summary>
        /// Deleta um usuário
        /// </summary>
        /// <param name="accountId">Id do usuário</param>
        /// <returns></returns>
        [HttpDelete, Route("accounts/{accountId}"), Authorize(Policy = "DeleteUserPolicy")]
        public async Task<IActionResult> DeleteAccount(Guid accountId)
        {
            var command = new RemoveAccount(accountId);
            var response = await _mediator.Send(command);

            if (response.HasMessages)
            {
                return BadRequest(response.Messages);
            }
            return Ok();
        }
    }
}