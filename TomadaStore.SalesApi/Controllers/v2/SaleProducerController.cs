using Microsoft.AspNetCore.Mvc;
using TomadaStore.SalesApi.DTOs.Sales;
using TomadaStore.SalesApi.Services.v2.Interfaces;

namespace TomadaStore.SalesApi.Controllers.v2;

[Route("api/v2/[controller]")]
[ApiController]
public class SaleProducerController(
    ISaleProducerService salesService,
    ILogger<SaleProducerController> logger
    )
    : ControllerBase
{
    private readonly ISaleProducerService _salesService = salesService;
    private readonly ILogger<SaleProducerController> _logger = logger;

    [HttpPost]
    public async Task<ActionResult> CreateSale([FromBody] SaleRequestDto sales)
    {
        try
        {
            await _salesService.CreateSalesAsync(sales);
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
