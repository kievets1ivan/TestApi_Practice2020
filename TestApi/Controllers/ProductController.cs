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
        public IActionResult GetAll()
        {
            return Ok(_productService.GetAllProds());
        }

        [HttpGet("{productId}")]
        public IActionResult Get([FromRoute] int productId)
        {

            var product = _productService.GetProdById(productId);

            if (product == null)
                NotFound();

            return Ok(product);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProductDTO newProductDTO)
        {


            var productAdded = _productService.AddProd(newProductDTO);

            return Ok(productAdded);

        }

        [HttpPut("{productId}")]
        public IActionResult Update([FromRoute] int productId, [FromBody] ProductDTO productDTO)
        {

            var product = _productService.UpdateProd(productId, productDTO);

            if (product == null)
                NotFound();

            return Ok(product);



        }

        [HttpDelete("{productId}")]
        public IActionResult DeleteById(int productId)
        {

            _productService.DeleteProd(productId);
            return Ok();

        }
    }
}