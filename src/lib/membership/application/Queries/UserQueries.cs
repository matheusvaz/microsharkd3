using Common.Domain.ValueObject;
using Membership.Application.Model;
using Membership.Domain;
using System.Linq;
using System.Threading.Tasks;

namespace Membership.Application.Queries
{
    public class UserQueries
    {
        private readonly IUserRepository userRepository;

        public UserQueries(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<UserModel> FindUser(Id userId)
        {
            var user = await userRepository.FindUser(userId);
            return MapUserModel(user);
        }

        public async Task<UserModel> FindUser(Email email)
        {
            var user = await userRepository.FindUser(email);
            return MapUserModel(user);
        }

        public async Task<UserModel> FindUser(Username username)
        {
            var user = await userRepository.FindUser(username);
            return MapUserModel(user);
        }

        public async Task<UserModel> FindUser(PhoneNumber phoneNumber)
        {
            var user = await userRepository.FindUser(phoneNumber);
            return MapUserModel(user);
        }

        private UserModel MapUserModel(User user)
        {
            if (user == null) return null;

            var model = new UserModel()
            {
                Id = user.Id.ToString(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email.ToString(),
                Username = user.Username.ToString(),
                Status = user.Status,
                Claims = user.Claims.ToDictionary(x => x.Name, x => x.Value),
                Roles = user.UserRoles.SelectMany(x => x.Roles.Select(y => y.Name)).ToList()
            };

            return model;
        }
    }
}
