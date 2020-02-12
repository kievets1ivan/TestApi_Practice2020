﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestApi.DAL.Entities;

namespace TestApi.DAL.Storages.Interfaces
{
    public interface IProductStorage
    {
        Task<ProductEntity> AddAsync(ProductEntity product);
        Task DeleteAsync(int productId);
        Task<IEnumerable<ProductEntity>> GetAllAsync();
        Task<ProductEntity> GetByIdAsync(int productId);
        Task<ProductEntity> UpdateAsync(ProductEntity newProduct);
        Task<ProductEntity> GetByNameAsync(string productName);
        int GetQuantityById(int productId);
    }
}
