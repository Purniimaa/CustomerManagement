
using CustomerManagement.DTO;
using CustomerManagement.Helper;
using CustomerManagement.Options;
using CustomerManagement.Repositories.IAuthService;
using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;

namespace CustomerManagement.Services
{
    public class AuthServices(DapperHelper helper, ShaAlgo _sha) : IAuthService
    {
        public async Task<RegisterResponse> Register(Register log)
        {
            try
            {
                string register = "usp_Auth";
                DynamicParameters param = new DynamicParameters();
                param.Add("@Flag",'r');
                param.Add("@Username",log.Username);
                param.Add("@Password", _sha.HashPassword(log.Password));
                param.Add("@ConfirmPassword", _sha.HashPassword(log.ConfirmPassword));
                param.Add("@FirstName", log.FirstName);
                param.Add("@LastName", log.LastName);
                param.Add("@Email", log.Email);
                param.Add("@Phone", log.Phone);
                param.Add("@Address", log.Address);
               

                var result = await helper.QuerySingleAsync<RegisterResponse>(register, param);
                return result;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Database error during registration: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error during registration: {ex.Message}", ex);

            }

        }

        public async Task<LoginResponse> Login(Login log)
        {
            try
            {
                string login = "usp_Auth";
                DynamicParameters param = new DynamicParameters();
                param.Add("@Flag",'l');
                param.Add("@Username", log.Username);
                param.Add("@CustomerId", log.CustomerId);
               

                var result = await helper.QueryFirstOrDefaultAsync<LoginResponse>(login, param);
                
                if (result == null)
                {
                    return null;
                }

                bool isValid = _sha.verify(log.Password, result.Password);
                if (!isValid)
                {
                    return null;
                }

                return result;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Database error during login: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error during login: {ex.Message}", ex);

            }

        }
    }

    }
