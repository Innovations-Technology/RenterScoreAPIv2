namespace RenterScoreAPIv2.Tab;

using Microsoft.AspNetCore.Mvc;
using RenterScoreAPIv2.Tab.Tabs;

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
    public async Task<ActionResult<BaseTab>> GetTabData(string tabId, [FromQuery] long? userId = null)
    {
        try
        {
            var tab = _tabFactory.CreateTab(tabId);
            
            if (tabId.ToLower() == "profile")
            {
                if (!userId.HasValue)
                {
                    return BadRequest("UserId is required for profile tab");
                }
                
                if (tab is ProfileTab profileTab)
                {
                    profileTab.SetUserId(userId.Value);
                }
            }
            
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