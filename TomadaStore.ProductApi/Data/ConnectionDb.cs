using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TomadaStore.Models.Entities;
using TomadaStore.ProductApi.Data;

namespace TomadaStore.CustomerApi.Data;

public class ConnectionDb
{
    public readonly IMongoCollection<Product> mongoCollection;
    public ConnectionDb(IOptions<MongoDbSettings> mongoDbSettings)
    {
        MongoClient client = new MongoClient(mongoDbSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
        mongoCollection = database.GetCollection<Product>(mongoDbSettings.Value.CollectionName);
    }

    public IMongoCollection<Product> GetMongoCollection()
    {
        return mongoCollection;
    }
}
