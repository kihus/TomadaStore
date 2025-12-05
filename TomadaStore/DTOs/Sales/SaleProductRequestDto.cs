using System.Text.Json.Serialization;

namespace TomadaStore.SalesApi.DTOs.Sales;
public class SaleProductRequestDto
{
    [JsonPropertyName("product_id")]
    public required string ProductId { get; init; }

    [JsonPropertyName("quantity")]
    public required int Quantity { get; init; } 
}
