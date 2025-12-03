using System;
namespace TomadaStore.Models.DTOs.Customer
{
    public class CustomerRequestDto
    {
        public required string FirstName { get; init; } 
        public required string LastName { get; init; }
        public required string Email { get; init; }
        public string? PhoneNumber { get; init; }
    }
}
