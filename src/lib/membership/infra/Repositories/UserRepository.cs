using Common.Domain.Data;
using Common.Domain.ValueObject;
using Membership.Domain;
using NHibernate;
using NHibernate.Linq;
using System.Linq;
using System.Threading.Tasks;

namespace Membership.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ISession session;
        private readonly IUnitOfWork unitOfWork;

        public UserRepository(ISession session, IUnitOfWork unitOfWork)
        {
            this.session = session;
            this.unitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork => unitOfWork;

        public async Task<User> FindUser(Id userId)
        {
            return await session.Query<User>()
                .Where(user => user.Id.ToString() == userId.ToString())
                .FirstOrDefaultAsync();
        }

        public async Task<User> FindUser(Username username)
        {
            return await session.Query<User>()
                .Where(user => user.Username.ToString() == username.ToString())
                .FirstOrDefaultAsync();
        }

        public async Task<User> FindUser(Email email)
        {
            return await session.Query<User>()
                .Where(user => user.Email.ToString() == email.ToString())
                .FirstOrDefaultAsync();
        }

        public async Task<User> FindUser(PhoneNumber phoneNumber)
        {
            return await session.Query<User>()
                .Where(user => user.PhoneNumber.ToString() == phoneNumber.ToString())
                .FirstOrDefaultAsync();
        }

        public async Task Add(User user)
        {
            await session.SaveAsync(user);
        }

        public async Task Update(User user)
        {
            await session.UpdateAsync(user);
        }

        public void Dispose()
        {
            session?.Dispose();
        }
    }
}
