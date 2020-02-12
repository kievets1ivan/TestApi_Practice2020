﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestApi.BL.DTOs;
using TestApi.BL.Services;
using TestApi.BL.Services.Interfaces;

namespace TestApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTransactionController : Controller
    {

        private readonly IProductTransactionService _productTransactionService;

        public ProductTransactionController(IProductTransactionService productTransactionService)
        {
            _productTransactionService = productTransactionService;
        }

        // GET: api/ProductTransaction/5
        [HttpGet("{productId}")]
        public IActionResult Get([FromRoute]int productId)
        {
            return Ok(_productTransactionService.GetTransactions(productId));

        }

        // POST: api/ProductTransaction
        [HttpPost("{productId}")]
        public IActionResult Create([FromRoute] int productId, [FromBody] TransactionDTO transactionDTO)
        {
            var transactions = _productTransactionService.CreateTransactionAsync(productId, transactionDTO);

            if (transactions.Result == null)
                return BadRequest(new ValidationResult($"There is no enough quantity of product for your transation"));

            return Ok(transactions);

        }
    }
}
