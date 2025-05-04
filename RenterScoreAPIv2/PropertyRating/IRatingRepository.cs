namespace RenterScoreAPIv2.PropertyRating;

public interface IRatingRepository
{
    Task<Rating?> GetRatingByPropertyAndUserIdAsync(long propertyId, long userId);
    Task<IEnumerable<Rating>> GetRatingsByPropertyIdAsync(long propertyId);
    Task<decimal> GetAverageRatingByPropertyIdAsync(long propertyId);
} 