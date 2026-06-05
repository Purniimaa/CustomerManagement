using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CustomerManagement.Services
{
    public class JwtServices
    {
        private readonly IConfiguration _config;

        public JwtServices(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(string Username)
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
    }
    }
