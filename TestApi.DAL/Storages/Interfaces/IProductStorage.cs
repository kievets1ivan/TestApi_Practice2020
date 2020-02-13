using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestApi.DAL.Entities;

namespace TestApi.DAL.Storages.Interfaces
{
    public interface IProductStorage
    {
        Task<ProductEntity> Add(ProductEntity product);
        Task Delete(int productId);
        Task<IEnumerable<ProductEntity>> GetAll();
        Task<ProductEntity> GetById(int productId);
        Task<ProductEntity> Update(ProductEntity newProduct);
        Task<ProductEntity> GetByName(string productName);
        int GetQuantityById(int productId);
        Task<bool> IsValidName(ProductEntity product);
        SearchResponse GetLazy(SearchRequest request);
    }
}
