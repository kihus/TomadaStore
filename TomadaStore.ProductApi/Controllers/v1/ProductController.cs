using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using TomadaStore.Models.DTOs.Product;
using TomadaStore.ProductApi.Services.Interfaces;

namespace TomadaStore.ProductApi.Controllers.v1;

[Route("api/v1/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductService _productService;

    public ProductController(ILogger<ProductController> logger, IProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductResponseDto>>> GetAllProductsAsync()
    {
        try
        {
            var products = await _productService.GetAllProducts();

            if (products.Count is 0)
                return NotFound("Register not found");

            return Ok(products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "error");
            return Problem(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductResponseDto>> GetProductByIdAsync(ObjectId id)
    {
        try
        {
            var products = await _productService.GetById(id);

            if (products is null)
                return NotFound("Register not found");

            return Ok(products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "error");
            return Problem(ex.Message);
        }
    }

    [HttpPost("products/")]
    public async Task<ActionResult<List<ProductResponseDto>>> GetProductsAsync([FromBody] ProductIdDto productsId)
    {
        try
        {
            var products = await _productService.GetSaleProduct(productsId);

            if (products is null)
                return NotFound("Register not found");

            return Ok(products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "error");
            return Problem(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> CreateProductAsync(ProductRequestDto product)
    {
        try
        {
            await _productService.CreateProductAsync(product);
            return Created();
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "error");
            return Problem(ex.Message);
        }
    }
}
