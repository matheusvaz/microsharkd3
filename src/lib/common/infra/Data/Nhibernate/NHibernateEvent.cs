using Common.Domain.Communication.Handlers;
using Common.Domain.Data;
using NHibernate.Event;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Infra.Data.NHibernate
{
    public class NHibernateEvents :
        IPostInsertEventListener,
        IPostDeleteEventListener,
        IPostUpdateEventListener,
        IPostCollectionUpdateEventListener
    {
        private readonly EventHandler eventHandler;

        public NHibernateEvents(EventHandler eventHandler)
        {
            this.eventHandler = eventHandler;
        }

        public void OnPostInsert(PostInsertEvent @event) { }
        public async Task OnPostInsertAsync(PostInsertEvent @event, CancellationToken cancellationToken) => await DispatchEvents(@event);

        public void OnPostUpdate(PostUpdateEvent @event) { }
        public async Task OnPostUpdateAsync(PostUpdateEvent @event, CancellationToken cancellationToken) => await DispatchEvents(@event);

        public void OnPostUpdateCollection(PostCollectionUpdateEvent @event) { }
        public async Task OnPostUpdateCollectionAsync(PostCollectionUpdateEvent @event, CancellationToken cancellationToken) => await DispatchEvents(@event);

        public void OnPostDelete(PostDeleteEvent @event) { }
        public async Task OnPostDeleteAsync(PostDeleteEvent @event, CancellationToken cancellationToken) => await DispatchEvents(@event);

        private async Task DispatchEvents(AbstractEvent @event)
        {
            Entity entity = null;

            if (@event is AbstractPostDatabaseOperationEvent single) entity = single.Entity as Entity;
            if (@event is AbstractCollectionEvent collection) entity = collection.AffectedOwnerOrNull as Entity;

            if (entity.Events != null && entity.Events.Any())
            {
                foreach (var e in entity.Events)
                {
                    await eventHandler.PublishEvent(e);
                }

                entity.ClearEvents();
            }
        }
    }
}
