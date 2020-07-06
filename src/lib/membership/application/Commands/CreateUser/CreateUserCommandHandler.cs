using Common.Domain.Communication.Handlers;
using Common.Domain.Multi;
using MediatR;
using Membership.Application.Events;
using Membership.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace Membership.Application.Commands.CreateUser
{
    public class CreateUserCommandHandler : CommandHandler, IRequestHandler<CreateUserCommand, bool>
    {
        private readonly IUserRepository userRepository;
        private readonly ICryptography cryptography;

        public CreateUserCommandHandler(IUserRepository userRepository,
                                        EventHandler eventHandler,
                                        ICryptography cryptography) : base(eventHandler)
        {
            this.userRepository = userRepository;
            this.cryptography = cryptography;
        }

        public async Task<bool> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            if (!await Validate(command)) return false;

            var username = new Username(command.Username);
            var email = new Email(command.Email);
            var userByUserName = await userRepository.FindUser(username);
            var userByEmail = await userRepository.FindUser(email);

            if (userByUserName != null && userByEmail != null) return true;

            userRepository.UnitOfWork.BeginTransaction();

            var user = new User(
                command.FirstName,
                command.LastName,
                username,
                email,
                new Password(command.Password, cryptography)
            );

            await userRepository.Add(user);

            user.AddEvent(new UserCreatedEvent(user.Id));

            return await userRepository.UnitOfWork.Commit();
        }
    }
}
