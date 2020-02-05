using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApi.DataLayer;
using TestApi.Entities;

namespace TestApi.Storages
{
    public class ProductStorage : IProductStorage
    {
        private readonly AppDbContext _dbContext;

        public ProductStorage(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<ProductEntity> GetAll() => _dbContext.ProductSet.ToList();

        public ProductEntity GetById(int productId) => _dbContext.ProductSet.SingleOrDefault(x => x.Id == productId);

        public ProductEntity Add(ProductEntity product)
        {
            _dbContext.ProductSet.Add(product);
            _dbContext.SaveChanges();
            return GetById(product.Id);
        }

        public void Delete(int productId)
        {

            var productToDelete = GetById(productId);//замапить dto to entity внутри, будет ли виден айди?


            if (productToDelete != null)
            {
                _dbContext.ProductSet.Remove(productToDelete);
                _dbContext.SaveChanges();
            }

        }

        public ProductEntity Update(ProductEntity newProduct)
        {

            _dbContext.ProductSet.Update(newProduct);
            _dbContext.SaveChanges();
            return GetById(newProduct.Id);

        }
    }
}
