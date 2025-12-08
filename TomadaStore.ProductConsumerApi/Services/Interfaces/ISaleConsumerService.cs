using TomadaStore.SalesApi.DTOs.Sales;

namespace TomadaStore.SalesConsumerApi.Services.Interfaces;

public interface ISaleConsumerService
{
    Task GetSalesFromRabbit();
}