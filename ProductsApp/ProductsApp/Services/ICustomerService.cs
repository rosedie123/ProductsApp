using ProductsApp.Models;

namespace ProductsApp.Services
{
    public interface ICustomerService
    {
        public Task<int> InsertBulkProduct(List<Customer> products);
    }
}
