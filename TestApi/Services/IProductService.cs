using System.Collections.Generic;
using System.Threading.Tasks;
using TestApi.DTOs;

namespace TestApi.Services
{
    public interface IProductService
    {
        Task<ProductDTO> AddProd(ProductDTO newProd);
        Task<bool> DeleteProd(int productId);
        Task<IEnumerable<ProductDTO>> GetAllProds();
        Task<ProductDTO> GetProdById(int productId);
        Task<ProductDTO> UpdateProd(int productId, ProductDTO productDTO);
    }
}