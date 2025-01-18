using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using ODataWebApi.Context;
using ODataWebApi.Dtos;
using ODataWebApi.Models;

namespace ODataWebApi.Controllers
{
    [Route("odata")]
    [ApiController]
  
    public class MyTestController(AppDbContext dbContext) : ODataController
    {

        public static IEdmModel GetEdmModel()
        {
            // ODataModelBuilder örneği oluşturuluyor.
            ODataConventionModelBuilder builder = new ();

            //Odata verilerini başlangıç olarak büyük harf vermektedir. O yüzden biz json formata dönüştürmekte sıkıntı yaratacağı için baş harflerini küçük yazmamız gerekmektedir. 
            builder.EnableLowerCamelCase();

            // "categories" adında bir EntitySet tanımlanıyor.
            // Bu EntitySet, "Category" tipindeki varlıkları içerir.
            builder.EntitySet<Category>("categories");
            builder.EntitySet<Product>("products");
            builder.EntitySet<ProductDto>("products-dto");
            builder.EntitySet<UserDto>("users");


            // Oluşturulan model, IEdmModel olarak döndürülüyor.
            return builder.GetEdmModel();
        }
        #region Categories
        [HttpGet("categories")]
        [EnableQuery]
        public IQueryable<Category> Categories()
        {
            var categories = dbContext.Categories.AsQueryable();
            return categories;
        }
        #endregion

        #region Products
        [HttpGet("products")]
        [EnableQuery]
        public IQueryable<Product> Products()
        {
            var products = dbContext.Products.AsQueryable();
            return products;
        }
        #endregion

        [HttpGet("products-dto")]
        [EnableQuery]
        public IQueryable<ProductDto> ProductDto()
        {
            var products = dbContext.Products.Select(s => new ProductDto
            {          
                  Name = s.Name,
                  Price = s.Price,
                  Id = s.Id,
                  CategoryName = s.Category != null ? s.Category.Name : ""
            }).AsQueryable();
            return products;
        }

        #region Users
        [HttpGet("users")]
        [EnableQuery]
        public IActionResult User()
        {
            var users = dbContext.Users
                .Select(s => new UserDto
                {
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    FullName = s.FullName,
                    Adress = s.Adress,
                    Id = s.Id,
                    UserType = s.UserType,
                    UserTypeName = s.UserType.Name,
                    UserTypeValue = s.UserType.Value
                })
                .AsQueryable();
            return Ok(users);
        }
        #endregion

    }
}
