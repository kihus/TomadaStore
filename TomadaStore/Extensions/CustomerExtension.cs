using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.Entities;

namespace TomadaStore.Models.Extensions;

public static class CustomerExtension
{
    public static Customer ToCustomer(this CustomerRequestDto customerDto)
    {
        return new Customer(
            customerDto.FirstName,
            customerDto.LastName,
            customerDto.Email,
            customerDto.PhoneNumber
            );
    }

    public static CustomerResponseDto ToCustomerResponse(this Customer customer)
    {
        return new CustomerResponseDto
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Email = customer.Email,
            Status = ((Status)customer.Status).ToString(),
            PhoneNumber = customer.PhoneNumber
        };
    }
}
