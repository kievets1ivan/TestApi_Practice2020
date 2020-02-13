using System.Collections.Generic;
using System.Threading.Tasks;
using TestApi.BL.DTOs;
using TestApi.DAL.Entities;

namespace TestApi.BL.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductOutcomeDTO> Add(ProductDTO newProd);
        Task Delete(int productId);
        Task<IEnumerable<ProductOutcomeDTO>> GetAll();
        Task<ProductOutcomeDTO> Get(int productId);
        Task<ProductOutcomeDTO> Update(int productId, ProductDTO productDTO);
        SearchResponse GetLazy(SearchRequest request);
    }
}
