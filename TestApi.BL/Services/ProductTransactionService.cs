using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestApi.BL.DTOs;
using TestApi.BL.Exceptions;
using TestApi.BL.Services.Interfaces;
using TestApi.DAL.Entities;
using TestApi.DAL.Enums;
using TestApi.DAL.Storages.Interfaces;

namespace TestApi.BL.Services
{
    public class ProductTransactionService : IProductTransactionService
    {
        private readonly IProductStorage _productStorage;
        private readonly IProductTransactionStorage _productTransactionStorage;
        private readonly IMapper _mapper;
        private readonly IUserStorage _userStorage;
        private readonly IDbTransactionService _dbTransactionService;


        public ProductTransactionService(IProductStorage productStorage,
                                         IProductTransactionStorage productTransactionStorage,
                                         IMapper mapper,
                                         IUserStorage userStorage,
                                         IDbTransactionService transactionService)
        {
            _productStorage = productStorage;
            _productTransactionStorage = productTransactionStorage;
            _mapper = mapper;
            _userStorage = userStorage;
            _dbTransactionService = transactionService;
        }

        public IEnumerable<TransactionOutcomeDTO> GetTransactions(int productId)
        {
            return _mapper.Map<IEnumerable<TransactionOutcomeDTO>>(_productTransactionStorage.GetTransactions(productId));
        }


        public async Task<IEnumerable<TransactionOutcomeDTO>> CreateTransaction(int productId, TransactionDTO transactionDTO)
        {
            var product = await _productStorage.GetById(productId);

            ValidateTransaction(product.Quantity, transactionDTO);

            var transaction = BuildUpTransaction(productId, transactionDTO);

            await SaveProductTransaction(product, transaction);
            
            return GetTransactions(product.Id);
        }

        private async Task SaveProductTransaction(ProductEntity product, TransactionEntity transaction)
        {
            _dbTransactionService.BeginTransaction();
            try
            {
                await _productTransactionStorage.AddTransaction(transaction);

                await ChangeProductQuantityByTransaction(product, transaction);

                _dbTransactionService.Commit();

            }
            catch (Exception)
            {
                _dbTransactionService.RollBack();
                throw;
            }
            finally
            {
                _dbTransactionService.Dispose();
            }
        }

        private void ValidateTransaction(int productQuantity, TransactionDTO transactionDTO)
        {
            if (productQuantity < transactionDTO.Quantity 
                && transactionDTO.Operation == TypeOperation.Expense)
            {
                throw new AppValidationException($"Transaction quantity ({transactionDTO.Quantity}) can't be more product quantity ({productQuantity}) in case expense!");
            }
        }

        private TransactionEntity BuildUpTransaction(int productId, TransactionDTO transactionDTO)
        {
            var transaction = _mapper.Map<TransactionEntity>(transactionDTO);//through action
            transaction.Date = DateTime.Now;
            transaction.ProductId = productId;
            transaction.UserId = _userStorage.GetCurrentUserId();
            transaction.User = _userStorage.GetUserById(transaction.UserId.ToString());

            return transaction;
        }

        private async Task ChangeProductQuantityByTransaction(ProductEntity product, TransactionEntity transaction)
        {
            switch (transaction.Operation)
            {
                case TypeOperation.Recepits:
                    product.Quantity += transaction.Quantity;
                    break;

                case TypeOperation.Expense:
                    product.Quantity -= transaction.Quantity;
                    break;
            }

            await _productStorage.Update(product);
        }
    }
}
