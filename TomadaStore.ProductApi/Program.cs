using TomadaStore.CustomerApi.Data;
using TomadaStore.ProductApi.Data;
using TomadaStore.ProductApi.Repositories;
using TomadaStore.ProductApi.Repositories.Interfaces;
using TomadaStore.ProductApi.Services;
using TomadaStore.ProductApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDB")
    );

builder.Services.AddSingleton<ConnectionDb>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

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
