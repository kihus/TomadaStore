using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using TomadaStore.Models.Entities;

namespace Infrastructure.Data.Mongo.Contexts;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MongoDb");
        var databaseName = configuration["DatabaseName"];

        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    public IMongoCollection<Category> Categories
        => _database.GetCollection<Category>("Categories");

    public IMongoCollection<Product> Products
        => _database.GetCollection<Product>("Products");

    public IMongoCollection<Sale> Sales
        => _database.GetCollection<Sale>("Sales");

    public IMongoCollection<Log> Logs
        => _database.GetCollection<Log>("Logs");
}
