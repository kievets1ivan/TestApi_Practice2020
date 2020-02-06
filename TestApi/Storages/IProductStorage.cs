using System.Collections.Generic;
using System.Threading.Tasks;
using TestApi.Entities;

namespace TestApi.Storages
{
    public interface IProductStorage
    {
        Task<ProductEntity> AddAsync(ProductEntity product);
        Task<bool> DeleteAsync(int productId);
        Task<IEnumerable<ProductEntity>> GetAllAsync();
        Task<ProductEntity> GetByIdAsync(int productId);
        Task<ProductEntity> UpdateAsync(ProductEntity newProduct);
    }
}