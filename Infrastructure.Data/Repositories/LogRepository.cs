using Infrastructure.Data.Mongo.Contexts;
using MongoDB.Driver;
using TomadaStore.Models.Entities;

namespace Infrastructure.Data.Repositories;

public class LogRepository
{
    private readonly IMongoCollection<Log> _logCollection;

    public LogRepository(MongoDbContext mongoDbContext)
    {
        _logCollection = mongoDbContext.Logs;
    }

    public async Task SaveAsync(Log log)
    {
        await _logCollection.InsertOneAsync(log);
    }
}
