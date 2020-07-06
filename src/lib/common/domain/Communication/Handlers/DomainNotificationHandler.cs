using Common.Domain.Communication.Messages;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Domain.Communication.Handlers
{
    public class DomainNotificationHandler : INotificationHandler<DomainNotification>
    {
        private List<DomainNotification> notifications;

        public DomainNotificationHandler()
        {
            notifications = new List<DomainNotification>();
        }

        public Task Handle(DomainNotification message, CancellationToken cancellationToken)
        {
            notifications.Add(message);
            return Task.CompletedTask;
        }

        public virtual List<DomainNotification> GetNotifications()
        {
            return notifications;
        }

        public virtual bool HasNotification()
        {
            return GetNotifications().Any();
        }

        public void Dispose()
        {
            notifications = new List<DomainNotification>();
        }
    }
}
