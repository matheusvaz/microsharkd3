using Common.Domain.ValueObject;
using System;

namespace Common.Domain.Communication.Messages
{
    public class StoredEvent
    {
        public Id Id { get; private set; }
        public string Type { get; private set; }
        public DateTime DateOcurred { get; set; }
        public string Data { get; private set; }

        public StoredEvent(Id id, string type, DateTime dateOcurred, string data)
        {
            Id = id;
            Type = type;
            DateOcurred = dateOcurred;
            Data = data;
        }
    }
}
