using Infrastructure.Data.Mongo.Contexts;
using MongoDB.Driver;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using TomadaStore.Models.Entities;
using TomadaStore.Models.Extensions;
using TomadaStore.SalesApi.DTOs.Sales;
using TomadaStore.SalesApi.Repositories.v2.Interface;

namespace TomadaStore.SalesApi.Repositories.v2;

public class SaleProducerRepository(
    ILogger<Sale> logger, 
    MongoDbContext connection, 
    IConnectionFactory factory
    ) : ISaleProducerRepository
{
    private readonly ILogger<Sale> _logger = logger;
    private readonly IMongoCollection<Sale> _saleCollection = connection.Sales;
    private readonly IConnectionFactory _factory = factory;

    public async Task CreateSaleAsync(Sale sale)
    {
        try
        {

            using var connection = await _factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "sales_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);

            var saleSerialize = JsonSerializer.Serialize(sale);

            var saleMessage = Encoding.UTF8.GetBytes(saleSerialize);

            await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "sales_queue", body: saleMessage);
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
