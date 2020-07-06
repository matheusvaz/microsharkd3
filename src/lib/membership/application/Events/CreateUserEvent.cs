using Common.Domain.Communication.Messages;
using Common.Domain.ValueObject;

namespace Membership.Application.Events
{
    public class UserCreatedEvent : Event
    {
        public Id UserId { get; private set; }

        public UserCreatedEvent(Id userId)
        {
            AggregateId = userId;
            UserId = userId;
        }
    }
}
