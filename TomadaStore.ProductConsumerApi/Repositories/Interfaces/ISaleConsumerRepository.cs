using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.Entities;

namespace TomadaStore.SalesConsumerApi.Repositories.Interfaces;

public interface ISaleConsumerRepository
{
    Task GetSalesFromRabbit(List<Sale> sales);
}