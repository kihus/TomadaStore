using MongoDB.Bson;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.Models.DTOs.Sales;
using TomadaStore.Models.Entities;
using TomadaStore.Models.Entities.Enum;
using TomadaStore.Models.Extensions;
using TomadaStore.SalesApi.DTOs.Sales;
using TomadaStore.SalesApi.Repositories.v2.Interface;
using TomadaStore.SalesApi.Services.v2.Interfaces;

namespace TomadaStore.SalesApi.Services.v2;

public class SaleProducerService(
    ILogger<SaleProducerService> logger, 
    IHttpClientFactory httpClientFactory, 
    ISaleProducerRepository saleRepository
    ) : ISaleProducerService
{
    private readonly ILogger<SaleProducerService> _logger = logger;
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly ISaleProducerRepository _saleRepository = saleRepository;

    public async Task CreateSalesAsync(SaleRequestDto saleDto)
    {
        try
        {
            var customerClient = _httpClientFactory.CreateClient("CustomersApi");
            var customer = await customerClient.GetFromJsonAsync<SaleCustomerResponseDto>($"/api/v1/customer/id/{saleDto.CustomerId.ToString()}")
                ?? throw new Exception("Customer not found!");

            var productsIds = new ProductIdDto
            {
                ProductsIds = [.. saleDto.Products.Select(x => x.ProductId)]
            };
            
            var productClient = _httpClientFactory.CreateClient("ProductsApi");
            var productResponse = await productClient.PostAsJsonAsync($"/api/v1/product/products/", productsIds);

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
                var productSale = new ProductSale(item.Id, item.Name, item.Price, productQuantity.Quantity, item.Category);
                productsSales.Add(productSale);
            }

            var sale = new Sale(customer.ToCustomerSale(), productsSales, EStatus.Pending.ToString());

            await _saleRepository.CreateSaleAsync(sale);

            var client = _httpClientFactory.CreateClient("PaymentApi");
            await client.PostAsync("/api/v1/payment", null);
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
