
using CustomerManagement.Repositories.IDbConnection;
using Microsoft.Data.SqlClient;

namespace CustomerManagement.Services
{
    public class DbConnectionServices(IConfiguration _config) : IConnection
    {
        private string con = _config.GetConnectionString("Default");
        public SqlConnection GetConnection()
        {
            return new SqlConnection(con);
        }
    }
}
