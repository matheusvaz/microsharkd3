using Common.Domain.Communication.Messages;
using Common.Domain.ValueObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Domain.Data
{
    public interface IEventSourcingRepository
    {
        Task Save<TEvent>(TEvent @event) where TEvent : Event;
        Task<IEnumerable<StoredEvent>> Get(Id aggregateId, int start = 0, int count = 500);
    }
}
