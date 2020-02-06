using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestApi.DTOs;
using TestApi.Entities;
using TestApi.Services;

namespace TestApi.Controllers
{
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
        public async Task<IActionResult> GetAll() => Ok(await _productService.GetAllProds());

        [HttpGet("{productId}")]
        public async Task<IActionResult> Get([FromRoute] int productId) => Ok(await _productService.GetProdById(productId));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductDTO newProductDTO) => Ok(await _productService.AddProd(newProductDTO));

        [HttpPut("{productId}")]
        public async Task<IActionResult> Update([FromRoute] int productId, [FromBody] ProductDTO productDTO) => Ok(await _productService.UpdateProd(productId, productDTO));

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteById(int productId) => Ok(await _productService.DeleteProd(productId));
    }
}