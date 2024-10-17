using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Store.Repository.Specification.Product;
using Store.Service.Services.Product;
using Store.Service.Services.Product.Dto;
using Store.Web.Helper;

namespace Store.Web.Controllers
{
  
    public class ProductsController : BaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<BrandTypesDetailsDto>>> GetAllBrandsAsync()
            => Ok(await _productService.GetAllBrandsAsync());

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<BrandTypesDetailsDto>>> GetAllTypesAsync()
            => Ok(await _productService.GetAllTypesAsync());

        [HttpGet]
        [Cache(10)]
        public async Task<ActionResult<IReadOnlyList<ProductDetailsDto>>> GetAllProductsAsync([FromQuery]ProductSpecification input)
            => Ok(await _productService.GetAllProductAsync(input));

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductDetailsDto>>> GetProductByIdAsync(int? id)
            => Ok(await _productService.GetProductById(id));

    }
}
