using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using TestApi.BL.DTOs;
using TestApi.BL.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

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
        public async Task<IActionResult> GetAll() => Ok(await _productService.GetAllProds());

        [HttpGet("{productId}")]
        public async Task<IActionResult> Get([FromRoute] int productId) => Ok(await _productService.GetProdById(productId));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductDTO newProductDTO)
        {
            var response = await _productService.AddProd(newProductDTO);

            if(response == null)
            {
                return BadRequest(new ValidationResult($"Product {newProductDTO.Name} is already exist."));
            }

            return Ok(response);
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> Update([FromRoute] int productId, [FromBody] ProductDTO productDTO)
        {
            var response = await _productService.UpdateProd(productId, productDTO);

            if (response == null)
            {
                return BadRequest(new ValidationResult($"Product {productDTO.Name} is already exist."));
            }

            return Ok(response);
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteById(int productId)
        {
            await _productService.DeleteProd(productId);
            return NoContent();
        }
    }
}