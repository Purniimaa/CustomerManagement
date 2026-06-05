using CustomerManagement.Common;
using CustomerManagement.DTO;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.Repositories.ICustomerRepositories
{
    public interface ICustomer
    {
   
        Task<DdResponse> CreateCustomer(CustomerDTO cus);
        Task<List<GetCustomer>> GetallCustomer();
        Task<GetCustomer> GetCustomerById(int id);
        Task<int> UpdateCustomer(CustomerDTO upcus, int id);
        Task<int> DeleteCustomer(int id);




    }
}
