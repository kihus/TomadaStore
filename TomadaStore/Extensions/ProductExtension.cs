using MongoDB.Bson;
using TomadaStore.Models.DTOs.Category;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.Entities;

namespace TomadaStore.Models.Extensions;

public static class ProductExtension
{
    public static Product ToProduct(this ProductRequestDto productDto)
    {
        return new Product(
            productDto.Name,
            productDto.Description,
            productDto.Price,
            productDto.Category.ToCategory()
            );
    }

    public static ProductResponseDto ToProductResponseDto(this Product product)
    {
        return new ProductResponseDto
        {
            Id = product.Id.ToString(),
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Category = product.Category.Name
        };
    }
}
