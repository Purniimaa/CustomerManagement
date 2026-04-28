using CustomerManagement.DTO;
using CustomerManagement.Repositories.IDbConnection;
using Dapper;
using System.Data;

namespace CustomerManagement.Helper
{
    public class DapperHelper(IConnection dbCon)
    {
        public async Task<T> QuerySingleAsync<T>(string sql, DynamicParameters patram)
        {
            using var con = dbCon.GetConnection();
            con.Open();
            return await con.QuerySingleAsync<T>(sql, patram, commandType:CommandType.StoredProcedure);
        }

        public async Task<List<T>> QueryAsync<T>(string sql, DynamicParameters param)
        {
            using var con = dbCon.GetConnection();
            con.Open();
            var result = await con.QueryAsync<T>(sql, param, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql,DynamicParameters param)
        {
            using var con = dbCon.GetConnection();
            con.Open();
            var result = await con.QueryFirstOrDefaultAsync<T>(sql, param, commandType: CommandType.StoredProcedure);
            return result;  
            
        }

        public async Task<int> ExecuteAsync(string sql,DynamicParameters param)
        {
            using var con = dbCon.GetConnection();
            con.Open();
            int rowsaffected = await con.ExecuteAsync(sql,param,commandType:CommandType.StoredProcedure);
            return rowsaffected;
        }
    }
}

