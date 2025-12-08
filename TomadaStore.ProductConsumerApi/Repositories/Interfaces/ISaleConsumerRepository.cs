using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.Entities;
using TomadaStore.SalesApi.DTOs.Sales;

namespace TomadaStore.SalesConsumerApi.Repositories.Interfaces;

public interface ISaleConsumerRepository
{
    Task GetSalesFromRabbit(Sale sales);
}