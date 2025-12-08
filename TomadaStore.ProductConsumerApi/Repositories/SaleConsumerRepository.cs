using Infrastructure.Data.Mongo.Contexts;
using MongoDB.Driver;
using TomadaStore.Models.Entities;
using TomadaStore.SalesConsumerApi.Repositories.Interfaces;

namespace TomadaStore.SalesConsumerApi.Repositories;

public class SaleConsumerRepository(
    MongoDbContext mongoDbContext
    ) : ISaleConsumerRepository
{
    private readonly IMongoCollection<Sale> _mongoCollection = mongoDbContext.Sales;

    public async Task GetSalesFromRabbit(Sale sales)
    {
        try
        {
            await _mongoCollection.InsertOneAsync(sales);
        }
        catch (MongoException mongoEx)
        {
            throw new MongoException(mongoEx.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
