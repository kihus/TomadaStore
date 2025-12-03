using TomadaStore.Models.Entities;

namespace TomadaStore.Models.DTOs.Customer;

public class CustomerResponseDto
{
    public int Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string Status { get; init; }
    public string? PhoneNumber { get; init; }
}
