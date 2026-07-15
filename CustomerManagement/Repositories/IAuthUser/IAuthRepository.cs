using CustomerManagement.DTO;

namespace CustomerManagement.Repositories.IAuthUser
{
    public interface IAuthRepository
    {
        Task<int> SaveRefreshToken(RefreshToken Token);
        Task<RefreshToken?> GetRefreshToken(string token);

        Task<int> RevokeRefreshToken(string token);
    }
}
