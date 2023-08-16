using Microsoft.AspNetCore.Mvc;
using ProductsApp.Models;
using ProductsApp.Services;

namespace ProductsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;    
        }

        [HttpPost("adddapperbulkproduct")]
        public async Task AddDapperBulkProduct(List<Customer> customer)
        {
            await _customerService.InsertBulkProduct(customer);
        }
    }
}
