namespace RenterScoreAPIv2.PropertyDetails;

using Microsoft.EntityFrameworkCore;
using RenterScoreAPIv2.EntityFramework;

public class PropertyDetailsRepository(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<PropertyDetails>> GetPropertiesWithUserProfilesAsync()
    {
        return await (
            from property in _context.Properties
            join userProfile in _context.UserProfiles
            on property.UserId equals userProfile.UserId into userProfilesGroup
            join user in _context.Users
            on property.UserId equals user.UserId into usersGroup
            from user in usersGroup.DefaultIfEmpty()
            from userProfile in userProfilesGroup.DefaultIfEmpty()
            select new PropertyDetails
            {
                Property = property,
                UserProfile = userProfile,
                User = user
            }).ToListAsync();
    }
}