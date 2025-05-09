namespace RenterScoreAPIv2.PropertyDetailsWithImages;

using global::AutoMapper;
using Microsoft.AspNetCore.Mvc;

[Route("api/v2/property")]
[ApiController]
public class PropertyDetailsWithImagesController(
    IPropertyDetailsWithImagesService propertyDetailsWithImagesService,
    IMapper mapper) : ControllerBase
{
    private IPropertyDetailsWithImagesService _propertyDetailsWithImagesService = propertyDetailsWithImagesService;
    private readonly IMapper _mapper = mapper;

    [HttpGet("properties")]
    public async Task<ActionResult<IEnumerable<PropertyDetailsWithImagesViewModel>>> GetProperties()
    {
        long? userId = null;
        if (Request.Headers.TryGetValue("x-userid", out var userIdHeader) && 
            long.TryParse(userIdHeader, out var parsedUserId))
        {
            userId = parsedUserId;
        }
        
        var propertyDetails = await _propertyDetailsWithImagesService.GetPropertyDetailsWithImagesListAsync(userId);
        var vm = _mapper.Map<IEnumerable<PropertyDetailsWithImagesViewModel>>(propertyDetails);
        return Ok(vm);
    }

    [HttpGet("details/{propertyId}")]
    public async Task<ActionResult<PropertyDetailsWithImagesViewModel>> GetProperties(long propertyId)
    {
        long? userId = null;
        if (Request.Headers.TryGetValue("x-userid", out var userIdHeader) && 
            long.TryParse(userIdHeader, out var parsedUserId))
        {
            userId = parsedUserId;
        }
        
        var propertyDetails = await _propertyDetailsWithImagesService.GetPropertyDetailsWithImagesAsync(propertyId, userId);
        if (propertyDetails == null)
        {
            return NotFound();
        }
        else
        {
            var vm = _mapper.Map<PropertyDetailsWithImagesViewModel>(propertyDetails);
            return Ok(vm);
        }
    }
}