using Infrastructure.Data.Mongo.Contexts;
using RabbitMQ.Client;
using TomadaStore.SalesConsumerApi.Repositories;
using TomadaStore.SalesConsumerApi.Repositories.Interfaces;
using TomadaStore.SalesConsumerApi.Services;
using TomadaStore.SalesConsumerApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton<MongoDbContext>();

builder.Services.AddSingleton<IConnectionFactory>(con =>
{
    var factory = new ConnectionFactory
    {
        HostName = "localhost",
        UserName = "guest",
        Password = "guest"
    };
    return factory;
});

builder.Services.AddScoped<ISaleConsumerRepository, SaleConsumerRepository>();
builder.Services.AddScoped<ISaleConsumerService, SaleConsumerService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
