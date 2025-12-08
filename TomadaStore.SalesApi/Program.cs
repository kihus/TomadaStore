using Infrastructure.Data.Mongo.Contexts;
using RabbitMQ.Client;
using TomadaStore.SalesApi.Repositories.v1.Interface;
using TomadaStore.SalesApi.Repositories.v1;
using TomadaStore.SalesApi.Repositories.v2.Interface;
using TomadaStore.SalesApi.Services.v1;
using TomadaStore.SalesApi.Services.v1.Interfaces;
using TomadaStore.SalesApi.Repositories.v2;
using TomadaStore.SalesApi.Services.v2.Interfaces;
using TomadaStore.SalesApi.Services.v2;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton<MongoDbContext>();

builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<ISaleService, SaleService>();
builder.Services.AddScoped<ISaleProducerRepository, SaleProducerRepository>();
builder.Services.AddScoped<ISaleProducerService, SaleProducerService>();

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

builder.Services.AddHttpClient("CustomersApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:9001");
});

builder.Services.AddHttpClient("ProductsApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:3001");
});

builder.Services.AddHttpClient("PaymentApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7036");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
