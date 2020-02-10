using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestApi.BL.DTOs;
using TestApi.BL.Services.Interfaces;
using TestApi.DAL.Entities;
using TestApi.DAL.Storages;

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

        public async Task<IEnumerable<ProductDTO>> GetAllProds() => _mapper.Map<IEnumerable<ProductDTO>>(await _productStorage.GetAllAsync());

        public async Task<ProductDTO> GetProdById(int productId) => _mapper.Map<ProductDTO>(await _productStorage.GetByIdAsync(productId));

        public async Task<ProductDTO> AddProd(ProductDTO newProd) {

            if(await _productStorage.GetByNameAsync(newProd.Name) != null)
            {
                return null;
            }

            return _mapper.Map<ProductDTO>(await _productStorage.AddAsync(_mapper.Map<ProductEntity>(newProd)));
        }


        public async Task DeleteProd(int productId) => await _productStorage.DeleteAsync(productId);

        public async Task<ProductDTO> UpdateProd(int productId, ProductDTO productDTO)
        {

            if (await _productStorage.GetByNameAsync(productDTO.Name) != null)
            {
                return null;
            }

            var newProduct = _mapper.Map<ProductEntity>(productDTO);
            newProduct.Id = productId;

            return _mapper.Map<ProductDTO>(await _productStorage.UpdateAsync(newProduct));
        }
    }
}
