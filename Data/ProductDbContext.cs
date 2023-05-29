using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SeedingDataAPIByJson.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

//Adding   Microsoft.EntityFrameworkCore from Nuget to can inherit class from  DbContext
//Adding   Microsoft.EntityFrameworkCore.SqlServer  to can use UseSqlServer method


namespace SeedingDataAPIByJson.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext()
        {

        }

        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
