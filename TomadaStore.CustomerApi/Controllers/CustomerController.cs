using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TomadaStore.CustomerApi.Services;
using TomadaStore.CustomerApi.Services.Interfaces;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Page;


namespace TomadaStore.CustomerApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;

        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateCustomer([FromBody] CustomerRequestDto customer)
        {
            try
            {
                _logger.LogInformation("Creating a new customer");
                await _customerService.InsertCustomerAsync(customer);
                return Ok("Customer created succesfully");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{page}")]
        public async Task<ActionResult<List<CustomerResponseDto>>> GetAllCustomers([FromQuery] int page)
        {
            try
            {
                _logger.LogInformation("Get all customers");
                var custormers = await _customerService.GetAllCustomerAsync(page);

                if (custormers is null)
                    return NotFound("Register not found!");

                return Ok(custormers);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<CustomerRequestDto>> GetCustomerById(int id)
        {
            try
            {
                _logger.LogInformation("Get customer by id");
                var customer = await _customerService.GetCustomerByIdAsync(id);

                if (customer is null)
                    return NotFound("Register not found!");

                return Ok(customer);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}

