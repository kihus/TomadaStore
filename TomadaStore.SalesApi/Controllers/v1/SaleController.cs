using Microsoft.AspNetCore.Mvc;
using TomadaStore.SalesApi.DTOs.Sales;
using TomadaStore.SalesApi.Services.v1.Interfaces;

namespace TomadaStore.SalesApi.Controllers.v1;

[Route("api/v1/[controller]")]
[ApiController]
public class SaleController(
    ISaleService salesService, 
    ILogger<SaleController> logger
    ) 
    : ControllerBase
{
    private readonly ISaleService _salesService = salesService;
    private readonly ILogger<SaleController> _logger = logger;


    [HttpPost]
    public async Task<ActionResult> CreateSale([FromBody] SaleRequestDto sales)
    {
        try
        {
            await _salesService.CreateSales(sales);
            return Created();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "error");
            return Problem(ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<SaleResponseDto>>> GetAllSales()
    {
        try
        {
            var sales = await _salesService.GetAllSales();

            if (sales.Count == 0)
                return NotFound("Register not found");

            return Ok(sales);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "error");
            return Problem(ex.Message);
        }
    }
}
