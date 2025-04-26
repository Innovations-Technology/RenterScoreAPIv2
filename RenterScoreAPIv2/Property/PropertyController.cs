namespace RenterScoreAPIv2.Property;

using global::AutoMapper;
using Microsoft.AspNetCore.Mvc;

[Route("api/properties")]
[ApiController]
public class PropertyController(
    PropertyRepository propertyRepository,
    IMapper mapper) : ControllerBase
{
    private PropertyRepository _propertyRepository = propertyRepository;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Property>>> GetProperties()
    {
        var properties = await _propertyRepository.GetPropertiesWithUserProfilesAsync();
        var vm = _mapper.Map<IEnumerable<PropertyViewModel>>(properties);
        return Ok(vm);
    }
}