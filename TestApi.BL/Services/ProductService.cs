using AutoMapper;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestApi.BL.DTOs;
using TestApi.BL.Exceptions;
using TestApi.BL.Services.Interfaces;
using TestApi.DAL.Entities;
using TestApi.DAL.Enums;
using TestApi.DAL.Storages.Interfaces;
using System;

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

        public async Task<IEnumerable<ProductOutcomeDTO>> GetAll() => _mapper.Map<IEnumerable<ProductOutcomeDTO>>(await _productStorage.GetAll());

        public async Task<ProductOutcomeDTO> Get(int productId) => _mapper.Map<ProductOutcomeDTO>(await _productStorage.GetById(productId));

        public async Task<ProductOutcomeDTO> Add(ProductDTO newProduct) 
        {
            if(!await _productStorage.IsValidName(_mapper.Map<ProductEntity>(newProduct)))
            {
                throw new AppValidationException($"Product {newProduct.Name} is already exist!");
            }

            return _mapper.Map<ProductOutcomeDTO>(await _productStorage.Add(_mapper.Map<ProductEntity>(newProduct)));
        }

        public async Task Delete(int productId) => await _productStorage.Delete(productId);

        public async Task<ProductOutcomeDTO> Update(int productId, ProductDTO productDTO)
        {
            var updatedProduct = _mapper.Map<ProductEntity>(productDTO);
            updatedProduct.Id = productId;
            updatedProduct.Quantity = _productStorage.GetQuantityById(updatedProduct.Id);

            if (!await _productStorage.IsValidName(updatedProduct))
            {
                throw new AppValidationException($"Product {updatedProduct.Name} is already exist!");
            }

            return _mapper.Map<ProductOutcomeDTO>(await _productStorage.Update(updatedProduct));
        }

        public SearchResponseDTO GetLazy(SearchRequestDTO request)
        {
            var response = new SearchResponseDTO
            {
                TotalCount = _productStorage.Count()
            };

            switch (request.SortBy)
            {
                case SortByColumn.NameASC:
                case SortByColumn.NameDESC:
                    response.Products = SortedProducts(request, x => x.Name);
                    break;

                case SortByColumn.PriceASC:
                case SortByColumn.PriceDESC:
                    response.Products = SortedProducts(request, x => x.Price);
                    break;

                case SortByColumn.QuantityASC:
                case SortByColumn.QuantityDESC:
                    response.Products = SortedProducts(request, x => x.Quantity);
                    break;
            }

            return response;
        }

        private IEnumerable<ProductOutcomeDTO> SortedProducts(SearchRequestDTO request, Func<ProductEntity, object> sort)
        {
            return _mapper.Map<IEnumerable<ProductOutcomeDTO>>(
                                        _productStorage.Sort(request.SortBy, sort)
                                                       .Skip(request.Page * request.PerPage)
                                                       .Take(request.PerPage));
        }
    }
}
