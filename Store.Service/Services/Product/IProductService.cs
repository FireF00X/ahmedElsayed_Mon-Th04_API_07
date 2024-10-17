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
    public interface IProductService
    {
        Task<ProductDetailsDto> GetProductById(int? productId);
        Task<PaginatedResultDto<ProductDetailsDto>> GetAllProductAsync(ProductSpecification specs);
        Task<IReadOnlyList<BrandTypesDetailsDto>> GetAllBrandsAsync();
        Task<IReadOnlyList<BrandTypesDetailsDto>> GetAllTypesAsync();

    }
}
