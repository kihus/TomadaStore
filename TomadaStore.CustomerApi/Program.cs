using TomadaStore.CustomerApi.Data;
using TomadaStore.CustomerApi.Repository;
using TomadaStore.CustomerApi.Repository.Interfaces;
using TomadaStore.CustomerApi.Services;
using TomadaStore.CustomerApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddSingleton<ConnectionDb>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
