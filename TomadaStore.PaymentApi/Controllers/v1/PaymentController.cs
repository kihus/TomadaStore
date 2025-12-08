using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TomadaStore.PaymentApi.Services.Interfaces;

namespace TomadaStore.PaymentApi.Controllers.v1;

[Route("api/v1/[controller]")]
[ApiController]
public class PaymentController(
    IPaymentService paymentService
    ) : ControllerBase
{
    private readonly IPaymentService _paymentService = paymentService;


    [HttpPost]
    public async Task<ActionResult> VerifyOrder()
    {
        try
        {
            await _paymentService.VerifyOrder();
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }
}
