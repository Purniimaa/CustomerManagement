using CustomerManagement.Common;
using CustomerManagement.DTO;
using CustomerManagement.Model;
using CustomerManagement.Repositories;
using CustomerManagement.Repositories.ICustomerRepositories;
using CustomerManagement.Repositories.IUploadService;
using CustomerManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;


namespace CustomerManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "V2")]

    public class CustomerController(ICustomer _cusServices,IConfiguration _config, JwtServices _jwtservices,IUploadService _imservice) : ControllerBase
    {
        //[HttpPost("login")]
        //public IActionResult Login([FromBody] LoginDTO log)
        //{
        //    if (log.Username != "admin" || log.Password != "123")
        //    {
        //        return Unauthorized("Invalid cerenditals");
        //    }

        //    var token = _jwtservices.GenerateToken(log.Username);
        //    return Ok(new { token });
        

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> CreateCustomer([FromForm] CustomerDTO cus)
        {
          
            var uploadResult = await _imservice.UploadFile(cus.ImageFile);

            cus.ImagePath = uploadResult.imagePath;
            cus.FileName = uploadResult.fileName;
            cus.FileType = FileTypes.Image;

            var CustId = await _cusServices.CreateCustomer(cus);

            if (!CustId.Code.Equals(200))
            {
                return BadRequest(new
                {
                    Message = CustId.Message
                });
            }

            return Ok(new
            {
                Message = "Customer created successfully!",
                Code = CustId.Code,
                ImagePath = uploadResult.imagePath
            });
        }

        [Authorize]
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

        [Authorize]
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


        [Authorize]
        [HttpPut("{id}")]

        public async Task<ActionResult> UpdateCustomer(int id, [FromForm] CustomerDTO upcus)
        {
            if (upcus.ImageFile != null)
            {
                var uploadResult = await _imservice.UploadFile(upcus.ImageFile);

                upcus.ImagePath = uploadResult.imagePath;
                upcus.FileName = uploadResult.fileName;
                upcus.FileType = FileTypes.Image;
            }
            var updated = await _cusServices.UpdateCustomer(upcus, id);

            if (updated == 0)
            {
                return BadRequest(new
                {
                    Message = "Customer cannot be updated!"
                });
            }

            return Ok(new
            {
                Message = "Customer updated successfully!",
                
            });
        }
        [Authorize]
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
