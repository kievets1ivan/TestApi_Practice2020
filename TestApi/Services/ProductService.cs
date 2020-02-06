using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApi.DTOs;
using TestApi.Entities;
using TestApi.Storages;

namespace TestApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductStorage _productStorage;
        private readonly IMapper _mapper;

        public ProductService(IProductStorage productStorage, IMapper mapper)
        {
            _productStorage = productStorage;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProds() => _mapper.Map<ProductDTO[]>(await _productStorage.GetAllAsync());

        public async Task<ProductDTO> GetProdById(int productId) => _mapper.Map<ProductDTO>(await _productStorage.GetByIdAsync(productId));

        public async Task<ProductDTO> AddProd(ProductDTO newProd) => _mapper.Map<ProductDTO>(await _productStorage.AddAsync(_mapper.Map<ProductEntity>(newProd)));

        public async Task<bool> DeleteProd(int productId) => await _productStorage.DeleteAsync(productId);

        public async Task<ProductDTO> UpdateProd(int productId, ProductDTO productDTO)
        {
            var newProduct = _mapper.Map<ProductEntity>(productDTO);
            newProduct.Id = productId;

            return _mapper.Map<ProductDTO>(await _productStorage.UpdateAsync(newProduct));
        }
    }
}
