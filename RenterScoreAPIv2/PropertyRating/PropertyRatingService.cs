namespace RenterScoreAPIv2.PropertyRating;

using System;

public class PropertyRatingService(IRatingRepository ratingRepository) : IPropertyRatingService
{
    private readonly IRatingRepository _ratingRepository = ratingRepository;

    public async Task<PropertyRatingViewModel?> GetPropertyRatingAsync(long propertyId, long userId)
    {
        var rating = await _ratingRepository.GetRatingByPropertyAndUserIdAsync(propertyId, userId);
        
        if (rating == null)
        {
            return new PropertyRatingViewModel
            {
                PropertyId = propertyId,
                Cleanliness = 0,
                Traffic = 0,
                Amenities = 0,
                Safety = 0,
                ValueForMoney = 0,
                Total = 0
            };
        }
        
        return new PropertyRatingViewModel
        {
            PropertyId = rating.PropertyId,
            Cleanliness = rating.Cleanliness,
            Traffic = rating.Traffic,
            Amenities = rating.Amenities,
            Safety = rating.Safety,
            ValueForMoney = rating.ValueForMoney,
            Total = rating.Total
        };
    }

    public async Task<PropertyRatingViewModel> GetAveragePropertyRatingAsync(long propertyId)
    {
        var ratings = await _ratingRepository.GetRatingsByPropertyIdAsync(propertyId);
        
        if (!ratings.Any())
        {
            return new PropertyRatingViewModel
            {
                PropertyId = propertyId,
                Cleanliness = 0,
                Traffic = 0,
                Amenities = 0,
                Safety = 0,
                ValueForMoney = 0,
                Total = 0
            };
        }
        
        return new PropertyRatingViewModel
        {
            PropertyId = propertyId,
            Cleanliness = (int)Math.Round(ratings.Average(r => r.Cleanliness)),
            Traffic = (int)Math.Round(ratings.Average(r => r.Traffic)),
            Amenities = (int)Math.Round(ratings.Average(r => r.Amenities)),
            Safety = (int)Math.Round(ratings.Average(r => r.Safety)),
            ValueForMoney = (int)Math.Round(ratings.Average(r => r.ValueForMoney)),
            Total = ratings.Average(r => r.Total)
        };
    }
} 