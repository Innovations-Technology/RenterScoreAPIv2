namespace RenterScoreAPIv2.PropertyDetails;

public interface IPropertyDetailsRepository
{
    Task<IEnumerable<PropertyDetails>> GetPropertiesWithUserProfilesAsync();
    Task<PropertyDetails?> GetPropertiesWithUserProfilesByIdAsync(long propertyId);
    Task<IEnumerable<PropertyDetails>> SearchPropertiesByTitleAsync(string titleSearch);
}
