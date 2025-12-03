using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.Entities;

namespace TomadaStore.CustomerApi.Services.Interfaces;

public interface ICustomerService
{
    Task InsertCustomerAsync(CustomerRequestDto customer);
    Task<List<CustomerResponseDto>> GetAllCustomerAsync();
    Task<CustomerResponseDto> GetCustomerByIdAsync(int id);
}