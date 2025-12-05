using MongoDB.Driver;
using System.Security.Cryptography.X509Certificates;
using TomadaStore.CustomerApi.Data;
using TomadaStore.Models.Entities;
using TomadaStore.Models.Extensions;
using TomadaStore.SalesApi.DTOs.Sales;
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
        catch (MongoException mongoEx)
        {
            _logger.LogError(mongoEx, "Mongo error");
            throw new MongoException(mongoEx.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "error");
            throw new Exception(ex.Message);
        }
    }

    public async Task<List<SaleResponseDto>> GetAllSales()
    {
        try
        {
            var sale = await _saleCollection.Find(_ => true)
                .Project(sale => new
                {
                    sale.Id,
                    sale.Products,
                    sale.Customer,
                    sale.SaleDate,
                    sale.TotalPrice
                })
                .ToListAsync();

            var saleResponse = sale.Select(s => new SaleResponseDto
            {
                Id = s.Id.ToString(),
                Products = [.. s.Products.Select(p => p.ToSaleProduct())],
                Customer = s.Customer,
                SaleDate = s.SaleDate,
                TotalPrice = s.TotalPrice
            }).ToList();

            return saleResponse;
        }
        catch (MongoException mongoEx)
        {
            _logger.LogError(mongoEx, "Mongo error");
            throw new MongoException(mongoEx.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "error");
            throw new Exception(ex.Message);
        }
    }
}
