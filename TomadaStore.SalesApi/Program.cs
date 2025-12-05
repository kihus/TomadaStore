using TomadaStore.CustomerApi.Data;
using TomadaStore.ProductApi.Data;
using TomadaStore.SalesApi.Repositories;
using TomadaStore.SalesApi.Repositories.Interface;
using TomadaStore.SalesApi.Services;
using TomadaStore.SalesApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.Configure<MongoDbSettings>(
   builder.Configuration.GetSection("MongoDB")
    );

builder.Services.AddSingleton<ConnectionDb>();

builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<ISaleService, SaleService>();

builder.Services.AddHttpClient("CustomersApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:5001/api/v1/customer/");
});

builder.Services.AddHttpClient("ProductsApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:6001/api/v1/product/");
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
