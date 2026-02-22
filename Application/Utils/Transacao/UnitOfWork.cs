using Microsoft.EntityFrameworkCore.Storage;
using Repository.ContextEFs;

namespace Application.Utils.Transacao
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ContextEF _context;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(ContextEF context)
        {
            _context = context;
        }

        public async Task BeginTransactionAsync()
        {
            try 
            {
                if (_transaction != null)
                    return;

                _transaction = await _context.Database.BeginTransactionAsync();
            }
            catch (Exception ex) 
            {
                throw;
            }
            
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await DisposeTransactionAsync();
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await DisposeTransactionAsync();
            }
        }

        private async Task DisposeTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }
}
