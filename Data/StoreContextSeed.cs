using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using SeedingDataAPIByJson.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting.Internal;
using System.Web;
using Azure.Core;
using Microsoft.Extensions.Hosting;

namespace SeedingDataAPIByJson.Data
{
    public class StoreContextSeed
    {
        //private readonly IWebHostEnvironment _env;
        
        public static async Task SeedAsync(ProductDbContext context, ILoggerFactory loggerFactory,IWebHostEnvironment env)
        {
           
            try
            {
                string contentRootPath = env.ContentRootPath;
                string webRootPath = env.WebRootPath;
               
                // var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                

                if (!context.ProductBrands.Any())
                {
                var brandsData =
                    File.ReadAllText(contentRootPath + @"/Data/SeedData/brands.json");
                    //File.ReadAllText("F:/A-SamplePrj/NetCoreAPI-SeedData-Json/SeedingDataAPIByJson/Data/SeedData/brands.json"); 
                  
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                    context.ProductBrands.AddRange(brands);
                    await context.SaveChangesAsync();
                }

                if (!context.ProductTypes.Any())
                {
                    var typesData =
                     File.ReadAllText(contentRootPath + @"/Data/SeedData/types.json");

                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                    context.ProductTypes.AddRange(types);
                    await context.SaveChangesAsync();
                }

                if (!context.Products.Any())
                {
                    var productsData =
                   
                     File.ReadAllText(contentRootPath + @"/Data/SeedData/products.json");

                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    context.Products.AddRange(products);
                    //foreach (var item in products)
                    //{
                    //    context.Products.Add(item);
                    //}

                    await context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}