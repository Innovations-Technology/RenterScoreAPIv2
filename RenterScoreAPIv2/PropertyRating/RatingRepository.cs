namespace RenterScoreAPIv2.PropertyRating;

using Microsoft.EntityFrameworkCore;
using RenterScoreAPIv2.EntityFramework;

public class RatingRepository(AppDbContext context) : IRatingRepository
{
    private readonly AppDbContext _context = context;

    public async Task<Rating?> GetRatingByPropertyAndUserIdAsync(long propertyId, long userId)
    {
        return await _context.Ratings
            .Where(r => r.PropertyId == propertyId && r.UserId == userId)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Rating>> GetRatingsByPropertyIdAsync(long propertyId)
    {
        return await _context.Ratings
            .Where(r => r.PropertyId == propertyId)
            .ToListAsync();
    }
    
    public async Task<decimal> GetAverageRatingByPropertyIdAsync(long propertyId)
    {
        var ratings = await _context.Ratings
            .Where(r => r.PropertyId == propertyId)
            .ToListAsync();
            
        if (!ratings.Any())
        {
            return 0;
        }
        
        return ratings.Average(r => r.Total);
    }
} 