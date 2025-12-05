using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Page;
using TomadaStore.Models.Entities;

namespace TomadaStore.CustomerApi.Services.Interfaces;

public interface ICustomerService
{
    Task InsertCustomerAsync(CustomerRequestDto customer);
    Task<CustomerDto> GetAllCustomerAsync(int page);
    Task<CustomerResponseDto> GetCustomerByIdAsync(int id);
}