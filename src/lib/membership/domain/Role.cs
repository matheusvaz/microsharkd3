using Common.Domain.Data;
using Common.Domain.Multi;
using System.Collections.Generic;
using System.Linq;

namespace Membership.Domain
{
    public class Role : Entity
    {
        public string Name { get; private set; }

        private ICollection<UserRole> _userRoles = new List<UserRole>();
        public IReadOnlyCollection<UserRole> UserRoles => _userRoles.ToArray();

        public Role(string name)
        {
            Name = name;
            Validate();
        }

        protected Role() { }

        public override string ToString()
        {
            return Id.ToString();
        }
        
        private void Validate()
        {
            Assert.NotEmpty(Name, Translation.Key("MEMBERSHIP_ROLE.NAME_NOT_EMPTY"));
            Assert.MaxLength(Name, FieldSize["Name"], Translation.Key("MEMBERSHIP_ROLE.NAME_MAXIMUM_LENGTH", false, FieldSize["Name"]));
        }

        public static IDictionary<string, int> FieldSize => new Dictionary<string, int>
        {
            { "Name", 64 }
        };
    }
}
