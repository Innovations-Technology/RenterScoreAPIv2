namespace RenterScoreAPIv2.PropertyDetails;

using Microsoft.EntityFrameworkCore;
using RenterScoreAPIv2.EntityFramework;

public class PropertyDetailsRepository(
    AppDbContext context) : IPropertyDetailsRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<PropertyDetails>> GetPropertiesWithUserProfilesAsync()
    {
        var propertyDetails = await (
            from property in _context.Properties
            join userProfile in _context.UserProfiles
            on property.UserId equals userProfile.UserId into userProfilesGroup
            join user in _context.Users
            on property.UserId equals user.UserId into usersGroup
            from user in usersGroup.DefaultIfEmpty()
            from userProfile in userProfilesGroup.DefaultIfEmpty()
            orderby property.ModifiedDate descending
            where property.PropertyState != "SUSPENDED"
            select new PropertyDetails
            {
                Property = property,
                UserProfile = userProfile,
                User = user,
            }).ToListAsync();

        return propertyDetails;
    }

    public async Task<PropertyDetails?> GetPropertiesWithUserProfilesByIdAsync(long propertyId)
    {
        var propertyDetails = await (
            from property in _context.Properties
            join userProfile in _context.UserProfiles
            on property.UserId equals userProfile.UserId into userProfilesGroup
            join user in _context.Users
            on property.UserId equals user.UserId into usersGroup
            from user in usersGroup.DefaultIfEmpty()
            from userProfile in userProfilesGroup.DefaultIfEmpty()
            where property.PropertyId == propertyId
            select new PropertyDetails
            {
                Property = property,
                UserProfile = userProfile,
                User = user,
            }).FirstOrDefaultAsync();

        return propertyDetails;
    }
}