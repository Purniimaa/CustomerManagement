using CustomerManagement.DTO;

namespace CustomerManagement.Repositories.IAuthService
{
    public interface IAuthService
    {
        Task<RegisterResponse> Register(Register log);
        Task<LoginResponse> Login(Login log);
    }
}
