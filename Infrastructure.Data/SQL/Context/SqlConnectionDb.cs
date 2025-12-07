using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data.SQL.Context;

public class SqlConnectionDb
{
    private readonly string _connection;

    public SqlConnectionDb(IConfiguration configuration)
    {
        _connection = configuration.GetConnectionString("SqlServer")!;
    }

    public SqlConnection GetConnectionString()
    {
        return new SqlConnection(_connection);
    }
}
