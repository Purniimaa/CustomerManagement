using CustomerManagement.DTO;
using CustomerManagement.Repositories.IAuthService;
using CustomerManagement.Services;
using CustomerManagement.Model;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CustomerManagement.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "V1")]

    public class AuthController(IAuthService _authService, JwtServices _jwtService) : ControllerBase
    {

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] Register log)
        {
            if (string.IsNullOrWhiteSpace(log.Password) || string.IsNullOrWhiteSpace(log.ConfirmPassword))
            {
                return BadRequest("Password and ConfirmPassword are required.");
            }

            if (!string.Equals(log.Password, log.ConfirmPassword))
            {
                return BadRequest("Passwords do not match.");
            }

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
        public async Task<ActionResult<LoginResponse>> Login([FromBody] Login log)
        {
            try
            {
               
                var result = await _authService.Login(log);

                if (result == null)
                {
                    return Unauthorized(new
                    {
                        Message = "Invalid username or password"
                    });
                }

                return Ok(new
                {
                    Message = "Login successful",
                    AccessToken = result.AccessToken,
                    ExpiresIn = result.ExpiresIn,
                    RefreshToken = result.RefreshToken
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = ex.Message
                });
            }
        }
        [AllowAnonymous]
        [HttpPost("RefreshToken")]
        public async Task<ActionResult<LoginResponse>> RefreshToken([FromBody] RefreshRequestModel request)
        {
            var result = await _jwtService.ValidateRefreshToken(request.Token);
            if (result == null)
            {
                return Unauthorized(new
                {
                    Message = "Invalid or expired refresh token"
                });
            }

            return result is not null ? result :Unauthorized();
        }
    }
}

