using TomadaStore.Utils.Factories;
using TomadaStore.Utils.Factories.Interfaces;

namespace TomadaStore.Utils;

public class SqlDbConnection : DbConnectionFactory
{
    public override IDBConnection CreateDBConnection()
    {
        return new SqlDbConnectionImpl();
    }
}
