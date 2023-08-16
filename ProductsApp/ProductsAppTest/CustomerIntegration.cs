using Microsoft.Extensions.Configuration;
using Moq;
using ProductsApp.Models;
using ProductsApp.Services;

namespace ProductsAppTest
{
    public class CustomerIntegration
    {
        private IConfiguration _configuration;

        public CustomerIntegration()
        {
            var mockConfSection = new Mock<IConfigurationSection>();
            mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "DefaultConnection")]).Returns("Server=(localdb)\\mssqllocaldb;Database=Demo;Trusted_Connection=True;MultipleActiveResultSets=true");

            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings"))).Returns(mockConfSection.Object);

            _configuration = mockConfiguration.Object;
        }

        [Fact]

        public void InsertDapperBulkProducs()
        {
            //Arrange
            ICustomerService customerService = new CustomerService(_configuration);

            //Act
            var result = customerService.InsertBulkProduct(GetCustomer());
            //Assert
            Assert.True(result.Result > 0);

        }


        private List<Customer> GetCustomer()
        {
            List<Customer> products = new List<Customer>();
            for (int i = 0; i <= 10; i++)
            {
                products.Add(new Customer
                {
                    CustomerName = $"CustomerName_{i}"
                }); 
            }

            return products;
        }
    }
}
