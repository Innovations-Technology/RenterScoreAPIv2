namespace RenterScoreAPIv2.PropertyImage;

public interface IPropertyImageRepository
{
    Task<IEnumerable<PropertyImage>> GetPropertyImagesByIdsAsync(IEnumerable<long> propertyIds);
    Task<IEnumerable<PropertyImage>> GetPropertyImagesByIdAsync(long propertyId);
} 