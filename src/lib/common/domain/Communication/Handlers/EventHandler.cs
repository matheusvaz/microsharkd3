using Common.Domain.Communication.Messages;
using Common.Domain.Data;
using MediatR;
using System.Threading.Tasks;

namespace Common.Domain.Communication.Handlers
{
    public class EventHandler
    {
        private readonly IMediator mediator;
        private readonly IEventSourcingRepository eventSourcingRepository;

        public EventHandler(IMediator mediator, IEventSourcingRepository eventSourcingRepository)
        {
            this.mediator = mediator;
            this.eventSourcingRepository = eventSourcingRepository;
        }

        public async Task PublishEvent<T>(T @event) where T : Event
        {
            await mediator.Publish(@event);

            if (!@event.GetType().BaseType.Name.Equals("DomainEvent"))
            {
                await eventSourcingRepository.Save(@event);
            }
        }

        public async Task PublishNotification<T>(T notification) where T : DomainNotification
        {
            await mediator.Publish(notification);
        }

        public async Task<bool> SendCommand<T>(T command) where T : Command
        {
            return await mediator.Send(command);
        }
    }
}
