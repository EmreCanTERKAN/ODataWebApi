using Bogus;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using ODataWebApi.Context;
using ODataWebApi.Controllers;
using ODataWebApi.Models;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    string connectionString = "Server=DESKTOP-GRDREHV\\SQLEXPRESS; Database=ODataWebApiDb; TrustServerCertificate=true; Trusted_Connection=true;";
    options.UseSqlServer(connectionString);
});

builder.Services.AddControllers().AddOData(opt =>
{
    opt.EnableQueryFeatures()
       .AddRouteComponents("odata", MyTestController.GetEdmModel());
});

builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi();

app.MapScalarApiReference();


app.MapGet("seed-data/categories", async (AppDbContext dbContext) =>
{
    Faker fakers = new();

    var categoryNames = fakers.Commerce.Categories(100);

    List<Category> categories = categoryNames.Select(s => new Category
    {
        Name = s,
    }).ToList();

    dbContext.AddRange(categories);
    await dbContext.SaveChangesAsync();
    return Results.NoContent();
}).Produces(204).WithTags("SeedCategories");

app.MapGet("seed-data/products", async (AppDbContext dbContext) =>
{
    var categories = dbContext.Categories.ToList();

    List<Product> products = new();
    for (int i = 0; i < 10000; i++)
    {
        Faker faker = new();
        Product product = new()
        {
            CategoryId = categories[new Random().Next(categories.Count)].Id,
            Name = faker.Commerce.ProductName(),
            Price = Convert.ToDecimal(faker.Commerce.Price(100, 1000000, 2))
        };

        products.Add(product);
    }

    dbContext.AddRange(products);

    await dbContext.SaveChangesAsync();

    return Results.NoContent();
}).Produces(204).WithTags("SeedProducts");


app.MapControllers();

app.Run();
