using Infrastructure.Data.SQL.Context;
using TomadaStore.CustomerApi.Repository;
using TomadaStore.CustomerApi.Repository.Interfaces;
using TomadaStore.CustomerApi.Services;
using TomadaStore.CustomerApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<SqlConnectionDb>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
