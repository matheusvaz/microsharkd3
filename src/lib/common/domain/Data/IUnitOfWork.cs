using System.Threading.Tasks;

namespace Common.Domain.Data
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        Task<bool> Commit();
        Task<bool> Rollback();
    }
}
