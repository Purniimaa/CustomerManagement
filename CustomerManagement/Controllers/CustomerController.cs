using CustomerManagement.Common;
using CustomerManagement.DTO;
using CustomerManagement.Repositories.ICustomerRepositories;
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
    [ApiExplorerSettings(GroupName = "V1")]

    public class CustomerController(ICustomer _cusServices,IConfiguration _config) : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO log)
        {
            if (log.Username != "admin" || log.Password != "123")
            {
                return Unauthorized("Invalid cerenditals");
            }

            var token = GenerateToken(log.Username);
            return Ok(new { token });
        }

        private string GenerateToken(string Username)
        {
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config["Jwt:key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name,Username),
                new Claim(ClaimTypes.Role,"Admin"),
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> CreateCustomer([FromForm] CustomerDTO cus)
        {

            string ImagePath = "";
            if (cus.Image != null && cus.Image.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var FileName = Guid.NewGuid().ToString() + "_" + cus.Image.FileName;
                var filePath = Path.Combine(uploadsFolder, FileName);

                if (System.IO.File.Exists(filePath)) 
                {
                    return BadRequest("Image doesnot exist");
                }

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await cus.Image.CopyToAsync(fileStream);
                }

                ImagePath = $"/images/{FileName}";
            }
            cus.ImagePath = ImagePath;
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
     
        public async Task<ActionResult> UpdateCustomer( int id, [FromForm] UpdateCustomer upcus)
        {
            var existing = await _cusServices.GetCustomerById(id);
            if (existing == null)
            {
                return NotFound(new { Message = $"Customer with id {id} not found" });
            }
            string? imagePath = null;

            if (upcus.Image != null && upcus.Image.Length > 0)
            {
                var uploadsFolder = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "images");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var FileName =
                    Guid.NewGuid().ToString() + "_" + upcus.Image.FileName;

                var filePath = Path.Combine(uploadsFolder,FileName);

                if (System.IO.File.Exists(filePath))
                {
                    return BadRequest("Image doesnot exist");
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await upcus.Image.CopyToAsync(stream);
                }

                upcus.ImagePath = "/images/" + FileName;
            }
            else
            {
                imagePath = existing.ImagePath;
            }


             imagePath = upcus.ImagePath;

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
                ImagePath = imagePath
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
