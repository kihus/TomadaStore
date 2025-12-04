using MongoDB.Bson;
using TomadaStore.SalesApi.DTOs.Sales;
using TomadaStore.Models.Entities;

namespace TomadaStore.SalesApi.Services.Interfaces;

public interface ISaleService
{
    Task CreateSales(SaleRequestDto sales);
    Task<List<SalesResponseDto>> GetAll();
    Task<SalesResponseDto> GetById();
    Task UpdateById(ObjectId id);
}