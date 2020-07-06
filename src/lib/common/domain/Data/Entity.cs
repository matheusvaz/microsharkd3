using Common.Domain.Communication.Messages;
using Common.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Common.Domain.Data
{
    public abstract class Entity
    {
        public Id Id { get; protected set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; protected set; }
        public DateTime? DeletedAt { get; protected set; }

        private List<Event> _events;
        public IReadOnlyCollection<Event> Events => _events?.AsReadOnly();

        public Entity()
        {
            Id = new Id();
            CreatedAt = DateTime.UtcNow;
        }

        public void AddEvent(Event @event)
        {
            _events = _events ?? new List<Event>();
            _events.Add(@event);
        }

        public void RemoveEvent(Event @event)
        {
            _events?.Remove(@event);
        }

        public void ClearEvents()
        {
            _events?.Clear();
        }

        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo))
            {
                return true;
            }

            return compareTo is null ? false : Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (a is null && b is null)
            {
                return true;
            }

            return a is null || b is null ? false : a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            var byteArray = new byte[4];
            var provider = new RNGCryptoServiceProvider();

            provider.GetBytes(byteArray);

            return (GetType().GetHashCode() * BitConverter.ToInt32(byteArray, 0)) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"{GetType().Name} - {Id}";
        }
    }
}
