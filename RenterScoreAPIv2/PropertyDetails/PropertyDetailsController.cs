namespace RenterScoreAPIv2.PropertyDetails;

using global::AutoMapper;
using Microsoft.AspNetCore.Mvc;

[Route("api/v2/property/properties")]
[ApiController]
public class PropertyDetailsController(
    PropertyDetailsRepository propertyRepository,
    IMapper mapper) : ControllerBase
{
    private PropertyDetailsRepository _propertyRepository = propertyRepository;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PropertyDetailsViewModel>>> GetProperties()
    {
        var properties = await _propertyRepository.GetPropertiesWithUserProfilesAsync();
        var vm = _mapper.Map<IEnumerable<PropertyDetailsViewModel>>(properties);
        return Ok(vm);
    }
}