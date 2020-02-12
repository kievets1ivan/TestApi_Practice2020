using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestApi.BL.DTOs;
using TestApi.BL.Exceptions;
using TestApi.BL.Services.Interfaces;
using TestApi.DAL.Entities;
using TestApi.DAL.Enums;
using TestApi.DAL.Storages;
using TestApi.DAL.Storages.Interfaces;

namespace TestApi.BL.Services
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

        public async Task<IEnumerable<ProductOutcomeDTO>> GetAll() => _mapper.Map<IEnumerable<ProductOutcomeDTO>>(await _productStorage.GetAllAsync());

        public async Task<ProductOutcomeDTO> Get(int productId) => _mapper.Map<ProductOutcomeDTO>(await _productStorage.GetByIdAsync(productId));

        public async Task<ProductOutcomeDTO> Add(ProductDTO newProd) {

            if(await _productStorage.GetByNameAsync(newProd.Name) != null)
            {
                throw new AppValidationException($"Product {newProd.Name} is already exist!");
            }

            return _mapper.Map<ProductOutcomeDTO>(await _productStorage.AddAsync(_mapper.Map<ProductEntity>(newProd)));
        }


        public async Task Delete(int productId) => await _productStorage.DeleteAsync(productId);

        public async Task<ProductOutcomeDTO> UpdateProd(int productId, ProductDTO productDTO)
        {

            var newProduct = _mapper.Map<ProductEntity>(productDTO);

            newProduct.Id = productId;
            newProduct.Quantity = _productStorage.GetQuantityById(newProduct.Id);

            return _mapper.Map<ProductOutcomeDTO>(await _productStorage.UpdateAsync(newProduct));
        }
    }
}
