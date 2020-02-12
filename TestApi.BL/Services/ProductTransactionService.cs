using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
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



        public ProductTransactionService(IProductStorage productStorage,
                                         IProductTransactionStorage productTransactionStorage,
                                         IMapper mapper,
                                         IUserStorage userStorage)
        {
            _productStorage = productStorage;
            _productTransactionStorage = productTransactionStorage;
            _mapper = mapper;
            _userStorage = userStorage;
        }

        public IEnumerable<TransactionOutcomeDTO> GetTransactions(int productId)
        {
            return _mapper.Map<IEnumerable<TransactionOutcomeDTO>>(_productTransactionStorage.GetTransactions(productId));
        }


        public async Task<IEnumerable<TransactionOutcomeDTO>> CreateTransactionAsync(int productId, TransactionDTO transactionDTO)
        {
            var product = await _productStorage.GetByIdAsync(productId);


            Check(product.Quantity, transactionDTO);

            var transaction = _mapper.Map<TransactionEntity>(transactionDTO);
            transaction.Date = DateTime.Now;
            transaction.ProductId = productId;
            transaction.UserId = _userStorage.GetCurrentUserId();
            transaction.User = _userStorage.GetUserById(transaction.UserId.ToString());

            await _productTransactionStorage.CreateTransactionAsync(transaction);

            switch (transactionDTO.Operation)
            {
                case TypeOperation.Recepits:
                    product.Quantity += transaction.Quantity;
                    break;

                case TypeOperation.Expense:
                    product.Quantity -= transaction.Quantity;
                    break;
            }

            await _productStorage.UpdateAsync(product);
            return GetTransactions(product.Id);

        }

        private void Check(int productQuantity, TransactionDTO transactionDTO)
        {
            if (productQuantity < transactionDTO.Quantity && transactionDTO.Operation == TypeOperation.Expense)
            {
                throw new AppValidationException($"Transaction quantity ({transactionDTO.Quantity}) can't be more product quantity ({productQuantity}) in case expense!");
            }
        }
    }
}
