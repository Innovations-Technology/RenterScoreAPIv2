namespace RenterScoreAPIv2.PropertyDetailsWithImages;

using global::AutoMapper;
using RenterScoreAPIv2.PropertyDetails;
using RenterScoreAPIv2.PropertyImage;
using RenterScoreAPIv2.PropertyRating;
using RenterScoreAPIv2.Bookmark;
using System.Collections.Generic;

public class PropertyDetailsWithImagesService : IPropertyDetailsWithImagesService
{
    private readonly IPropertyDetailsRepository _propertyDetailsRepository;
    private readonly PropertyImageRepository _propertyImageRepository;
    private readonly IPropertyRatingService _propertyRatingService;
    private readonly IBookmarkRepository _bookmarkRepository;
    private readonly IMapper _mapper;

    public PropertyDetailsWithImagesService(
        IPropertyDetailsRepository propertyDetailsRepository,
        PropertyImageRepository propertyImageRepository,
        IPropertyRatingService propertyRatingService,
        IBookmarkRepository bookmarkRepository,
        IMapper mapper)
    {
        _propertyDetailsRepository = propertyDetailsRepository;
        _propertyImageRepository = propertyImageRepository;
        _propertyRatingService = propertyRatingService;
        _bookmarkRepository = bookmarkRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PropertyDetailsWithImages>> GetPropertyDetailsWithImagesListAsync()
    {
        return await GetPropertyDetailsWithImagesListAsync(null);
    }

    public async Task<PropertyDetailsWithImages?> GetPropertyDetailsWithImagesAsync(long propertyId)
    {
        return await GetPropertyDetailsWithImagesAsync(propertyId, null);
    }

    public async Task<IEnumerable<PropertyDetailsWithImages>> GetPropertyDetailsWithImagesListAsync(long? userId)
    {
        var PropertyDetailsWithImagesList = new List<PropertyDetailsWithImages>();
        var propertyDetailsList = await _propertyDetailsRepository.GetPropertiesWithUserProfilesAsync();

        var propertyIds = propertyDetailsList.Select(pd => pd.Property.PropertyId).ToList();
        var propertyImages = await _propertyImageRepository.GetPropertyImagesByIdsAsync(propertyIds);
        var propertyImagesDict = propertyImages.GroupBy(pi => pi.PropertyId).ToDictionary(g => g.Key, g => g.ToList());
        
        foreach (var propertyDetails in propertyDetailsList)
        {
            var PropertyDetailsWithImages = _mapper.Map<PropertyDetailsWithImages>(propertyDetails);
            
            // Add property images
            propertyImagesDict.TryGetValue(propertyDetails.Property.PropertyId, out var images);
            PropertyDetailsWithImages.PropertyImages = images ?? new List<PropertyImage>();
            
            // Add property rating
            var propertyRating = await _propertyRatingService.GetAveragePropertyRatingAsync(propertyDetails.Property.PropertyId);
            PropertyDetailsWithImages.PropertyRating = propertyRating;
            
            // Check if property is bookmarked by the user
            if (userId.HasValue)
            {
                PropertyDetailsWithImages.IsBookmarked = await _bookmarkRepository.IsPropertyBookmarkedByUserAsync(
                    propertyDetails.Property.PropertyId, userId.Value);
            }
            
            PropertyDetailsWithImagesList.Add(PropertyDetailsWithImages);
        }
        return PropertyDetailsWithImagesList;
    }

    public async Task<PropertyDetailsWithImages?> GetPropertyDetailsWithImagesAsync(long propertyId, long? userId)
    {
        var propertyDetails = await _propertyDetailsRepository.GetPropertiesWithUserProfilesByIdAsync(propertyId);
        if (propertyDetails == null) return null;
        
        var PropertyDetailsWithImages = _mapper.Map<PropertyDetailsWithImages>(propertyDetails);
        
        // Add property images
        var images = await _propertyImageRepository.GetPropertyImagesByIdAsync(propertyId);
        PropertyDetailsWithImages.PropertyImages = images;
        
        // Add property rating
        var propertyRating = await _propertyRatingService.GetAveragePropertyRatingAsync(propertyId);
        PropertyDetailsWithImages.PropertyRating = propertyRating;
        
        // Check if property is bookmarked by the user
        if (userId.HasValue)
        {
            PropertyDetailsWithImages.IsBookmarked = await _bookmarkRepository.IsPropertyBookmarkedByUserAsync(propertyId, userId.Value);
        }
        
        return PropertyDetailsWithImages;
    }
}