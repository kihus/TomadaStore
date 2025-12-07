using MongoDB.Bson;
using TomadaStore.Models.DTOs.Product;

namespace TomadaStore.ProductApi.Services.Interfaces;

public interface IProductService
{
    Task<List<ProductResponseDto>> GetAllProducts();
    Task<ProductResponseDto> GetById(ObjectId name);
    Task<List<ProductResponseDto>> GetSaleProduct(ProductIdDto productsIds);
    Task CreateProductAsync(ProductRequestDto product);
    Task UpdateProduct(string id, ProductRequestDto product);
    Task DeleteProduct(string id);
}