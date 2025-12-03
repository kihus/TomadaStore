using Microsoft.Extensions.Configuration;
using TomadaStore.Utils.Factories.Interfaces;

namespace TomadaStore.Utils;

internal class SqlDbConnectionImpl : IDBConnection
{
    private readonly string _connection;
    private readonly IConfiguration _configuration;

    public SqlDbConnectionImpl()
    {
        _connection = _configuration.GetConnectionString("SqlServer")!;
    }

    public string ConnectionString()
    {
        return _connection;
    }
}