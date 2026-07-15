using CustomerManagement.DTO;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Reflection.Metadata.Ecma335;
using CustomerManagement.Repositories.IAuthUser;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Linq.Expressions;
using Microsoft.Identity.Client;

namespace CustomerManagement.Services
{
    public class JwtServices(IConfiguration _config, IAuthRepository repo)
    {
        public async Task<LoginResponse> GenerateToken(string Username)
        {
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var tokenValidityMins = _config.GetValue<int>("Jwt:TokenValidityMins");
            var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(tokenValidityMins);

            var token = new JwtSecurityToken(issuer,
               audience,
               new List<Claim>
               {
                  new Claim(ClaimTypes.Name, Username),

               },
               expires: tokenExpiryTimeStamp,
               signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
              );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            var refreshToken = GenerateRefreshToken();
            
            var rtoken = await SaveRefreshToken(refreshToken,Username);
            return new LoginResponse
            {
                Username = Username,
                AccessToken = accessToken,
                ExpiresIn = (int)(tokenExpiryTimeStamp - DateTime.UtcNow).TotalSeconds,
                RefreshToken = refreshToken,
               
            };
        }
        public async Task<RefreshToken> SaveRefreshToken(string Token, string Username)
        {
            try
            {
                RefreshToken retoken = new RefreshToken()
                {
                    Username = Username,
                    refreshtoken = Token,
                    ExpireDate = DateTime.UtcNow.AddDays(7)

                };
                var res = await repo.SaveRefreshToken(retoken);
                return retoken;
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to generate token{e.GetBaseException().Message}", e);
            }

        }


        private string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }

        public async Task<LoginResponse> ValidateRefreshToken(string Token)
        {
            var refresh = await repo.GetRefreshToken(Token);
            if (refresh is null || refresh.ExpireDate < DateTime.UtcNow)
                return null;

            await repo.RevokeRefreshToken(refresh.refreshtoken);
            return await GenerateToken(refresh.Username);
        }
    }
}
