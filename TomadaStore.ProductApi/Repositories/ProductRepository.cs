using MongoDB.Bson;
using MongoDB.Driver;
using TomadaStore.CustomerApi.Data;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.Entities;
using TomadaStore.ProductApi.Repositories.Interfaces;

namespace TomadaStore.ProductApi.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ILogger<ProductRepository> _logger;
    private readonly IMongoCollection<Product> _mongoCollection;
    private readonly ConnectionDb _connection;

    public ProductRepository(ILogger<ProductRepository> logger, ConnectionDb connection)
    {
        _logger = logger;
        _connection = connection;
        _mongoCollection = connection.GetMongoCollection();
    }

    public async Task CreateProduct(Product product)
    {
        try
        {
            await _mongoCollection.InsertOneAsync(product);
        }
        catch (MongoException mongoEx)
        {
            _logger.LogError(mongoEx, "Mongo error");
            throw new Exception(mongoEx.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "error");
            throw new Exception(ex.Message);
        }
    }

    public Task DeleteProduct(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Product>> GetAllProducts()
    {
        try
        {
            var filter = Builders<Product>.Filter.Empty;
            return _mongoCollection.Find(filter).ToList();
        }
        catch (MongoException mongoEx)
        {
            _logger.LogError(mongoEx, "Mongo error");
            throw new Exception(mongoEx.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "error");
            throw new Exception(ex.Message);
        }
    }

    public async Task<Product> GetProductById(ObjectId id)
    {
        try
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            return await _mongoCollection.FindAsync(filter).Result.FirstOrDefaultAsync();
        }
        catch (MongoException mongoEx)
        {
            _logger.LogError(mongoEx, "Mongo error");
            throw new Exception(mongoEx.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "error");
            throw new Exception(ex.Message);
        }
    }

    public async Task<List<Product>> GetSaleProductsAsync(List<ObjectId> products)
    {
        try
        {
            var filter = Builders<Product>.Filter.In(p => p.Id, products);
            return await _mongoCollection.FindAsync(filter).Result.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public Task UpdateProduct(string id, ProductResponseDto product)
    {
        throw new NotImplementedException();
    }
}
