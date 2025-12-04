using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace TomadaStore.Models.DTOs.Category;

public class CategoryResponseDto
{
    [BsonElement("id")]
    [JsonPropertyName("id")]
    public string Id { get; init; }
    [BsonElement("name")]
    [JsonPropertyName("name")]
    public string Name { get; init; }

    [BsonElement("description")]
    [JsonPropertyName("description")]
    public string Description { get; init; }
}
