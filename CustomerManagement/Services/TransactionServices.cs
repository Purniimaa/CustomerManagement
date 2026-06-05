using CustomerManagement.Common;
using CustomerManagement.DTO;
using CustomerManagement.Helper;
using CustomerManagement.Repositories.IDbConnection;
using CustomerManagement.Repositories.ITransactionRepositories;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Numerics;



namespace CustomerManagement.Services
{
    public class TransactionServices(DapperHelper helper) : ITransaction
    {

        //public async Task<int> Login(LoginDTO login)
        //{
        //    try
        //    {
        //        string login_cus = "login_customer";
        //        DynamicParameters param = new DynamicParameters();
        //        param.Add("Username", null);
        //        param.Add("Password", null);

        //        return await helper.ExecuteAsync(login_cus, param);

        //    }
        //    catch(SqlException ex)
        //    {
        //        throw new Exception("Login failed",ex);
        //    }
        //}
        public async Task<int> Deposit(decimal amount,int id)

        {
            try
            {

                string deposit = "usp_transactions";
                DynamicParameters param = new DynamicParameters();
                param.Add("Flag", "d");
                param.Add("CustomerId", id);
                param.Add("Amount", amount);

              return await helper.ExecuteAsync(deposit, param);

            }
            catch (SqlException e)
            {
                throw new Exception($"Database error : {e.Message}", e);

            }
            catch (Exception e)
            {
                throw new Exception($"Error: {e.Message}", e);

            }



        }

        public async Task<int> Withdraw(decimal amount,int id)
        {
            try
            {

                string deposit = "usp_transactions";
                DynamicParameters param = new DynamicParameters();
                param.Add("Flag", "w");
                param.Add("CustomerId", id);
                param.Add("Amount", amount);

                return await helper.ExecuteAsync(deposit, param);

            
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<List<TransactionDTO>>GetAllTransactions()
        {
            try
            {

                {
                    String gettrans = "usp_transactions";
                    DynamicParameters param = new DynamicParameters();
                    param.Add("Flag", "g");
                    param.Add("TransactionId",null);
                    param.Add("CustomerId",null);
                    param.Add("Amount",null);
                    param.Add("Status",null);


                    return await helper.QueryAsync<TransactionDTO>(gettrans, param);


                }
            }
            catch(SqlException e)                                
            {
                throw new Exception($"Database Error:{e.Message}", e);
            }
            catch (Exception e)
            {
                throw new Exception($" Error:{e.Message}", e);
            }
        }

        public async Task<TransactionDTO> GetTransactionById(int id)
        {
            try
            {
                string getbytrans = "usp_transactions";
                DynamicParameters param = new DynamicParameters();
                param.Add("Flag", "gb");
                param.Add("TransactionId", null);
                param.Add("CustomerId", id);
                param.Add("Amount", null);
                param.Add("Status", null);


                return await helper.QueryFirstOrDefaultAsync<TransactionDTO>(getbytrans,param);
                   
                }
            
            catch (SqlException ex)
            {
                throw new Exception($"Error to fetch by id{ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Unexpected error while fetching customer by id {ex.Message}", ex);
            }
        }

        public async Task<decimal> TotalDeposit(int CustomerId)
        {
            try
            {
                string totaldeposit = "usp_transactions";
                DynamicParameters param = new DynamicParameters();
                param.Add("Flag", "tde");
                param.Add("CustomerId", CustomerId);

                return await helper.QuerySingleAsync<decimal>(totaldeposit, param);
                }
            

            catch (SqlException e)
            {
                throw new Exception(e.Message, e);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<decimal> TotalWithdraw(int CustomerId)
        {
            try
            {
                string totalwithdraw = "usp_transactions";
                DynamicParameters param = new DynamicParameters();
                param.Add("Flag", "twi");
                param.Add("CustomerId", CustomerId);

                return await helper.QuerySingleAsync<decimal>(totalwithdraw, param);
            }
            

            catch (SqlException e)
            {
                throw new Exception(e.Message, e);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<List<CustomerTransaction>> CusDeposit(int id)
        {
            try
            {
                string cusdep = "usp_transactions";
                DynamicParameters param = new DynamicParameters();
                param.Add("Flag", "cd");
                param.Add("TransactionId", null);
                param.Add("CustomerId", id);
                param.Add("Name", null);
                param.Add("Email", null);
                param.Add("Phone", null);
                param.Add("Address", null);
                param.Add("Amount", null);
                param.Add("Status", null);
                param.Add("Date", null);

                return await helper.QueryAsync<CustomerTransaction>(cusdep, param);

            }
            catch (SqlException w)
            {
                throw new Exception( w.GetBaseException().Message);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.GetBaseException().Message);
            }
        }

        public async Task<List<CustomerTransaction>> CusWithdraw(int id)
        {
            try
            {

                string cuswit = "usp_transactions";
                DynamicParameters param = new DynamicParameters();
                param.Add("Flag", "cw");
                param.Add("TransactionId", null);
                param.Add("CustomerId", id);
                param.Add("Name", null);
                param.Add("Email", null);
                param.Add("Phone", null);
                param.Add("Address", null);
                param.Add("Amount", null);
                param.Add("Status", null);
                param.Add("Date", null);

                return await helper.QueryAsync<CustomerTransaction>(cuswit, param);
            }
            catch (SqlException w)
            {
                throw new Exception(w.GetBaseException().Message);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.GetBaseException().Message);
            }
        }


    }



    }



