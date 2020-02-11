using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestApi.BL.DTOs;

namespace TestApi.BL.Services.Interfaces
{
    public interface IProductTransactionService
    {
        Task<IEnumerable<TransactionOutcomeDTO>> CreateTransactionAsync(int productId, TransactionDTO transactionDTO);
        IEnumerable<TransactionOutcomeDTO> GetTransactions(int productId);
    }
}
