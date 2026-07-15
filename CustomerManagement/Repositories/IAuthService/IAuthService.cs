using CustomerManagement.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.Repositories.IAuthService
{
    public interface IAuthService
    {
        Task<RegisterResponse> Register(Register log);
        Task<LoginResponse> Login([FromBody] Login log);

        
    }
}
