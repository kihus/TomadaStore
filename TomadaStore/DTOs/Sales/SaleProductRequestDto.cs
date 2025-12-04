using MongoDB.Bson;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using TomadaStore.Models.DTOs.Category;
using TomadaStore.Models.Entities;

namespace TomadaStore.SalesApi.DTOs.Sales;
public class SaleProductRequestDto
{
    [JsonPropertyName("product_id")]
    public required string ProductId { get; init; }

    [JsonPropertyName("quantity")]
    public required int Quantity { get; init; } 
}
