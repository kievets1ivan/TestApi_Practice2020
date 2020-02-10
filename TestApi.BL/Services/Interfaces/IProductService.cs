using System.Collections.Generic;
using System.Threading.Tasks;
using TestApi.BL.DTOs;

namespace TestApi.BL.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductDTO> AddProd(ProductDTO newProd);
        Task DeleteProd(int productId);
        Task<IEnumerable<ProductDTO>> GetAllProds();
        Task<ProductDTO> GetProdById(int productId);
        Task<ProductDTO> UpdateProd(int productId, ProductDTO productDTO);
    }
}
