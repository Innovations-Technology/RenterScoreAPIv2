namespace RenterScoreAPIv2.UserProfile;

using Microsoft.EntityFrameworkCore;
using RenterScoreAPIv2.EntityFramework;

public class UserProfileRepository(AppDbContext context) : IUserProfileRepository
{
    private readonly AppDbContext _context = context;

    public async Task<UserProfile?> GetUserProfileByUserIdAsync(long userId)
    {
        return await _context.UserProfiles
            .Where(up => up.UserId == userId)
            .FirstOrDefaultAsync();
    }
} 