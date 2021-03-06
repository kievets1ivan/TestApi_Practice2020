﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using TestApi.BL.DTOs;
using TestApi.BL.Services.Interfaces;
using TestApi.BL.Exceptions;
using System;

namespace TestApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _productService.GetAll());

        [HttpGet("{productId}")]
        public async Task<IActionResult> Get([FromRoute] int productId) => Ok(await _productService.Get(productId));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductDTO newProductDTO)
        {
            try
            {
                return Ok(await _productService.Add(newProductDTO));
            }
            catch (AppValidationException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> Update([FromRoute] int productId, [FromBody] ProductDTO productDTO)
        {
            try
            {
                return Ok(await _productService.Update(productId, productDTO));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete([FromRoute] int productId)
        {
            await _productService.Delete(productId);
            return NoContent();
        }

        [HttpPost("lazy")]
        public IActionResult GetLazy([FromBody] SearchRequestDTO request)
        {
            return Ok(_productService.GetLazy(request));
        }
    }
}