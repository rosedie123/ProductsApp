using Moq;
using ProductsApp.Controllers;
using ProductsApp.Models;
using ProductsApp.Services;

namespace ProductsAppTest
{
    public class ProductUnitTest
    {
        private readonly Mock<IProductService> _productService;
        public ProductUnitTest()
        {
            _productService = new Mock<IProductService>();
        }

        [Fact]
        public async Task GetProductList_return_all_product_listAsync()
        {
            
            //Arrange
            
            _productService.Setup(p => p.GetProductsAsync()).Returns(Task.FromResult(GetMockProductsData()));
            var productController = new ProductController(_productService.Object);

            //Act
            var productList = await productController.GetProductList();

            //Assert
            Assert.NotNull(productList);
            Assert.Equal(3, productList.Count());

        }
        [Theory]
        [InlineData(2)]
        public async Task GetProductById(int id)
        {
            //Arrange
            var productList = GetMockProductsData().ToList();
            _productService.Setup(p => p.GetProductByIdAsync(id)).Returns(Task.FromResult(productList[1]));
            var productController = new ProductController(_productService.Object);
            
            //Act
            var product = await productController.GetProductById(id);

            //Asert
            Assert.Equal(productList[1].ProductId, product.ProductId);

        }

        [Fact]
        public async Task AddProduct()
        {
            //Arragne
            var product = new Product { 
                Price = 10,
                ProductDescription = "Cellphone",
                ProductName = "Nokia",
                Stock = 1
            };

            _productService.Setup(p => p.AddProductAsync(product)).Returns(Task.FromResult(1));
            var productController = new ProductController(_productService.Object);

            //Act
            var itemProduct = await productController.AddProduct(product);

            //Asert
            Assert.Equal(1, itemProduct);
        }

        
        private IEnumerable<Product> GetMockProductsData()
        {
            return new List<Product>() { 
                new Product { 
                    ProductId = 1,
                    ProductName = "TV",
                    ProductDescription = "Samsung smart tv",
                    Price = 50000,
                    Stock = 10
                },
                new Product
                {
                    ProductId = 2,
                    ProductName = "IPhone",
                    ProductDescription = "IPhone 12",
                    Price = 55000,
                    Stock = 10
                },
                new Product
                { 
                    ProductId = 3,
                    ProductName = "Laptop",
                    ProductDescription = "Hp Pavillion",
                    Price = 60000,
                    Stock = 10
                }

            };
        }
    }
}
