
using CustomerManagement.DTO;
using CustomerManagement.Helper;
using CustomerManagement.Options;
using CustomerManagement.Repositories.IAuthService;
using Dapper;
using System;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;

namespace CustomerManagement.Services
{
    public class AuthServices(DapperHelper helper, ShaAlgo _sha, JwtServices _jwtServices) : IAuthService
    {
        public async Task<RegisterResponse> Register(Register log)
        {
            try
            {
                string register = "usp_Auth";
                DynamicParameters param = new DynamicParameters();
                param.Add("@Flag", 'r');
                param.Add("@Username", log.Username);

                var hashed = _sha.HashPassword(log.Password);
                param.Add("@Password", hashed);
                param.Add("@ConfirmPassword", hashed);
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
                param.Add("@Flag", 'l');
                param.Add("@Username", log.Username);


                if (string.IsNullOrWhiteSpace(log.Username) || string.IsNullOrWhiteSpace(log.Password))
                {
                    throw new ArgumentException("Username and password must not be empty.");
                }
                var result = await helper.QueryFirstOrDefaultAsync<Login>(login, param);

                if (result is null)
                    return null;

                if (!string.Equals(result.Username, log.Username))
                    return null;
                bool isValid = _sha.verify(log.Password, result.Password);
                if (!isValid)
                    return null;

                return await _jwtServices.GenerateToken(result.Username);

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

