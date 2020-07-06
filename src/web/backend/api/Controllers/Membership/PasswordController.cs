using Common.Domain.Communication.Handlers;
using Common.Domain.Communication.Messages;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Membership
{
    [ApiController]
    [Route("v{version:apiVersion}/{lang}/membership/password")]
    [Authorize]
    public class PasswordController : Controller
    {
        public PasswordController(INotificationHandler<DomainNotification> notifications,
                               EventHandler eventHandler) : base(notifications, eventHandler) { }
    }
}
