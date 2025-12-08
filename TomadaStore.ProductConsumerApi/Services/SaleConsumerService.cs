using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using TomadaStore.Models.Entities;
using TomadaStore.SalesConsumerApi.Repositories.Interfaces;
using TomadaStore.SalesConsumerApi.Services.Interfaces;

namespace TomadaStore.SalesConsumerApi.Services;

public class SaleConsumerService(
     IConnectionFactory factory,
     ILogger<SaleConsumerService> logger,
     ISaleConsumerRepository repository
    ) : ISaleConsumerService
{
    private readonly IConnectionFactory _factory = factory;
    private readonly ILogger<SaleConsumerService> _logger = logger;
    private readonly ISaleConsumerRepository _repository = repository;

    public async Task GetSalesFromRabbit()
    {
        try
        {
            List<Sale> sales = null;
            using var connection = await _factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();
            
            await channel.QueueDeclareAsync(queue: "sales_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
            _logger.LogInformation("Receiving sales...");

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var sale = JsonSerializer.Deserialize<Sale>(message);
                _logger.LogInformation(sale.ToString());

                return Task.CompletedTask;
            };
            await channel.BasicConsumeAsync("sales_queue", autoAck: true, consumer: consumer);
            await _repository.GetSalesFromRabbit(sales);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
}
