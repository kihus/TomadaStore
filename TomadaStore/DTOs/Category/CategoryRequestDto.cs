using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace TomadaStore.Models.DTOs.Category;

public class CategoryRequestDto
{
    
    [BsonElement("name")]
    [JsonPropertyName("name")]
    public string Name { get; init; }

    [BsonElement("description")]
    [JsonPropertyName("description")]
    public string Description { get; init; }
}
