using MongoDB.Bson;
using System.Xml.Linq;

namespace TomadaStore.Models.Entities;

public class ProductSale(
    ObjectId id,
    string name,
    decimal price,
    int quantity,
    string category
    )
{
    public ObjectId Id { get; private set; } = id;
    public string Name { get; private set; } = name;
    public decimal Price { get; private set; } = price;
    public int Quantity { get; private set; } = quantity;
    public decimal TotalPrice { get; private set; } = price * quantity;
    public string Category { get; private set; } = category;
}
