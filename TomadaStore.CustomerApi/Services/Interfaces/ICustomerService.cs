using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Page;
using TomadaStore.Models.DTOs.Result;

namespace TomadaStore.CustomerApi.Services.Interfaces;

public interface ICustomerService
{
    Task InsertCustomerAsync(CustomerRequestDto customer);
    Task<Result<CustomerResponseDto>> GetAllCustomerAsync(PageDto dto);
    Task<CustomerResponseDto> GetCustomerByIdAsync(int id);
}