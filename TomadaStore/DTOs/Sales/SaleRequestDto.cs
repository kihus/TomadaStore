using System.Text.Json.Serialization;

namespace TomadaStore.SalesApi.DTOs.Sales;

public class SaleRequestDto
{
    [JsonPropertyName("customer_id")]
    public required int CustomerId { get; init; }

    [JsonPropertyName("products")]
    public required List<SaleProductRequestDto> Products { get; init; }
}
