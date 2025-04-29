namespace RenterScoreAPIv2.PropertyDetailsWithImages;

using global::AutoMapper;
using RenterScoreAPIv2.PropertyDetails;
using RenterScoreAPIv2.PropertyImage;

public class PropertyDetailsWithImagesService(
    IPropertyDetailsRepository propertyDetailsRepository,
    PropertyImageRepository propertyImageRepository,
    IMapper mapper)
{
    private readonly IPropertyDetailsRepository _propertyDetailsRepository = propertyDetailsRepository;
    private readonly PropertyImageRepository _propertyImageRepository = propertyImageRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<PropertyDetailsWithImages>> GetPropertyDetailsWithImagesListAsync()
    {
        var PropertyDetailsWithImagesList = new List<PropertyDetailsWithImages>();
        var propertyDetailsList = await _propertyDetailsRepository.GetPropertiesWithUserProfilesAsync();

        var propertyIds = propertyDetailsList.Select(pd => pd.Property.PropertyId).ToList();
        var propertyImages = await _propertyImageRepository.GetPropertyImagesByIdsAsync(propertyIds);
        var propertyImagesDict = propertyImages.GroupBy(pi => pi.PropertyId).ToDictionary(g => g.Key, g => g.ToList());
        foreach (var propertyDetails in propertyDetailsList)
        {
            var PropertyDetailsWithImages = _mapper.Map<PropertyDetailsWithImages>(propertyDetails);
            propertyImagesDict.TryGetValue(propertyDetails.Property.PropertyId, out var images);
            if (images != null)
            {
                PropertyDetailsWithImages.PropertyImages = images;
            }
            PropertyDetailsWithImagesList.Add(PropertyDetailsWithImages);
        }
        return PropertyDetailsWithImagesList;
    }

    public async Task<PropertyDetailsWithImages?> GetPropertyDetailsWithImagesAsync(long propertyId)
    {
        var propertyDetails = await _propertyDetailsRepository.GetPropertiesWithUserProfilesByIdAsync(propertyId);
        if (propertyDetails == null) return null;
        var PropertyDetailsWithImages = _mapper.Map<PropertyDetailsWithImages>(propertyDetails);
        PropertyDetailsWithImages.PropertyImages = await _propertyImageRepository.GetPropertyImagesByIdAsync(propertyId);
        return PropertyDetailsWithImages;
    }
}