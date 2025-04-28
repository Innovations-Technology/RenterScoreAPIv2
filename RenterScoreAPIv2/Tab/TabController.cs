namespace RenterScoreAPIv2.Tab;

using Microsoft.AspNetCore.Mvc;

[Route("api/v2/tab")]
[ApiController]
public class TabController : ControllerBase
{
    private readonly ITabFactory _tabFactory;

    public TabController(ITabFactory tabFactory)
    {
        _tabFactory = tabFactory;
    }

    [HttpGet("{tabId}")]
    public async Task<ActionResult<BaseTab>> GetTabData(string tabId)
    {
        try
        {
            var tab = _tabFactory.CreateTab(tabId);
            await tab.LoadDataAsync();
            return Ok(tab);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
} 