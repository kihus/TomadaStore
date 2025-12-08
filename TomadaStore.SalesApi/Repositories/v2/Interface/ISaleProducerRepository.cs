using TomadaStore.Models.Entities;
using TomadaStore.SalesApi.DTOs.Sales;

namespace TomadaStore.SalesApi.Repositories.v2.Interface;

public interface ISaleProducerRepository
{
    Task CreateSaleAsync(Sale sale);
    Task<List<SaleResponseDto>> GetAllSales();
}

