namespace RenterScoreAPIv2.PropertyRating;

public interface IPropertyRatingService
{
    Task<PropertyRatingViewModel?> GetPropertyRatingAsync(long propertyId, long userId);
    Task<PropertyRatingViewModel> GetAveragePropertyRatingAsync(long propertyId);
} 