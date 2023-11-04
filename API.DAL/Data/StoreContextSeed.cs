using API.BLL.Entities;
using API.BLL.Entities.Order;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
//using Microsoft.Extensions.Logging;

namespace API.DAL.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context,ILoggerFactory loggerFactory)
        {
            try
            {
                if (context.ProductBrands != null && !context.ProductBrands.Any())
                {
                    var brandData = File.ReadAllText("../API.DAL/Data/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
                    for (int i = 0; i < brands?.Count; i++)
                    {
                        context.ProductBrands.Add(brands[i]);
                    }
                    await context.SaveChangesAsync();
                }

                if (context.ProductTypes != null && !context.ProductTypes.Any())
                {
                    var typesData = File.ReadAllText("../API.DAL/Data/SeedData/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                    for (int i = 0; i < types?.Count; i++)
                    {
                        context.ProductTypes.Add(types[i]);
                    }
                    await context.SaveChangesAsync();
                }

                if (context.Products != null && !context.Products.Any())
                {
                    var productsData = File.ReadAllText("../API.DAL/Data/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    for (int i = 0; i < products?.Count; i++)
                    {
                        context.Products.Add(products[i]);
                    }
                    await context.SaveChangesAsync();
                }

                if (context.DeliveryMethods != null && !context.DeliveryMethods.Any())
                {
                    var deliverymethods = File.ReadAllText("../API.DAL/Data/SeedData/delivery.json");
                    var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliverymethods);
                    for (int i = 0; i < methods?.Count; i++)
                    {
                        context.DeliveryMethods.Add(methods[i]);
                    }
                    await context.SaveChangesAsync();
                }
            }
            catch(Exception )
            {
                //var logger = LoggerFactory.CreateLogger<StoreContextSeed>();
                //logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
