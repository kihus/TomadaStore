using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System.Text;
using System.Text.Json;
using TomadaStore.Models.Entities;
using TomadaStore.PaymentApi.Repositories.Intefaces;

namespace TomadaStore.PaymentApi.Repositories;

public class PaymentRepository(
    IConnectionFactory factory
    ) : IPaymentRepository
{
    private readonly IConnectionFactory _factory = factory;

    public async Task VerifyOrder(Sale sale)
    {
        try
        {
            using var connection = await _factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "payment_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);

            var saleSerialize = JsonSerializer.Serialize(sale);
            var saleMessage = Encoding.UTF8.GetBytes(saleSerialize);

            await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "payment_queue", body: saleMessage);
        }
        catch (RabbitMQClientException rbtEx)
        {
            throw new Exception(rbtEx.Message);
        }
    }
}
