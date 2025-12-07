using TomadaStore.CustomerApi.Repository.Interfaces;
using TomadaStore.CustomerApi.Services.Interfaces;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Page;
using TomadaStore.Models.DTOs.Result;
using TomadaStore.Models.Extensions;

namespace TomadaStore.CustomerApi.Services;

public class CustomerService(
    ILogger<CustomerService> logger, 
    ICustomerRepository customerRepository
    ) : ICustomerService
{
    private readonly ILogger<CustomerService> _logger = logger;
    private readonly ICustomerRepository _customerRespository = customerRepository;

    public async Task<Result<CustomerResponseDto>> GetAllCustomerAsync(PageDto page)
    {
        var currentPage = 1;

        if (page.Page > 1)
            currentPage = page.Page;

        try
        {
            var customers = await _customerRespository.GetAllCustomerAsync();
            var quantityCustomers = customers.Count;

            var list = customers.Skip(currentPage > 1 ? 20 * (currentPage - 1) : 0).ToList();

            string url = "https://localhost:9001/api/v1";

            return list.ToResult(currentPage, quantityCustomers, list, url);
        }
        catch (Exception ex)
        {
            _logger.LogError($"SQL Error insert customer: {ex.StackTrace}");

            throw new Exception(ex.Message);
        }
    }

    public async Task<CustomerResponseDto> GetCustomerByIdAsync(int id)
    {
        try
        {
            var customer = await _customerRespository.GetCustomerById(id);
            if (customer is null)
                return null;

            return customer;
        }
        catch (Exception ex)
        {
            _logger.LogError($"SQL Error insert customer: {ex.StackTrace}");

            throw new Exception(ex.Message);
        }
    }

    public async Task InsertCustomerAsync(CustomerRequestDto customer)
    {
        try
        {
            await _customerRespository.InsertCustomerAsync(customer.ToCustomer());
        }
        catch (Exception ex)
        {
            _logger.LogError($"SQL Error insert customer: {ex.StackTrace}");

            throw new Exception(ex.Message);
        }
    }

    public async Task UpdateStatusCustomerAsync(int id)
    {
        try
        {
           await _customerRespository.UpdateStatusCustomerAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError($"SQL Error insert customer: {ex.StackTrace}");

            throw new Exception(ex.Message);
        }
    }
}
