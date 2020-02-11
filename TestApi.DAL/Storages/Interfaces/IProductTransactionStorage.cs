using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestApi.DAL.Entities;

namespace TestApi.DAL.Storages.Interfaces
{
    public interface IProductTransactionStorage
    {
        Task CreateTransactionAsync(TransactionEntity transaction);
        IEnumerable<TransactionEntity> GetTransactions(int productId);
    }
}
