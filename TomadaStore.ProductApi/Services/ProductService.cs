using MongoDB.Bson;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.Extensions;
using TomadaStore.ProductApi.Repositories.Interfaces;
using TomadaStore.ProductApi.Services.Interfaces;

namespace TomadaStore.ProductApi.Services;

public class ProductService(
    ILogger<ProductService> logger,
    IProductRepository repository
    ) : IProductService
{
    private readonly ILogger<ProductService> _logger = logger;
    private readonly IProductRepository _productRepository = repository;

    public async Task CreateProductAsync(ProductRequestDto product)
    {
        try
        {
            await _productRepository.CreateProduct(product.ToProduct());
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public Task DeleteProduct(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ProductResponseDto>> GetAllProducts()
    {
        try
        {
            var product = await _productRepository.GetAllProducts();
            return [.. product.Select(p => p.ToProductResponseDto())]  ;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }

    public async Task<ProductResponseDto> GetById(ObjectId id)
    {
        try
        {
            var product = await _productRepository.GetProductById(id);

            return product.ToProductResponseDto();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<List<ProductResponseDto>> GetSaleProduct(ProductIdDto productsIds)
    {
        try
        {
            var products = await _productRepository.GetSaleProductsAsync(productsIds.ProductsIds.Select(x => ObjectId.Parse(x)).ToList());
            return [.. products.Select(x => x.ToProductResponseDto())];
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public Task UpdateProduct(string id, ProductRequestDto product)
    {
        throw new NotImplementedException();
    }
}
