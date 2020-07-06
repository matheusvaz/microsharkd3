using Common.Domain.Data;
using System.Collections.Generic;
using System.Linq;

namespace Membership.Domain
{
    public class UserRole : Entity
    {
        private ICollection<User> _users = new List<User>();
        public virtual ICollection<User> Users => _users.ToArray();

        private ICollection<Role> _roles = new List<Role>();
        public virtual ICollection<Role> Roles => _roles.ToArray();

        protected UserRole() { }
    }
}
