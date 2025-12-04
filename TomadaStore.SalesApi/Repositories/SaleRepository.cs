using MongoDB.Driver;
using TomadaStore.CustomerApi.Data;
using TomadaStore.Models.Entities;
using TomadaStore.SalesApi.Repositories.Interface;

namespace TomadaStore.SalesApi.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly ILogger<Sale> _logger;
    private readonly IMongoCollection<Sale> _saleCollection;
    private readonly ConnectionDb _connection;

    public SaleRepository(ILogger<Sale> logger, ConnectionDb connection)
    {
        _logger = logger;
        _connection = connection;
        _saleCollection = _connection.GetMongoCollection();
    }

    public async Task CreateSaleAsync(Sale sale)
    {
        try
        {
            await _saleCollection.InsertOneAsync(sale);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
