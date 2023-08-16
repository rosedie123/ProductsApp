using Dapper;
using Microsoft.Data.SqlClient;
using ProductsApp.Models;
using System.Data;

namespace ProductsApp.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IConfiguration _configuration;

        public CustomerService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> InsertBulkProduct(List<Customer> products)
        {
            using (IDbConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                using (var trans = con.BeginTransaction())
                {

                    try
                    {
                        var result = await con.ExecuteAsync(@"Insert Customers(CustomerName)
                                           Values(@CustomerName)", products, trans);
                        trans.Commit();
                        return result;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw;
                    }
                    finally
                    {
                        con.Close();
                    }
                }

            }
        }
    }
}
