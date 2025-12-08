using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using TomadaStore.Models.Entities;
using TomadaStore.Models.Entities.Enum;
using TomadaStore.SalesApi.DTOs.Sales;
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
            using var connection = await _factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();
            
            await channel.QueueDeclareAsync(queue: "payment_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var sale = JsonSerializer.Deserialize<Sale>(message);
                await _repository.GetSalesFromRabbit(sale);
                return;
            };

            await channel.BasicConsumeAsync("payment_queue", autoAck: true, consumer: consumer);
           
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
}
