using Microsoft.AspNetCore.Mvc;
using RenterScoreAPIv2.EntityFramework;

namespace RenterScoreAPIv2.PropertyRating;

[ApiController]
[Route("api/v2/rating")]
public class PropertyRatingController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IPropertyRatingService _propertyRatingService;

    public PropertyRatingController(AppDbContext context, IPropertyRatingService propertyRatingService)
    {
        _context = context;
        _propertyRatingService = propertyRatingService;
    }

    [HttpPost]
    public async Task<IActionResult> AddRating([FromBody] AddRatingRequest request)
    {
        var property = await _context.Properties.FindAsync(request.PropertyId);
        if (property == null)
        {
            return NotFound("Property not found");
        }

        var rating = new Rating
        {
            UserId = request.UserId,
            PropertyId = request.PropertyId,
            Cleanliness = request.Cleanliness,
            Traffic = request.Traffic,
            Amenities = request.Amenities,
            Safety = request.Safety,
            ValueForMoney = request.ValueForMoney
        };

        var existingRating = await _context.Ratings.FindAsync(request.UserId, request.PropertyId);
        if (existingRating != null)
        {
            existingRating.Cleanliness = request.Cleanliness;
            existingRating.Traffic = request.Traffic;
            existingRating.Amenities = request.Amenities;
            existingRating.Safety = request.Safety;
            existingRating.ValueForMoney = request.ValueForMoney;
        }
        else
        {
            await _context.Ratings.AddAsync(rating);
        }

        await _context.SaveChangesAsync();
        return Ok(new { message = "Rating added successfully" });
    }
}

public class AddRatingRequest
{
    public required long UserId { get; set; }
    public required long PropertyId { get; set; }
    public required int Cleanliness { get; set; }
    public required int Traffic { get; set; }
    public required int Amenities { get; set; }
    public required int Safety { get; set; }
    public required int ValueForMoney { get; set; }
} 