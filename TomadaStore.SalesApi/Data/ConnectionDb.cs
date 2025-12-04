using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TomadaStore.Models.Entities;
using TomadaStore.ProductApi.Data;

namespace TomadaStore.CustomerApi.Data;

public class ConnectionDb
{
    public readonly IMongoCollection<Sale> mongoCollection;
    public ConnectionDb(IOptions<MongoDbSettings> mongoDbSettings)
    {
        MongoClient client = new MongoClient(mongoDbSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
        mongoCollection = database.GetCollection<Sale>(mongoDbSettings.Value.CollectionName);
    }

    public IMongoCollection<Sale> GetMongoCollection()
    {
        return mongoCollection;
    }
}
