using Common.Domain.Multi;

namespace Membership.Application.Events
{
    public class UserCreatedEventHandler
    {
        private readonly IMailService mailService;

        public UserCreatedEventHandler(IMailService mailService)
        {
            this.mailService = mailService;
        }
    }
}
