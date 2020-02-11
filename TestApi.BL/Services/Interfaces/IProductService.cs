using System.Collections.Generic;
using System.Threading.Tasks;
using TestApi.BL.DTOs;

namespace TestApi.BL.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductOutcomeDTO> AddProd(ProductDTO newProd);
        Task DeleteProd(int productId);
        Task<IEnumerable<ProductOutcomeDTO>> GetAllProds();
        Task<ProductOutcomeDTO> GetProdById(int productId);
        Task<ProductOutcomeDTO> UpdateProd(int productId, ProductDTO productDTO);
    }
}
