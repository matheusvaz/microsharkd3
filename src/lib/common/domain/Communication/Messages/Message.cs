using Common.Domain.ValueObject;

namespace Common.Domain.Communication.Messages
{
    public abstract class Message
    {
        public string MessageType { get; private set; }
        public Id AggregateId { get; protected set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}
