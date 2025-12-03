using Microsoft.Data.SqlClient;

namespace TomadaStore.CustomerApi.Data;

public class ConnectionDb
{
    private readonly string _connectionString;

    public ConnectionDb(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("SqlServer")!;
    }

    public SqlConnection GetConnectionString()
    {
        return new SqlConnection(_connectionString);
    }
}
