using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TomadaStore.Models.DTOs.Category;
using TomadaStore.Models.Entities;

namespace TomadaStore.Models.DTOs.Product;

public class ProductResponseDto
{
    public string Id { get; init; }

    [BsonElement("name")]
    public string Name { get; init; }

    [BsonElement("description")]
    public string Description { get; init; }

    [BsonElement("price")]
    public decimal Price { get; init; }

    [BsonElement("category")]
    public string Category { get; init; }
}
