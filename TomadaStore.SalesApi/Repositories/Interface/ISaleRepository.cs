using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.DTOs;
using TomadaStore.Models.Entities;

namespace TomadaStore.SalesApi.Repositories.Interface;

public interface ISaleRepository
{
    Task CreateSaleAsync(Sale sale);
}
