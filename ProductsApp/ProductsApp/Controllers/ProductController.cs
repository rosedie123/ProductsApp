using Microsoft.AspNetCore.Mvc;
using ProductsApp.Models;
using ProductsApp.Services;


namespace ProductsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("productlist")]
        public async Task<IEnumerable<Product>> GetProductList()
        {
           var products =  await _productService.GetProductsAsync();
           return products;
        }

        [HttpGet("getbyproductid/{id}")]
        public async Task<Product?> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return product;
        }

        [HttpPost("addproduct")]
        public async Task<int> AddProduct(Product product)
        {
            return await _productService.AddProductAsync(product);
        }

        [HttpPost("updateproduct")]
        public async Task<int> UpdateProduct(Product product)
        {
            return await _productService.UpdateProductAsync(product);    
        }

        [HttpDelete("deleteproduct/{id}")]
        public async Task<int> DeleteProduct(int id)
        {
            return await _productService.DeleteProductAsync(id);
        }

        [HttpPost("addbulkproduct")]
        public async Task AddBulkProduct(List<Product> product)
        {
             await _productService.InsertBulkProducts(product);
        }

    }
}
