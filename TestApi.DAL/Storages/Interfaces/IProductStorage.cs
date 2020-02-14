using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApi.DAL.Entities;
using TestApi.DAL.Enums;

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
        IOrderedEnumerable<ProductEntity> Sort(SortByColumn sortBy, Func<ProductEntity, object> sort);
        int Count();
    }
}
