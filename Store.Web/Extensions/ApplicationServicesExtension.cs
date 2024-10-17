using Microsoft.AspNetCore.Mvc;
using Store.Repository.Interfaces;
using Store.Repository.Repositories;
using Store.Service.Services.Product.Dto;
using Store.Service.Services.Product;
using Store.Service.HandleResponses;
using Store.Service.Services.CacheService;
using Store.Service.Services.BasketService.Dto;
using Store.Service.Services.BasketService;
using Store.Repository.Basket;
using Store.Service.Services.TokenServices;
using Store.Service.Services.UserService;
using Store.Service.Services.OrderService.Dto;
using Store.Service.Services.OrderService;
using Store.Service.Services.PaymentService;

namespace Store.Web.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPaymentService, PaymentService>();

            services.AddScoped<IBasketRepository, BasketRepository>();

            services.AddAutoMapper(typeof(ProductProfile));
            services.AddAutoMapper(typeof(BasketProfile));
            services.AddAutoMapper(typeof(OrderProfile));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionResult =>
                {
                    var errors = actionResult.ModelState
                                             .Where(model => model.Value?.Errors.Count() > 0)
                                             .SelectMany(model => model.Value?.Errors)
                                             .Select(errors => errors.ErrorMessage)
                                             .ToList();

                    var errorResponse = new ValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

            return services;
        }
    }
}
