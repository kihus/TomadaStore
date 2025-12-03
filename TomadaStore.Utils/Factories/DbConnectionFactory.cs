using System.Security.Cryptography.X509Certificates;
using TomadaStore.Utils.Factories.Interfaces;

namespace TomadaStore.Utils.Factories;

public abstract class DbConnectionFactory
{
    public abstract IDBConnection CreateDBConnection();

    public string GetDBConnection()
    {
        var connection = CreateDBConnection();
        return connection.ConnectionString();
    }
}
