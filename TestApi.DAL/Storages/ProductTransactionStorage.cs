using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApi.DAL.Entities;
using TestApi.DAL.Storages.Interfaces;

namespace TestApi.DAL.Storages
{
    public class ProductTransactionStorage : IProductTransactionStorage
    {
        private readonly AppDbContext _dbContext;

        public ProductTransactionStorage(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<TransactionEntity> GetTransactions(int productId)
        {
            return _dbContext.TransactionSet.Include(x => x.Product)
                                            .Include(x => x.User)
                                            .Where(x => x.ProductId == productId).ToList();
        }

        public async Task CreateTransactionAsync(TransactionEntity transaction)
        {
            _dbContext.TransactionSet.Add(transaction);
            await _dbContext.SaveChangesAsync();
        }

    }
}
