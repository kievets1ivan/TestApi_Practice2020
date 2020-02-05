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

        public IEnumerable<ProductDTO> GetAllProds()
        {

            return _mapper.Map<ProductDTO[]>(_productStorage.GetAll());//замапить entity to dto

        }

        public ProductDTO GetProdById(int productId)
        {
                
            return _mapper.Map<ProductDTO>(_productStorage.GetById(productId));

        }

        public ProductDTO AddProd(ProductDTO newProd)
        {


            return _mapper.Map<ProductDTO>(_productStorage.Add(_mapper.Map<ProductEntity>(newProd)));


            //замапить entity to dto снаружи
            //замапить dto to entity внутри
        }

        public void DeleteProd(int productId)
        {

            _productStorage.Delete(productId);

        }

        public ProductDTO UpdateProd(int productId, ProductDTO productDTO)
        {
            var newProduct = _mapper.Map<ProductEntity>(productDTO);
            newProduct.Id = productId;


            return _mapper.Map<ProductDTO>(_productStorage.Update(newProduct));

        }
    }
}
