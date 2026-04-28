using Microsoft.Data.SqlClient;

namespace CustomerManagement.Repositories.IDbConnection
{
    public interface IConnection
    {
        SqlConnection GetConnection();
    }
}
