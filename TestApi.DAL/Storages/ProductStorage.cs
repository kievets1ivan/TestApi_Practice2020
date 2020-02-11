using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestApi.DAL.Entities;
using TestApi.DAL.Storages.Interfaces;

namespace TestApi.DAL.Storages
{
    public class ProductStorage : IProductStorage
    {
        private readonly AppDbContext _dbContext;

        public ProductStorage(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ProductEntity>> GetAllAsync() => await _dbContext.ProductSet.ToListAsync();

        public async Task<ProductEntity> GetByIdAsync(int productId) => await _dbContext.ProductSet.SingleOrDefaultAsync(x => x.Id == productId);

        public async Task<ProductEntity> AddAsync(ProductEntity product)
        {
            await _dbContext.ProductSet.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return await GetByIdAsync(product.Id);
        }

        public async Task DeleteAsync(int productId)
        {
            var productToDelete = await GetByIdAsync(productId);

            if (productToDelete != null)
            {
                _dbContext.ProductSet.Remove(productToDelete);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<ProductEntity> UpdateAsync(ProductEntity newProduct)
        {
            _dbContext.ProductSet.Update(newProduct);
            await _dbContext.SaveChangesAsync();
            return await GetByIdAsync(newProduct.Id);
        }

        public async Task<ProductEntity> GetByNameAsync(string productName) => await _dbContext.ProductSet.SingleOrDefaultAsync(x => x.Name == productName);
    }
}
