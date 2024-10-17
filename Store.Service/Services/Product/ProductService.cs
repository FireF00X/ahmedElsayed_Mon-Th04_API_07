using AutoMapper;
using Store.Data.Entities;
using Store.Repository.Interfaces;
using Store.Repository.Specification.Product;
using Store.Service.Helper;
using Store.Service.Services.Product.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<BrandTypesDetailsDto>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.Repository<BrandType, int>().GetAllAsync();

            var mappedBrands = _mapper.Map<IReadOnlyList<BrandTypesDetailsDto>>(brands);

            return mappedBrands;
        }

        public async Task<PaginatedResultDto<ProductDetailsDto>> GetAllProductAsync(ProductSpecification input)
        {
            var specs = new ProducrWithSpecifications(input);

            var products =  await _unitOfWork.Repository<Store.Data.Entities.Product,int>().GetAllAsyncWithSpecification(specs);
           
            var countSpecs =  new ProductWithCountSpecification(input);

            var count = await _unitOfWork.Repository<Store.Data.Entities.Product, int>().GetCountSpecificationAsync(countSpecs);

            var mappedProducts = _mapper.Map<IReadOnlyList<ProductDetailsDto>>(products);



            return new PaginatedResultDto<ProductDetailsDto>(input.PageSize, input.PageIndex , count, mappedProducts);
        }

        public async Task<IReadOnlyList<BrandTypesDetailsDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.Repository<ProductType, int>().GetAllAsync();

            var mappedTypes = _mapper.Map<IReadOnlyList<BrandTypesDetailsDto>>(types);

            return mappedTypes;
        }

        public async Task<ProductDetailsDto> GetProductById(int? productId)
        {
            if(productId is null)
            {
                throw new Exception("Id Is Null");
            }

            var specs = new ProducrWithSpecifications(productId);

            var product = await _unitOfWork.Repository<Store.Data.Entities.Product, int>().GetWithSpecificationByIdAsync(specs);

            if (product is null)
            {
                throw new Exception("Product Is Null");
            }

            var mappedProduct = _mapper.Map<ProductDetailsDto>(product);

            return mappedProduct;
        }
    }
}
