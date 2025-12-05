using TomadaStore.CustomerApi.Repository.Interfaces;
using TomadaStore.CustomerApi.Services.Interfaces;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Page;
using TomadaStore.Models.Entities;
using TomadaStore.Models.Extensions;

namespace TomadaStore.CustomerApi.Services;

public class CustomerService : ICustomerService
{
    private readonly ILogger<CustomerService> _logger;
    private readonly ICustomerRepository _customerRespository;

    public CustomerService(ILogger<CustomerService> logger, ICustomerRepository customerRepository)
    {
        _logger = logger;
        _customerRespository = customerRepository;
    }

    public async Task<CustomerDto> GetAllCustomerAsync(int page)
    {
        var currentPage = 1;

        if (page > 1)
            currentPage = page;

        try
        {
            var customers = await _customerRespository.GetAllCustomerAsync();
            var quantityCustomers = customers.Count;

            var customersList = customers.Skip(currentPage > 1 ? 20 * (currentPage - 1) : 0).Take(20).ToList();

            var info = new Info
            {
                CustomerQuantity = quantityCustomers,
                Pages = (quantityCustomers <= 20
                            ? 1
                            : (quantityCustomers / 20) + 1),
                Next = (customersList.Count >= 20 ? $"https://localhost:5001/api/v1/customer/?page={currentPage + 1}" : null),
                Previous = (currentPage > 1 ? $"https://localhost:5001/api/v1/customer/?page={currentPage - 1}" : null)
            };

            var customer = new CustomerDto
            {
                Info = info,
                Result = customersList
            };

            return customer;
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

        }
        catch (Exception ex)
        {
            _logger.LogError($"SQL Error insert customer: {ex.StackTrace}");

            throw new Exception(ex.Message);
        }
    }
}
