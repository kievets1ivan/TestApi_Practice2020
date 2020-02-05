using System.Collections.Generic;
using TestApi.DTOs;

namespace TestApi.Services
{
    public interface IProductService
    {
        ProductDTO AddProd(ProductDTO newProd);
        void DeleteProd(int productId);
        IEnumerable<ProductDTO> GetAllProds();
        ProductDTO GetProdById(int productId);
        ProductDTO UpdateProd(int productId, ProductDTO productDTO);
    }
}