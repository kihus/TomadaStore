using MongoDB.Bson;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.SalesApi.DTOs.Sales;
using TomadaStore.Models.Entities;
using TomadaStore.Models.Extensions;
using TomadaStore.SalesApi.Repositories.Interface;
using TomadaStore.SalesApi.Services.Interfaces;
using TomadaStore.Models.DTOs.Sales;

namespace TomadaStore.SalesApi.Services;

public class SaleService(ILogger<SaleService> logger, IHttpClientFactory httpClientFactory, ISaleRepository saleRepository) : ISaleService
{
    private readonly ILogger<SaleService> _logger = logger;
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly ISaleRepository _saleRepository = saleRepository;

    public async Task CreateSales(SaleRequestDto saleDto)
    {
        try
        {
            var customerClient = _httpClientFactory.CreateClient("CustomersApi");
            var customer = await customerClient.GetFromJsonAsync<SaleCustomerResponseDto>(saleDto.CustomerId.ToString())
                ?? throw new Exception("Customer not found!");

            var productsIds = new ProductIdDto
            {
                ProductsIds = [.. saleDto.Products.Select(x => x.ProductId)]
            };
            
            var productClient = _httpClientFactory.CreateClient("ProductsApi");
            var productResponse = await productClient.PostAsJsonAsync("products/", productsIds);

            if (productResponse.StatusCode == System.Net.HttpStatusCode.NoContent)
                throw new HttpRequestException("Request error");

            var productResponseText = await productResponse.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(productResponseText))
                throw new Exception("Response is null");

            var products = await productResponse.Content.ReadFromJsonAsync<List<ProductResponseDto>>() 
                ?? throw new Exception("Product not found!");

            var productsSales = new List<ProductSale>();

            foreach(var item in products)
            {
                var productQuantity = saleDto.Products.Find(x => x.ProductId.ToString() == item.Id);
                var productSale = new ProductSale(ObjectId.Parse(item.Id), item.Name, item.Price, productQuantity.Quantity, item.Category);
                productsSales.Add(productSale);
            }

            var sale = new Sale(customer.ToCustomerSale(), productsSales);

            await _saleRepository.CreateSaleAsync(sale);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "error");
            throw new Exception(ex.Message);
        }
    }

    public async Task<List<SaleResponseDto>> GetAllSales()
    {
        try
        {
            var sales = await _saleRepository.GetAllSales();
            return sales;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public Task<SaleResponseDto> GetById()
    {
        throw new NotImplementedException();
    }

    public Task UpdateById(ObjectId id)
    {
        throw new NotImplementedException();
    }
}
