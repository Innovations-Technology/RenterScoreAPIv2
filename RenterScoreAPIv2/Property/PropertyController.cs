namespace RenterScoreAPIv2.Property;

using Microsoft.AspNetCore.Mvc;

[Route("api/properties")]
[ApiController]
public class PropertyController : ControllerBase
{
    private PropertyRepository _propertyRepository;

    public PropertyController(PropertyRepository propertyRepository)
    {
        _propertyRepository = propertyRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Property>>> GetProperties()
    {
        var properties = await _propertyRepository.GetAllPropertiesAsync();
        return Ok(properties);
    }
}