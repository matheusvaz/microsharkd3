using Common.Domain.Communication.Handlers;
using Common.Domain.Communication.Messages;
using Common.Domain.ValueObject;
using IdentityModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Api.Controllers
{
    public class Controller : ControllerBase
    {
        private readonly DomainNotificationHandler notifications;
        private readonly EventHandler eventHandler;

        public bool Valid
        {
            get
            {
                return !notifications.HasNotification();
            }
        }

        public bool Invalid
        {
            get
            {
                return notifications.HasNotification();
            }
        }

        protected Id UserId
        {
            get
            {
                var sub = HttpContext.User.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Subject)?.Value;
                return !string.IsNullOrEmpty(sub) ? new Id(sub) : null;
            }
        }

        protected EventHandler EventHandler => eventHandler;

        public Controller(INotificationHandler<DomainNotification> notifications, EventHandler eventHandler)
        {
            this.notifications = (DomainNotificationHandler)notifications;
            this.eventHandler = eventHandler;
        }

        public Dictionary<string, List<string>> GetNotifications()
        {
            return notifications.GetNotifications()
                .GroupBy(g => g.Key)
                .ToDictionary(d => d.Key, d => d.Select(x => x.Value).ToList());
        }

        protected IActionResult Created()
        {
            return StatusCode((int)HttpStatusCode.Created);
        }
    }
}
