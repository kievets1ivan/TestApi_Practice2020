using System.Collections.Generic;
using TestApi.Entities;

namespace TestApi.Storages
{
    public interface IProductStorage
    {
        ProductEntity Add(ProductEntity product);
        void Delete(int productId);
        IEnumerable<ProductEntity> GetAll();
        ProductEntity GetById(int productId);
        ProductEntity Update(ProductEntity newProduct);
    }
}