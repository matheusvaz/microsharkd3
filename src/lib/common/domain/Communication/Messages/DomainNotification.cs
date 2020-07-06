using Common.Domain.ValueObject;
using MediatR;
using System;

namespace Common.Domain.Communication.Messages
{
    public class DomainNotification : Message, INotification
    {
        public DateTime Timespamp { get; private set; }
        public Id DomainNotificationId { get; private set; }
        public string Key { get; private set; }
        public string Value { get; private set; }
        public int Version { get; private set; }

        public DomainNotification(string key, string value)
        {
            Timespamp = DateTime.UtcNow;
            DomainNotificationId = new Id();
            Version = 1;
            Key = key;
            Value = value;
        }
    }
}
