using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TomadaStore.Models.Entities;

public class Category(
    string name, 
    string description
    )
{
    public ObjectId Id { get; private set; }
    public string Name { get; private set; } = name;
    public string Description { get; private set; } = description;
}