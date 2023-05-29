using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;

using SeedingDataAPIByJson.Model;
using SeedingDataAPIByJson.Data;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;



var builder = WebApplication.CreateBuilder(args);

//Add connectionString
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); //DefaultConnection is defined in appsettings.json
builder.Services.AddTransient<StoreContextSeed>();
builder.Services.AddDbContext<ProductDbContext>(x => x.UseSqlServer(connectionString));
//Add Swagger Support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwaggerUI();


SeedData(app);

//Seed Data
 static async Task SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
   
    using (var scope = scopedFactory.CreateScope())
    {
        
        var services = scope.ServiceProvider;
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        var WebHostEnvironment  = services.GetRequiredService<IWebHostEnvironment>(); // to Get root path
        try
        {
            var context = services.GetRequiredService<ProductDbContext>();

            await StoreContextSeed.SeedAsync(context, loggerFactory, WebHostEnvironment);
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogError(ex, "An error occured");
        }
    }
       
}
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}


app.UseSwagger(x => x.SerializeAsV2 = true);

app.Run();



