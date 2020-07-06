using Common.Domain.Data;
using Common.Domain.Multi;
using System.Threading.Tasks;
using INHibernateSession = NHibernate.ISession;
using INHibernateTransaction = NHibernate.ITransaction;

namespace Common.Infra.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly INHibernateSession session;        
        private INHibernateTransaction transaction;

        public UnitOfWork(INHibernateSession session)
        {
            this.session = session;            
        }

        public void BeginTransaction()
        {
            if (transaction != null)
            {
                throw new System.InvalidOperationException(Translation.Key("ERROR.TRANSACTION_ALREADY_OPENED", true));
            }

            transaction = session.BeginTransaction();
        }

        public async Task<bool> Commit()
        {
            try
            {
                if (transaction != null)
                {
                    await transaction.CommitAsync();
                }

                return true;
            }
            catch
            {
                if (transaction != null && transaction.IsActive)
                {
                    await transaction.RollbackAsync();
                }

                return false;
            }
            finally
            {
                transaction.Dispose();
                transaction = null;
            }
        }

        public async Task<bool> Rollback()
        {
            try
            {
                if (transaction != null && transaction.IsActive)
                {
                    await transaction.RollbackAsync();
                }

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                transaction.Dispose();
                transaction = null;
            }
        }
    }
}
