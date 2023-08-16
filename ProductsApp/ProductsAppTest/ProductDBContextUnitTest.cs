
using Microsoft.EntityFrameworkCore;
using Moq;
using ProductsApp.Data;
using ProductsApp.Models;
using ProductsApp.Services;

namespace ProductsAppTest
{
    public class ProductDBContextUnitTest
    {
        private  Mock<DbSet<Product>> mockProductDBSet;

        [Fact]
        public void AddProduct()
        {
            //arrange
            var produclist = GetProducts().AsQueryable();

            mockProductDBSet = new Mock<DbSet<Product>>();
            mockProductDBSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(produclist.Provider);
            mockProductDBSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(produclist.ElementType);
            mockProductDBSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(produclist.Expression);
            mockProductDBSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(produclist.GetEnumerator());

            var mockContext = new Mock<DbMainContext>();
            mockContext.Setup(m => m.Products).Returns(mockProductDBSet.Object);
            mockContext.Setup(m => m.SaveChanges()).Returns(1);

            //act
            var productService = new ProductService(mockContext.Object);
            var _product = productService.CreateProduct(produclist.FirstOrDefault());

            //assert
            mockProductDBSet.Verify(m => m.Add(It.IsAny<Product>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());

            Assert.Equal(1, _product.ProductId);

            

        }
        private List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            for (int i = 0; i <= 10; i++)
            {
                products.Add(new Product
                {
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
