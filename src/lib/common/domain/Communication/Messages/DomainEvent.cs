using Common.Domain.ValueObject;

namespace Common.Domain.Communication.Messages
{
    public class DomainEvent : Event
    {
        public DomainEvent(Id aggregateId)
        {
            AggregateId = aggregateId;
        }
    }
}
