using Microsoft.AspNetCore.Mvc;

namespace TomadaStore.CategoryApi.Controllers.v1;

[Route("api/v1/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreateCategory()
    {
        return NoContent();
    }
}
