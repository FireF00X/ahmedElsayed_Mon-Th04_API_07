using Microsoft.Extensions.Logging;
using Store.Data.Contexts;
using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.Repository
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (context.BrandTypes != null && !context.BrandTypes.Any() /*any items there*/)
                {
                    // presist data to database

                    var brandsData = File.ReadAllText("../Store.Repository/SeedData/brands.json");
                    // we want to convert from string to somthing that database can understand
                    // so we have two processes [Serilization - De Serilization]
                    // Serilization => convert from Object -> string
                    // De Serilization => convert from string -> Object

                    var brands = JsonSerializer.Deserialize<List<BrandType>>(brandsData);
                    // name of prop have to be thw same

                    if (brands is not null)
                    {
                        await context.BrandTypes.AddRangeAsync(brands);
                    }

                }

                if (context.ProductType != null && !context.ProductType.Any() /*any items there*/)
                {
                    var typesData = File.ReadAllText("../Store.Repository/SeedData/types.json");

                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    if (types is not null)
                    {
                        await context.ProductType.AddRangeAsync(types);
                    }
                }

                if (context.Products != null && !context.Products.Any() /*any items there*/)
                {
                    var productsData = File.ReadAllText("../Store.Repository/SeedData/products.json");

                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    if (products is not null)
                    {
                        await context.Products.AddRangeAsync(products);
                    }
                }

                if (context.DeliveryMethods != null && !context.DeliveryMethods.Any() /*any items there*/)
                {
                    var deliveryMethodsData = File.ReadAllText("../Store.Repository/SeedData/products.json");

                    var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodsData);

                    if (deliveryMethods is not null)
                    {
                        await context.DeliveryMethods.AddRangeAsync(deliveryMethods);
                    }
                }

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                {
                    var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                    logger.LogError(ex.Message);
                }

            }
        }
    }
}
