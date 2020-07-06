using Common.Domain.Communication.Handlers;
using Common.Domain.Communication.Messages;
using MediatR;
using Membership.Application.Commands.CreateUser;
using Membership.Application.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers.Membership
{
    [ApiController]
    [Route("v{version:apiVersion}/{lang}/membership/user")]
    [Authorize]
    public class UserController : Controller
    {
        public UserController(INotificationHandler<DomainNotification> notifications,
                              EventHandler eventHandler) : base(notifications, eventHandler) { }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser(UserModel model)
        {
            var command = new CreateUserCommand(model?.FirstName, model?.LastName, model.Username, model.Email, model.Password);
            await EventHandler.SendCommand(command);

            return Created();
        }

       
    }
}
