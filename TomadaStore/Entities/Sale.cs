using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TomadaStore.Models.Entities.Enum;

namespace TomadaStore.Models.Entities;

public class Sale(
    CustomerSale customer, 
    List<ProductSale> products,
    string status
    )
{
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; private set; }

    [BsonElement("customer")]
    public CustomerSale Customer { get; private set; } = customer;

    [BsonElement("products")]
    public List<ProductSale> Products { get; private set; } = products;

    [BsonElement("status")]
    public string Status { get; private set; } = status.ToString();

    [BsonElement("sale_date")]
    public DateTime SaleDate { get; private set; } = DateTime.Now;

    [BsonElement("total_price")]
    public decimal TotalPrice { get; private set; } = products.Sum(x => x.TotalPrice);
}
