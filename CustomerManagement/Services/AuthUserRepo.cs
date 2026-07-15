using CustomerManagement.Helper;
using CustomerManagement.Repositories.IAuthUser;
using CustomerManagement.DTO;
using Dapper;
using CustomerManagement.Model;
namespace CustomerManagement.Services
{
    public class AuthUserRepo(DapperHelper helper) : IAuthRepository
    {
        public async Task<int> SaveRefreshToken(RefreshToken Token)
        {
            string refresh = "SaveRefreshToken";
            DynamicParameters param = new DynamicParameters();
            param.Add("Flag", 'i');
            param.Add("Username", Token.Username);
            param.Add("RefreshToken", Token.refreshtoken);
            param.Add("ExpireDate", Token.ExpireDate);
            return await helper.ExecuteAsync(refresh, param);
        }
        public async Task<RefreshToken?> GetRefreshToken(string token)
        {
            string refresh = "SaveRefreshToken";
            DynamicParameters param = new DynamicParameters();
            param.Add("Flag", 'g');
            param.Add("Username", null);
            param.Add("RefreshToken", token);
            param.Add("ExpireDate", null);
            var tok = await helper.QueryFirstOrDefaultAsync<RefreshToken>(refresh, param);
            return tok;
        }

        public async Task<int> RevokeRefreshToken(string token)
        {
            string refresh = "SaveRefreshToken";
            DynamicParameters param = new DynamicParameters();
            param.Add("Flag", 'r');
            param.Add("Username", null);
            param.Add("RefreshToken", token);
            param.Add("ExpireDate", null);
            return await helper.ExecuteAsync(refresh, param);
        }

    
    }
}
