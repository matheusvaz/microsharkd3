using Common.Domain.Communication.Messages;
using System.Threading.Tasks;

namespace Common.Domain.Communication.Handlers
{
    public abstract class CommandHandler
    {
        private readonly EventHandler eventHandler;

        public CommandHandler(EventHandler eventHandler)
        {
            this.eventHandler = eventHandler;
        }

        protected virtual async Task<bool> Validate(Command command)
        {
            if (command.IsValid()) return true;

            foreach (var error in command.ValidationResult.Errors)
            {
                await eventHandler.PublishNotification(new DomainNotification(error.PropertyName, error.ErrorMessage));
            }

            return false;
        }
    }
}
