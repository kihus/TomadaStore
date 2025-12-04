using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.Entities;
using TomadaStore.SalesApi.DTOs.Sales;

namespace TomadaStore.Models.Extensions;

public static class SaleExtension
{
    public static Sale ToSale(this SaleRequestDto dto)
    {
        throw new NotImplementedException();
    }

    public static SaleProductResponseDto ToSaleProduct(this Product dto)
    {
        return new SaleProductResponseDto
        {
            Id = dto.Id.ToString(),
            Name = dto.Name,
            Category = dto.Category.Name,
            Price = dto.Price
        };
    }
}
