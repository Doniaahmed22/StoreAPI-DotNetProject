using Microsoft.Extensions.Logging;
using Store.Data.Context;
using Store.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.Reposatry
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreDbContext context,ILoggerFactory loggerFactory )
        {
            try
            {
                if (context.ProductBrands != null && !(context.ProductBrands.Any()))
                {
                    var brandsData = File.ReadAllText("../Store.Reposatry/SeedData/brands.json");
                    var brands= JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                    if (brands is not null)
                    {
                        foreach(var brand in brands)
                        {
                            await context.ProductBrands.AddAsync(brand);
                        }
                        
                    }

                }
                //---------------
                if (context.ProductTypes != null && !(context.ProductTypes.Any()))
                {
                    var TypessData = File.ReadAllText("../Store.Reposatry/SeedData/types.json");
                    var Types = JsonSerializer.Deserialize<List<ProductType>>(TypessData);
                    if (Types is not null)
                    {
                        foreach (var type in Types)
                        {
                            await context.ProductTypes.AddAsync(type);
                        }
                     
                    }

                }
                //------------------
                if (context.Products != null && !(context.Products.Any()))
                {
                    var ProductsData = File.ReadAllText("../Store.Reposatry/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                    if (products is not null)
                    {
                        foreach (var product in products)
                        {
                            await context.Products.AddAsync(product);
                        }
                      
                    }

                }
                //--------------
                if (context.DeleviryMethods != null && !(context.DeleviryMethods.Any()))
                {
                    var deleviryMethodsData = File.ReadAllText("../Store.Reposatry/SeedData/delivery.json");
                    var deleviryMethods = JsonSerializer.Deserialize<List<DeleviryMethod>>(deleviryMethodsData);
                    if (deleviryMethods is not null)
                    {
                        foreach (var deleviryMethod in deleviryMethods)
                        {
                            await context.DeleviryMethods.AddAsync(deleviryMethod);
                        }

                    }

                }
                await context.SaveChangesAsync();
            }
            catch(Exception ex)
            {

               var logger=loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            
            }
            

        }
    }
}
