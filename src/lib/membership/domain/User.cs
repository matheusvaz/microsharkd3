using Common.Domain.Data;
using Common.Domain.Multi;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Membership.Domain
{
    public class User : Entity, IAggregateRoot
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Username Username { get; private set; }
        public Email Email { get; private set; }
        public Password Password { get; private set; }
        public UserStatus Status { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
        public bool TwoFactorEnabled { get; private set; }

        private ICollection<Claim> _claims = new List<Claim>();
        public virtual IReadOnlyCollection<Claim> Claims => _claims.ToArray();

        private ICollection<UserRole> _userRoles = new List<UserRole>();
        public virtual IReadOnlyCollection<UserRole> UserRoles => _userRoles.ToArray();

        public User(string firstName, string lastName, Username username, Email email, Password password)
        {
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Email = email;
            Password = password;
            Status = UserStatus.Inactive;
            TwoFactorEnabled = false;

            Validate();
        }

        protected User() { }

        public void AddPhoneNumber(PhoneNumber phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        public void ChangeName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = LastName;

            Validate();
        }

        public void EnableTwoFactorAuthentication()
        {
            TwoFactorEnabled = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public void DisableTwoFactorAuthentication()
        {
            TwoFactorEnabled = false;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Activate()
        {
            Status = UserStatus.Active;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Inactivate()
        {
            Status = UserStatus.Inactive;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Lock()
        {
            Status = UserStatus.Locked;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Suspend()
        {
            Status = UserStatus.Suspended;
            UpdatedAt = DateTime.UtcNow;
        }

        public static IDictionary<string, int> FieldSize => new Dictionary<string, int>
        {
            { "FirstName", 64 },
            { "LastName", 64 }
        };

        private void Validate()
        {
            Assert.NotEmpty(FirstName, Translation.Key("MEMBERSHIP_USER.FIRSTNAME_NOT_EMPTY"));
            Assert.MaxLength(FirstName, FieldSize["FirstName"], Translation.Key("MEMBERSHIP_USER.FIRST_NAME_MAXIMUM_LENGTH", false, FieldSize["FirstName"]));
            Assert.NotEmpty(LastName, Translation.Key("MEMBERSHIP_USER.LASTNAME_NOT_EMPTY"));
            Assert.MaxLength(LastName, FieldSize["LastName"], Translation.Key("MEMBERSHIP_USER.LASTNAME_MAXIMUM_LENGTH", false, FieldSize["LastName"]));
        }
    }
}
