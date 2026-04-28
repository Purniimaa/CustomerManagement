using CustomerManagement.Common;
using CustomerManagement.DTO;
using CustomerManagement.Repositories.ICustomerRepositories;
using CustomerManagement.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;


namespace CustomerManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "customer")]


    public class CustomerController(ICustomer _cusServices) : ControllerBase
    {

        [HttpPost]
        public async Task<ActionResult> CreateCustomer([FromBody] CustomerDTO cus)
        {
            var CustId = await _cusServices.CreateCustomer(cus);

            if (!CustId.Code.Equals(200))
                return BadRequest(new {Message = CustId.Message });
            return Ok(
                new
                {
                    Message = "Customer created successfully!",
                    Code = CustId.Code,
                });

        }
        [HttpGet]
        public async Task<ActionResult<List<GetCustomer>>> GetallCustomer()
        {
            var customers = await _cusServices.GetallCustomer();
            if (customers == null || !customers.Any())
            {
                return NotFound(
                    new
                    {
                        Message = "Unable to retrieve all customer",
                        Code = "404"
                    });

            }
            return Ok(new { customers});
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<GetCustomer>> GetCustomerById(int id)
        {
            var getbyid = await _cusServices.GetCustomerById(id);

            if (getbyid == null)
                return NotFound(
                    new
                    {
                        Message = $"Customer with id {id} is not found",
                        Code = "404"
                    }
                );

            return Ok(new {getbyid , Message="Customer found"});

        }



        [HttpPut("{id}")]

        public async  Task<ActionResult> UpdateCustomer([FromBody]UpdateCustomer upcus,int id)

        {
            //updateCustomer.CID = id;
            var  updated =await _cusServices.UpdateCustomer(upcus,id);
            

            if (updated == 0)
                return BadRequest(new { Message = "Customer cannot be update!" });
            return Ok(
                new
                {
                    Message = "Customer updated successfully!",
                   
                });


        }

        [HttpDelete]

        public async Task<ActionResult<DdResponse>>  DeleteCustomer(int id)
        {
            int deleted = await _cusServices.DeleteCustomer(id);

            if (deleted == 0)
                return BadRequest(new { Message = "Customer cannot be delete!" });
            return Ok(
                new
                {
                    Message = "Customer deleted successfully!",

                });


        }




    }
}
