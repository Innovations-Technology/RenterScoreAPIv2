namespace RenterScoreAPIv2.Health;

using Microsoft.AspNetCore.Mvc;

[Route("api/v2/health")]
[ApiController]
public class HealthController() : ControllerBase
{
    [HttpGet]
    public IActionResult CheckHealth()
    {
        return Ok("Demo Recording...");
    }
}