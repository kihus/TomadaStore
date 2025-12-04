using MongoDB.Bson;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using TomadaStore.Models.DTOs.Category;
using TomadaStore.Models.Entities;

namespace TomadaStore.SalesApi.DTOs.Sales;

public class SaleProductResponseDto
{
    [JsonPropertyName("id")]
    public string Id { get; init; }
    [JsonPropertyName("name")]
    public string Name { get; init; }
    [JsonPropertyName("price")]
    public decimal Price { get; init; }
    [JsonPropertyName("quantity")]
    public int Quantity { get; init; }
    [JsonPropertyName("total_price")]
    public decimal TotalPrice { get; init; }
    [JsonPropertyName("category")]
    public string Category { get; init; }
}
