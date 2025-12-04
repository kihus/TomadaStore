using MongoDB.Bson;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.Entities;

namespace TomadaStore.ProductApi.Repositories.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetAllProducts();
    Task<Product> GetProductById(ObjectId name);
    Task<List<Product>> GetSaleProductsAsync(List<ObjectId> productsIds);
    Task CreateProduct(Product product);
    Task UpdateProduct(string id, ProductResponseDto product);
    Task DeleteProduct(string id);
}