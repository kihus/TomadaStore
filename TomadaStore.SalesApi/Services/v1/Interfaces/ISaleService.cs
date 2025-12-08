using MongoDB.Bson;
using TomadaStore.SalesApi.DTOs.Sales;

namespace TomadaStore.SalesApi.Services.v1.Interfaces;

public interface ISaleService
{
    Task CreateSales(SaleRequestDto sales);
    Task<List<SaleResponseDto>> GetAllSales();
    Task<SaleResponseDto> GetById();
    Task UpdateById(ObjectId id);
}