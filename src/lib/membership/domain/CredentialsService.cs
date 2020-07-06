using Common.Domain.Multi;
using System.Threading.Tasks;

namespace Membership.Domain
{
    public class CredentialsService
    {
        private readonly IUserRepository userRepository;
        private readonly ICryptography cryptography;

        public CredentialsService(IUserRepository userRepository, ICryptography cryptography)
        {
            this.userRepository = userRepository;
            this.cryptography = cryptography;
        }

        public async Task<bool> VerifyCredentials(Username username, string password)
        {
            var user = await userRepository.FindUser(username);
            return VerifyHashedPassword(user, password);
        }

        public async Task<bool> VerifyCredentials(Email email, string password)
        {
            var user = await userRepository.FindUser(email);
            return VerifyHashedPassword(user, password);
        }

        public Task<bool> VerifyCredentials(PhoneNumber email, string code)
        {
            throw new System.NotImplementedException();
        }

        private bool VerifyHashedPassword(User user, string password)
        {
            if (user is null) return false;
            return cryptography.VerifyHashedPassword(password, user.Password.ToString());
        }
    }
}
