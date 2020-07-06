using Common.Domain.Multi;
using System;

namespace Common.Domain.ValueObject
{
    public class Id
    {
        public string Identifier { get; private set; } 

        public Id()
        {
            Identifier = Guid.NewGuid().ToString().Replace("-", string.Empty);
        }

        public Id(Guid id)
        {
            Identifier = id.ToString().Replace("-", string.Empty);
        }

        public Id(string id)
        {
            Identifier = id;
            Validate();
        }

        public override string ToString()
        {
            return Identifier;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = GetType().GetHashCode();
                hash = (hash * 31) ^ Identifier.GetHashCode();

                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            var other = obj as dynamic;

            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Identifier == other.Identifier;
        }

        public static int FieldSize => 32;

        private void Validate()
        {
            Assert.Length(Identifier, FieldSize, Translation.Key("ERROR.ID_FORMAT", true));
        }
    }
}
