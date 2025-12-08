using TomadaStore.Models.Entities;
using TomadaStore.SalesApi.DTOs.Sales;

namespace TomadaStore.SalesApi.Repositories.v1.Interface;

public interface ISaleRepository
{
    Task CreateSaleAsync(Sale sale);
    Task<List<SaleResponseDto>> GetAllSales();
}

