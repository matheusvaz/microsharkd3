using Common.Domain.Data;
using Common.Domain.ValueObject;
using System.Threading.Tasks;

namespace Membership.Domain
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> FindUser(Id userId);
        Task<User> FindUser(Username username);
        Task<User> FindUser(Email email);
        Task<User> FindUser(PhoneNumber phoneNumber);
        Task Add(User user);
        Task Update(User user);
    }
}
