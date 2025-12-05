using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.DTOs.Sales;
using TomadaStore.Models.Entities;
using TomadaStore.SalesApi.DTOs.Sales;

namespace TomadaStore.Models.Extensions;

public static class SaleExtension
{

    public static SaleProductResponseDto ToSaleProduct(this ProductSale dto)
    {
        return new SaleProductResponseDto
        {
            Id = dto.Id.ToString(),
            Name = dto.Name,
            Category = dto.Category,
            Price = dto.Price,
            Quantity = dto.Quantity,
            TotalPrice = dto.TotalPrice
        };
    }

    public static CustomerSale ToCustomerSale(this SaleCustomerResponseDto dto)
    {
        return new CustomerSale(
            dto.Id, 
            dto.FirstName, 
            dto.LastName);
    }
}
