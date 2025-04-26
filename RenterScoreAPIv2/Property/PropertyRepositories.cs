namespace RenterScoreAPIv2.Property;

using Microsoft.EntityFrameworkCore;
using RenterScoreAPIv2.EntityFramework;

public class PropertyRepository(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<Property>> GetAllPropertiesAsync()
    {
        return await _context.Properties.ToListAsync();
    }

    public async Task<IEnumerable<PropertyWithUserProfile>> GetPropertiesWithUserProfilesAsync()
    {
        return await (
            from property in _context.Properties
            join userProfile in _context.UserProfiles
            on property.UserId equals userProfile.UserId into userProfilesGroup
            from userProfile in userProfilesGroup.DefaultIfEmpty()
            select new PropertyWithUserProfile
            {
                Property = property,
                UserProfile = userProfile
            }).ToListAsync();
    }
}