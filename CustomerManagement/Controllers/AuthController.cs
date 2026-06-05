using CustomerManagement.DTO;
using CustomerManagement.Repositories.IAuthService;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "V1")]

    public class AuthController(IAuthService _authService) : ControllerBase
    {

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] Register log)
       {
            var result = await _authService.Register(log);
            if (result != null)
            {
                return Ok(new
                {
                    Message = "Registration successful",
                    CustomerId = result.CustomerId,
                    Username = result.Username,
                    
                });
            }
            else
            {
                return BadRequest("Registration failed");
            }

           
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] Login log)
        {
            var result = await _authService.Login(log);
            if (result != null)
            {
                return Ok(new
                {
                    Message = "Login successful",
                  
                });
            }
            else
            {
                return BadRequest("Login failed");
            }


        }
    }
}
