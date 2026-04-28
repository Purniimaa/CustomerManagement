using CustomerManagement.Common;
using CustomerManagement.DTO;
using CustomerManagement.Helper;
using CustomerManagement.Repositories.ICustomerRepositories;
using CustomerManagement.Repositories.IDbConnection;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Numerics;

namespace CustomerManagement.Services
{
    public class CustomerServices(
        DapperHelper helper
        ) : ICustomer
    {
        public async Task<DdResponse> CreateCustomer(CustomerDTO cus)
        {
            try
            {
                string insertCustomer = "usp_customer";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Flag", "i");
                parameters.Add("@Name", cus.Name);
                parameters.Add("@Email", cus.Email);
                parameters.Add("@Phone", cus.Phone);
                parameters.Add("@Address", cus.Address);
                parameters.Add("@Balance", cus.Balance);

                return await helper.QuerySingleAsync<DdResponse>(insertCustomer, parameters);

            }
            catch (SqlException ex)
            {
                throw new Exception($"Database error while adding customer {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Unexpected error while adding customer {ex.Message}", ex);
            }

        }

        public async Task<List<GetCustomer>> GetallCustomer()
        {
            try
            {
                string getcustomer = "usp_customer";
                DynamicParameters param = new DynamicParameters();
                param.Add("Flag", "g");
                param.Add("Name", null);
                param.Add("Email", null);
                param.Add("Phone", null);
                param.Add("Address", null);
                param.Add("Balance", null);
                param.Add("CustomerId", null);


                return await helper.QueryAsync<GetCustomer>(getcustomer, param);

            }
            catch (SqlException ex)
            {
                throw new Exception($"Database error while retrieving customer {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while retrieving customer {ex.Message}", ex);
            }
        }


        public async Task<GetCustomer> GetCustomerById(int id)
        {
            try
            {
                string getbyid = "usp_customer";
                DynamicParameters param = new DynamicParameters();
                param.Add("@Flag", "b");
                param.Add("@CustomerId", id);
                return await helper.QueryFirstOrDefaultAsync<GetCustomer>(getbyid,param);     

            }
            
            catch (SqlException ex)
            {
                throw new Exception($"Error to fetch by id{ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Unexpected error while fetching customer by id {ex.Message}", ex);
            }
        }


        public async Task<int> UpdateCustomer(UpdateCustomer upcus,int id)
        {
            try
            {
                string update = "usp_customer";
                DynamicParameters param = new DynamicParameters();
                param.Add("@Flag", "u");
                param.Add("@Customerid",id);
                param.Add("@Name",upcus.Name);
                param.Add("@Email", upcus.Email);
                param.Add("@Phone", upcus.Phone);
                param.Add("@Address", upcus.Address);

                int rowsaffected = await helper.ExecuteAsync(update, param);
                return rowsaffected;

            }
            
            catch (SqlException ex)
            {
                throw new Exception($"Database error during updating data{ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Unexpected error while updating data {ex.Message}", ex);
            }
        }

        public async Task<int> DeleteCustomer(int id)
        {
            try
            {
                string deletedbyid = "usp_customer";
                DynamicParameters param = new DynamicParameters();
                param.Add("@Flag", "d");
                param.Add("@CustomerId", id);
                int result = await helper.ExecuteAsync(deletedbyid, param);
                return result;

            }
            catch (SqlException ex)
            {
                throw new Exception($"Database error while deleting customer {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to delete {ex.Message}", ex);
            }

        }
    }
}
