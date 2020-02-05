using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestApi.DataLayer;
using TestApi.Entities;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly AppDbContext _dbContext;




        public ProductController(AppDbContext dbContext)
        {
            _dbContext = dbContext;

        }


        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_dbContext.ProductSet);
        }

        [HttpGet("{productId}")]
        public IActionResult Get([FromRoute] int productId)
        {

            var product = _dbContext.ProductSet.SingleOrDefault(x => x.Id == productId);

            if (product == null)
                NotFound();

            return Ok(product);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProductEntity newProductDTO)
        {
            //маппинг с модели входа(фронт)

            var newProduct = new ProductEntity
            {

                Name = newProductDTO.Name,
                Price = newProductDTO.Price
            };


            _dbContext.ProductSet.Add(newProduct);
            _dbContext.SaveChangesAsync();


            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + "api /[controller]/" + newProduct.Id.ToString();

            //маппинг на выходящую модель

            return Created(locationUri, newProductDTO);
            //return Ok();

            //_productService.GetProdById(newProduct.Id);
            //return instance
        }

        [HttpPut("{productId}")]
        public IActionResult Update([FromRoute] int productId, [FromBody] ProductEntity productDTO)
        {
            var newProduct = _dbContext.ProductSet.SingleOrDefault(x => x.Id == productId);

            if (newProduct != null)
            {
                newProduct.Name = productDTO.Name;
                newProduct.Price = productDTO.Price;



                _dbContext.ProductSet.Update(newProduct);
                _dbContext.SaveChangesAsync();

                for(var i = 0; i < 10000; i++)
                {
                    Console.WriteLine("111");
                }


                var productUpdated = _dbContext.ProductSet.SingleOrDefault(x => x.Id == newProduct.Id);

                return Ok(productUpdated);
            }

            return NotFound();

        }

        [HttpDelete("{productId}")]
        public IActionResult DeleteById(int productId)
        {

            var productToDelete = _dbContext.ProductSet.SingleOrDefault(x => x.Id == productId);

            if (productToDelete != null)
            {
                _dbContext.ProductSet.Remove(productToDelete);
                _dbContext.SaveChangesAsync();

                return NoContent();

            }

            return NotFound();
        }
    }
}