namespace RenterScoreAPIv2.PropertyDetailsWithImages;

public interface IPropertyDetailsWithImagesService
{
    Task<IEnumerable<PropertyDetailsWithImages>> GetPropertyDetailsWithImagesListAsync();
    Task<PropertyDetailsWithImages?> GetPropertyDetailsWithImagesAsync(long propertyId);
    
    Task<IEnumerable<PropertyDetailsWithImages>> GetPropertyDetailsWithImagesListAsync(long? userId);
    Task<PropertyDetailsWithImages?> GetPropertyDetailsWithImagesAsync(long propertyId, long? userId);
} 