using Microsoft.AspNetCore.Mvc;
using TomadaStore.SalesConsumerApi.Services.Interfaces;

namespace TomadaStore.SalesConsumerApi.Controllers.v1;

[Route("api/v1/[controller]")]
[ApiController]
public class SaleConsumerController(
    ISaleConsumerService saleConsumerService
    ) : ControllerBase
{
    private readonly ISaleConsumerService _saleConsumerService = saleConsumerService;

    [HttpPost]
    public async Task<ActionResult> GetSalesFromRabbit()
    {
        try
        {
            await _saleConsumerService.GetSalesFromRabbit();
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }
}
