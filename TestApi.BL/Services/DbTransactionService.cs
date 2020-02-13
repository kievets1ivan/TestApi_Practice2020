using Microsoft.EntityFrameworkCore.Storage;
using System;
using TestApi.BL.Services.Interfaces;
using TestApi.DAL;

namespace TestApi.BL.Services
{
    public class DbTransactionService : IDbTransactionService
    {

        private IDbContextTransaction _transaction;
        private readonly AppDbContext _dbContext;
        private bool _disposed = false;

        public DbTransactionService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void BeginTransaction()
        {
            _transaction = _dbContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void RollBack()
        {
            _transaction.Rollback();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing && _transaction != null)
            {
                _transaction.Dispose();
            }

            _disposed = true;
        }

    }
}
