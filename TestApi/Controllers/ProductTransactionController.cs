﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestApi.BL.DTOs;
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
        public async Task<IActionResult> Create([FromRoute] int productId, [FromBody] TransactionDTO transactionDTO)
        {
            try
            {   
                return Ok(await _productTransactionService.CreateTransaction(productId, transactionDTO));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }   
        }
    }
}
