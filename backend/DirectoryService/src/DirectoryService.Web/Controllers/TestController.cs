using Microsoft.AspNetCore.Mvc;

namespace DirectoryService.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class TestController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            message = "Test endpoint is working!",
        });
    }
}

