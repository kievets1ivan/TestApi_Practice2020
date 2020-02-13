using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestApi.DAL.Entities;
using TestApi.DAL.Enums;
using TestApi.DAL.Storages.Interfaces;
using System.Linq;
using System;

namespace TestApi.DAL.Storages
{
    public class ProductStorage : IProductStorage
    {
        private readonly AppDbContext _dbContext;

        public ProductStorage(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ProductEntity>> GetAll() => await _dbContext.ProductSet.ToListAsync();

        public async Task<ProductEntity> GetById(int productId) => await _dbContext.ProductSet.SingleOrDefaultAsync(x => x.Id == productId);

        public async Task<ProductEntity> Add(ProductEntity product)
        {
            await _dbContext.ProductSet.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return await GetById(product.Id);
        }

        public async Task Delete(int productId)
        {
            var productToDelete = await GetById(productId);

            if (productToDelete != null)
            {
                _dbContext.ProductSet.Remove(productToDelete);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<ProductEntity> Update(ProductEntity newProduct)
        {
            _dbContext.ProductSet.Update(newProduct);
            await _dbContext.SaveChangesAsync();
            return await GetById(newProduct.Id);
        }

        public SearchResponse GetLazy(SearchRequest request)
        {
            var response = new SearchResponse();

            response.TotalCount = _dbContext.ProductSet.Count();

            switch (request.SortBy)
            {
                case SortByColumn.NameASC:
                case SortByColumn.NameDESC:
                    response.Products = SearchByRequest(_dbContext.ProductSet.ToList(), request, x => x.Name);                         
                    break;

                case SortByColumn.PriceASC:
                case SortByColumn.PriceDESC:
                    response.Products = SearchByRequest(_dbContext.ProductSet.ToList(), request, x => x.Price);
                    break;

                case SortByColumn.QuantityASC:
                case SortByColumn.QuantityDESC:
                    response.Products = SearchByRequest(_dbContext.ProductSet.ToList(), request, x => x.Quantity);
                    break;
            }

            return response;
        }

        private IEnumerable<ProductEntity> SearchByRequest(IEnumerable<ProductEntity> products,
                                                           SearchRequest request,
                                                           Func<ProductEntity, object> sort)
        {

            if ((int)request.SortBy % 2 == 0)
            {
                return products.OrderBy(sort)
                              .Skip(request.Page * request.PerPage)
                              .Take(request.PerPage);
            }

            return products.OrderByDescending(sort)
                              .Skip(request.Page * request.PerPage)
                              .Take(request.PerPage);
        }



        public async Task<ProductEntity> GetByName(string productName) => await _dbContext.ProductSet.SingleOrDefaultAsync(x => x.Name == productName);

        public int GetQuantityById(int productId) => _dbContext.ProductSet.AsNoTracking().SingleOrDefault(x => x.Id == productId).Quantity;

        public async Task<bool> IsValidName(ProductEntity product)
        {
            if(product.Id == 0)
            {
                if (await GetByName(product.Name) != null)
                {
                    return false;
                }
                return true;
            }

            if (await _dbContext.ProductSet.FirstOrDefaultAsync(x => x.Name == product.Name && x.Id != product.Id) != null)
            {
                return false;
            }
            return true;
        }
    }
}
