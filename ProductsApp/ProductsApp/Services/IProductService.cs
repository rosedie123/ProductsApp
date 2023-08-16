using ProductsApp.Models;

namespace ProductsApp.Services
{
    public interface IProductService
    {
        public Task<IEnumerable<Product>> GetProductsAsync();
        public Task<Product?> GetProductByIdAsync(int id);
        public Task<int> AddProductAsync(Product product);
        public Task<int> DeleteProductAsync(int id);
        public Task<int> UpdateProductAsync(Product product);

        public Task<int> InsertBulkProducts(List<Product> products);

        public Product CreateProduct(Product product);
        

    }
}
