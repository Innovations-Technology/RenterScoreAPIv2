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
    public async Task<ActionResult<IEnumerable<PropertyDetailsWithImagesViewModel>>> GetProperties(
        [FromHeader(Name = "x-userid")] long? userId
    )
    {        
        var propertyDetails = await _propertyDetailsWithImagesService.GetPropertyDetailsWithImagesListAsync(userId);
        var vm = _mapper.Map<IEnumerable<PropertyDetailsWithImagesViewModel>>(propertyDetails);
        return Ok(vm);
    }

    [HttpGet("details/{propertyId}")]
    public async Task<ActionResult<PropertyDetailsWithImagesViewModel>> GetProperties(
        long propertyId,
        [FromHeader(Name = "x-userid")] long? userId)
    {
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

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<PropertyDetailsWithImagesViewModel>>> SearchProperties(
        [FromQuery] string title,
        [FromHeader(Name = "x-userid")] long? userId)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return BadRequest("Search title cannot be empty");
        }
        var propertyDetails = await _propertyDetailsWithImagesService.SearchPropertyDetailsWithImagesByTitleAsync(title, userId);
        var vm = _mapper.Map<IEnumerable<PropertyDetailsWithImagesViewModel>>(propertyDetails);
        return Ok(vm);
    }
}