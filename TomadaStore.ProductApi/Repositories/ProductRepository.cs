using Infrastructure.Data.Mongo.Contexts;
using Infrastructure.Data.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.Entities;
using TomadaStore.ProductApi.Repositories.Interfaces;

namespace TomadaStore.ProductApi.Repositories;

public class ProductRepository(
    ILogger<ProductRepository> logger, 
    MongoDbContext connection,
    LogRepository logRepository
    ) : IProductRepository
{
    private readonly ILogger<ProductRepository> _logger = logger;
    private readonly IMongoCollection<Product> _mongoCollection = connection.Products;
    private readonly LogRepository _logRepository = logRepository;

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
            var log = new Log(DateTime.Now.ToLongDateString(), "teste", "testando stack", "teste 2", DateTime.UtcNow);
            var filter = Builders<Product>.Filter.Empty;
            await _logRepository.SaveAsync(log);
            
            return _mongoCollection.Find(filter).ToList();
        }
        catch (MongoException mongoEx)
        {
            var log = new Log(DateTime.Now.ToLongDateString(), mongoEx.Message, mongoEx.StackTrace, mongoEx.Source, DateTime.UtcNow);
            await _logRepository.SaveAsync(log);
            throw new Exception(mongoEx.Message);
        }
        catch (Exception ex)
        {
            var log = new Log(DateTime.Now.ToLongDateString(), ex.Message, ex.StackTrace, ex.Source, DateTime.UtcNow);
            await _logRepository.SaveAsync(log);
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
