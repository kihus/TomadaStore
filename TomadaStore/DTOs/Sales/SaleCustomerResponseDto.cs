using System.Text.Json.Serialization;

namespace TomadaStore.Models.DTOs.Sales;

public class SaleCustomerResponseDto
{
    public int Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
}