using Common.Domain.Communication.Messages;
using FluentValidation.Results;

namespace Membership.Application.Commands.CreateUser
{
    public class CreateUserCommand : Command
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        
        public CreateUserCommand(string firstName, string lastName, string username, string email, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Email = email;
            Password = password;
        }

        public override bool IsValid()
        {
            ValidationResult = new CreateUserCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
