using MongoDB.Bson.Serialization.Attributes;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;

namespace TomadaStore.SalesApi.DTOs.Sales;
public class SalesResponseDto
{
    [BsonElement("id")]
    public string Id { get; init; }
    [BsonElement("customer")]
    public CustomerResponseDto Customer { get; init; }
    [BsonElement("products")]
    public List<SaleProductResponseDto> Products { get; init; }
    [BsonElement("sale_date")]
    public DateTime SaleDate { get; init; }
    [BsonElement("total_price")]
    public decimal TotalPrice { get; init; }
}
