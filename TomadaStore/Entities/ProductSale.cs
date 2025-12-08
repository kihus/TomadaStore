using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Xml.Linq;

namespace TomadaStore.Models.Entities;

public class ProductSale(
    string id,
    string name,
    decimal price,
    int quantity,
    string category
    )
{
    [BsonElement("id")]
    public string Id { get; private set; } = id;

    [BsonElement("name")]
    public string Name { get; private set; } = name;

    [BsonElement("price")]
    public decimal Price { get; private set; } = price;

    [BsonElement("quantity")]
    public int Quantity { get; private set; } = quantity;

    [BsonElement("total_price")]
    public decimal TotalPrice { get; private set; } = price * quantity;

    [BsonElement("category")]
    public string Category { get; private set; } = category;
}
