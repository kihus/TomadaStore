using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
using TomadaStore.Models.DTOs.Category;
using TomadaStore.Models.Entities;

namespace TomadaStore.Models.DTOs.Product;

public class ProductRequestDto
{
    [BsonElement("name")]
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [BsonElement("description")]
    [JsonPropertyName("description")]
    public required string Description { get; init; }

    [BsonElement("price")]
    [JsonPropertyName("price")]
    public required decimal Price { get; init; }

    [BsonElement("category")]
    [JsonPropertyName("category")]
    public required CategoryRequestDto Category { get; init; } 
}
