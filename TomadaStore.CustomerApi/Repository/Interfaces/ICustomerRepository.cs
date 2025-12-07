using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.Entities;


namespace TomadaStore.CustomerApi.Repository.Interfaces;

public interface ICustomerRepository
{
    Task InsertCustomerAsync(Customer customer);
    Task<List<CustomerResponseDto>> GetAllCustomerAsync();
    Task<CustomerResponseDto?> GetCustomerById(int id);
    Task UpdateStatusCustomerAsync(int id);
}