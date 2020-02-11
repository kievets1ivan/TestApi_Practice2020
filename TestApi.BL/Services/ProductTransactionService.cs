﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApi.BL.DTOs;
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
        private readonly UserManager<UserEntity> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;



        public ProductTransactionService(IProductStorage productStorage,
                                         IProductTransactionStorage productTransactionStorage,
                                         IMapper mapper,
                                         UserManager<UserEntity> userManager,
                                         IHttpContextAccessor httpContextAccessor)
        {
            _productStorage = productStorage;
            _productTransactionStorage = productTransactionStorage;
            _mapper = mapper;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public IEnumerable<TransactionOutcomeDTO> GetTransactions(int productId)
        {
            return _mapper.Map<IEnumerable<TransactionOutcomeDTO>>(_productTransactionStorage.GetTransactions(productId));
        }


        public async Task<IEnumerable<TransactionOutcomeDTO>> CreateTransactionAsync(int productId, TransactionDTO transactionDTO)
        {

            var transaction = _mapper.Map<TransactionEntity>(transactionDTO);
            transaction.Date = DateTime.Now;
            transaction.UserId = new Guid(_userManager.Users.SingleOrDefault(x => x.UserName == _userManager.GetUserId(_httpContextAccessor.HttpContext.User)).Id);
            transaction.UserName = _userManager.Users.SingleOrDefault(x => x.Id == transaction.UserId.ToString()).UserName;
            transaction.ProductId = productId;
            transaction.Product = await _productStorage.GetByIdAsync(productId);

            await _productTransactionStorage.CreateTransactionAsync(transaction);

            var product = await _productStorage.GetByIdAsync(productId);

            switch (transactionDTO.Operation)
            {
                case TypeOperation.Recepits:
                    product.Quantity += transactionDTO.Quantity;
                    break;

                case TypeOperation.Expense:
                    product.Quantity -= transactionDTO.Quantity;
                    break;
            }

            await _productStorage.UpdateAsync(product);
            return GetTransactions(product.Id);

        }
    }
}
