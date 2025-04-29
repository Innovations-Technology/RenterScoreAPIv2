namespace RenterScoreAPIv2.PropertyDetailsWithImages;

public interface IPropertyDetailsWithImagesService
{
    Task<IEnumerable<PropertyDetailsWithImages>> GetPropertyDetailsWithImagesListAsync();
    Task<PropertyDetailsWithImages?> GetPropertyDetailsWithImagesAsync(long propertyId);
} 