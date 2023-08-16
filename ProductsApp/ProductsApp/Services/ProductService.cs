using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using ProductsApp.Data;
using ProductsApp.Models;
using System.Data;

namespace ProductsApp.Services
{
    public class ProductService : IProductService
    {
        private readonly DbMainContext _dbContext;


        public ProductService(DbMainContext dbContext)
        {
            _dbContext = dbContext;

        }

        public async Task<int> AddProductAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            
           return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteProductAsync(int id)
        {
            var product = await _dbContext.Products.Where(p=>p.ProductId == id).FirstOrDefaultAsync();

            _dbContext.Products.Remove(product);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _dbContext.Products.Where(p => p.ProductId == id).FirstOrDefaultAsync();
        }

        public async Task<int> UpdateProductAsync(Product product)
        {
            _dbContext.Products.Update(product);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> InsertBulkProducts(List<Product> products)
        {
            try
            {
               await _dbContext.BulkInsertAsync(products);
               return 1;
            } catch (Exception e)
            {
                throw;
            }
           
        }

        public Product CreateProduct(Product product)
        {
            var _product = _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
            return _product.Entity;

        }
    }
}
