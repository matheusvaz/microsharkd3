using Common.Domain.Communication.Messages;
using Common.Domain.Data;
using Common.Domain.ValueObject;
using Common.Infra.Data.Dapper;
using Dapper;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Infra.Data.EventSourcing
{
    public class EventSourcingRepository : IEventSourcingRepository
    {
        private readonly ConnectionScope scope;

        public EventSourcingRepository(ConnectionScope scope)
        {
            this.scope = scope;
        }

        public async Task<IEnumerable<StoredEvent>> Get(Id aggregateId, int start = 0, int count = 500)
        {
            return await scope.Connection.QueryAsync<StoredEvent>("SELECT Id, Type, DateOccurred, Data FROM event WHERE Id = @Id", new
            {
                Id = aggregateId.ToString()
            });
        }

        public async Task Save<TEvent>(TEvent @event) where TEvent : Event
        {
            var storedEvent = new StoredEvent(
                new Id(),
                @event.MessageType,
                @event.Timestamp,
                JsonConvert.SerializeObject(@event)
            );
                        
            await scope.Connection.ExecuteAsync(@"INSERT INTO event SET Id = @Id, Type = @Type, DateOccurred = @DateOccurred, Data = @Data", new
            {
                Id = storedEvent.Id.ToString(),
                Type = storedEvent.Type,
                DateOccurred = storedEvent.DateOcurred,
                Data = storedEvent.Data
            });
        }
    }
}
