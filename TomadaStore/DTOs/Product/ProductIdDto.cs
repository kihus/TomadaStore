using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace TomadaStore.Models.DTOs.Product;

public class ProductIdDto
{
    [JsonPropertyName("products_ids")]
    public List<string> ProductsIds { get; init; }
}
