using MongoDB.Bson;
using TomadaStore.SalesApi.DTOs.Sales;

namespace TomadaStore.SalesApi.Services.v2.Interfaces;

public interface ISaleProducerService
{
    Task CreateSalesAsync(SaleRequestDto saleDto);
    Task<List<SaleResponseDto>> GetAllSales();
    Task<SaleResponseDto> GetById();
    Task UpdateById(ObjectId id);
}