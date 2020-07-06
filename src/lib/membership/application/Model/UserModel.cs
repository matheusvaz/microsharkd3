using Membership.Domain;
using System.Collections.Generic;

namespace Membership.Application.Model
{
    public class UserModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserStatus Status { get; set; }
        public Dictionary<string, string> Claims { get; set; }
        public List<string> Roles { get; set; }
    }
}
