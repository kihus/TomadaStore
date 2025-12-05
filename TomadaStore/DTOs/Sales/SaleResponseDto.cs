using MongoDB.Bson.Serialization.Attributes;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.Entities;

namespace TomadaStore.SalesApi.DTOs.Sales;
public class SaleResponseDto
{
    [BsonElement("id")]
    public string Id { get; init; }
    [BsonElement("customer")]
    public CustomerSale Customer { get; init; }
    [BsonElement("products")]
    public List<SaleProductResponseDto> Products { get; init; }
    [BsonElement("sale_date")]
    public DateTime SaleDate { get; init; }
    [BsonElement("total_price")]
    public decimal TotalPrice { get; init; }
}
