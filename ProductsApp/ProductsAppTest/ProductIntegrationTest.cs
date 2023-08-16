using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using ProductsApp.Data;
using ProductsApp.Models;
using ProductsApp.Services;

namespace ProductsAppTest
{
    public class ProductIntegrationTest
    {
        private  DbMainContext? dbMainContext;
   

        public ProductIntegrationTest()
        {
            SetupEFDBConnection();
        }

        private void SetupEFDBConnection()
        {
          var serviceProvider = new ServiceCollection()
         .AddEntityFrameworkSqlServer()
         .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<DbMainContext>();

            builder.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database=Demo;Trusted_Connection=True;MultipleActiveResultSets=true")
                    .UseInternalServiceProvider(serviceProvider);

            dbMainContext = new DbMainContext(builder.Options);
            dbMainContext.Database.Migrate();
        }

        //private void SetupDapperConnection()
        //{
        //    var mockConfSection = new Mock<IConfigurationSection>();
        //    mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "DefaultConnection")]).Returns("Server=(localdb)\\mssqllocaldb;Database=Demo;Trusted_Connection=True;MultipleActiveResultSets=true");

        //    var mockConfiguration = new Mock<IConfiguration>();
        //    mockConfiguration.Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings"))).Returns(mockConfSection.Object);

        //    _configuration = mockConfiguration.Object;
        //}


        [Fact]
        public void InsertBulkProducs()
        {
            //Arrange
            IProductService productService = new ProductService(dbMainContext);

            //Act
            var result = productService.InsertBulkProducts(GetProducts());

            //Assert
            Assert.True(result.Result > 0);

        }



        private List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            for (int i = 0; i <= 10; i++)
            {
                products.Add(new Product { 
                    ProductId = i,
                    ProductName = $"Product Name_{i}",
                    ProductDescription = $"Product Description_{i}",
                    Price = 10,
                    Stock = 5
                });
            }

            return products;
        }
    }
}
