using System.Collections.Generic;
using System.Threading.Tasks;
using TestApi.BL.DTOs;

namespace TestApi.BL.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductOutcomeDTO> Add(ProductDTO newProd);
        Task Delete(int productId);
        Task<IEnumerable<ProductOutcomeDTO>> GetAll();
        Task<ProductOutcomeDTO> Get(int productId);
        Task<ProductOutcomeDTO> UpdateProd(int productId, ProductDTO productDTO);
    }
}
