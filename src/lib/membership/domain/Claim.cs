using Common.Domain.Data;
using Common.Domain.Multi;
using Common.Domain.ValueObject;
using System.Collections.Generic;

namespace Membership.Domain
{
    public class Claim : Entity
    {
        public string Name { get; private set; }
        public string Value { get; private set; }
        public Id UserId { get; private set; }
        public User User { get; private set; }

        public Claim(string name, string value, User user)
        {
            Name = name;
            Value = value;
            User = user;
            UserId = user.Id;

            Validate();
        }

        protected Claim() { }

        public override string ToString()
        {
            return Id.ToString();
        }

        public static IDictionary<string, int> FieldSize => new Dictionary<string, int>
        {
            { "Name", 64 },
            { "Value", 64 }
        };

        private void Validate()
        {
            Assert.NotEmpty(Name, Translation.Key("MEMBERSHIP_CLAIMS.NAME_NOT_EMPTY"));
            Assert.MaxLength(Name, FieldSize["Name"], Translation.Key("MEMBERSHIP_CLAIMS.NAME_MAXIMUM_LENGTH", false, FieldSize["Name"]));
            Assert.NotEmpty(Value, Translation.Key("MEMBERSHIP_CLAIMS.VALUE_NOT_EMPTY"));
            Assert.MaxLength(Name, FieldSize["Value"], Translation.Key("MEMBERSHIP_CLAIMS.VALUE_MAXIMUM_LENGTH", false, FieldSize["Value"]));
        }
    }
}
