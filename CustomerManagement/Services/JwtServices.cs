using CustomerManagement.DTO;
using System.Text;
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

        public async Task<LoginResponse> GenerateToken(string Username,int CustomerId)
        {
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var tokenValidityMins = _config.GetValue<int>("Jwt:TokenValidityMins");
            var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(tokenValidityMins);

            var token = new JwtSecurityToken (issuer,
               audience,
               new List<Claim>
               {
                  new Claim(ClaimTypes.Name, Username),
                  new Claim("CustomerId", CustomerId.ToString())
               },
               expires: tokenExpiryTimeStamp,
               signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
              );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new LoginResponse
            {
                Username = Username,
                AccessToken = accessToken,
                ExpiresIn = (int)(tokenExpiryTimeStamp - DateTime.UtcNow).TotalSeconds
            };




        }
    }
    }
