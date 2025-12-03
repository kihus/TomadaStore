using TomadaStore.CustomerApi.Repository.Interfaces;
using TomadaStore.CustomerApi.Services.Interfaces;
using TomadaStore.Models.DTOs.Customer;
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

    public async Task<List<CustomerResponseDto>> GetAllCustomerAsync()
    {
        try
        {
            var customers = await _customerRespository.GetAllCustomerAsync();
            var quantityCustomers = customers.Count;
            
            if (quantityCustomers is 0)
                return null;

            var info = new Info
            {
                CustomerQuantity = quantityCustomers,
                Pages = (quantityCustomers < 20 
                            ? 1
                            : (quantityCustomers)/20)

                 
            };


            return customers;
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
}
