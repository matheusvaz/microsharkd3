using Common.Domain.Multi;
using FluentValidation;
using Membership.Domain;

namespace Membership.Application.Commands.CreateUser
{
    public class CreateUserCommandValidation : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidation()
        {
            RuleFor(p => p.FirstName)
                .NotEmpty()
                .WithMessage(Translation.Key("CREATE_USER_COMMAND.FIRSTNAME_NOT_EMPTY"));

            RuleFor(p => p.FirstName)
                .MaximumLength(User.FieldSize["FirstName"])
                .WithMessage(Translation.Key("CREATE_USER_COMMAND.FIRST_NAME_MAXIMUM_LENGTH", false, User.FieldSize["FirstName"]));

            RuleFor(p => p.LastName)
                .NotEmpty()
                .WithMessage(Translation.Key("CREATE_USER_COMMAND.LASTNAME_NOT_EMPTY"));

            RuleFor(p => p.FirstName)
                .MaximumLength(User.FieldSize["LastName"])
                .WithMessage(Translation.Key("CREATE_USER_COMMAND.LASTNAME_MAXIMUM_LENGTH", false, User.FieldSize["LastName"]));

            RuleFor(p => p.Username)
                .NotEmpty()
                .WithMessage(Translation.Key("CREATE_USER_COMMAND.USERNAME_NOT_EMPTY"));

            RuleFor(p => p.Username)
                .MaximumLength(Username.FieldSize)
                .WithMessage(Translation.Key("CREATE_USER_COMMAND.USERNAME_MAXIMUM_LENGTH", false, Username.FieldSize));

            RuleFor(p => p.Email)
                .NotEmpty()
                .WithMessage(Translation.Key("CREATE_USER_COMMAND.EMAIL_NOT_EMPTY"));

            RuleFor(p => p.Email)
                .MaximumLength(Email.FieldSize)
                .WithMessage(Translation.Key("CREATE_USER_COMMAND.EMAIL_MAXIMUM_LENGTH", false, Email.FieldSize));

            RuleFor(p => p.Email)
                .EmailAddress()
                .WithMessage(Translation.Key("CREATE_USER_COMMAND.EMAIL"));

            RuleFor(p => p.Password)
               .NotEmpty()
               .WithMessage(Translation.Key("CREATE_USER_COMMAND.PASSWORD_NOT_EMPTY"));
        }
    }
}
