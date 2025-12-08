using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using TomadaStore.Models.Entities;
using TomadaStore.Models.Extensions;
using TomadaStore.PaymentApi.Repositories.Intefaces;
using TomadaStore.PaymentApi.Services.Interfaces;

namespace TomadaStore.PaymentApi.Services;

public class PaymentService(
    IPaymentRepository paymentRepository,
    IConnectionFactory factory
    ) : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository = paymentRepository;
    private readonly IConnectionFactory _factory = factory;

    public async Task VerifyOrder()
    {
        try
        {
            using var connection = await _factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "sales_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var sale = JsonSerializer.Deserialize<Sale>(message)
                    ?? throw new Exception("Sales not found!");

                await _paymentRepository.VerifyOrder(sale.VerifyPrice());
                
            };

            await channel.BasicConsumeAsync(queue: "sales_queue", autoAck: true, consumer: consumer);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
