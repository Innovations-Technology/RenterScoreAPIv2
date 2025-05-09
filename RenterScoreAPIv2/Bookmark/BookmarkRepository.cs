namespace RenterScoreAPIv2.Bookmark;

using Microsoft.EntityFrameworkCore;
using RenterScoreAPIv2.EntityFramework;

public class BookmarkRepository(AppDbContext context) : IBookmarkRepository
{
    private readonly AppDbContext _context = context;

    public async Task<bool> IsPropertyBookmarkedByUserAsync(long propertyId, long userId)
    {
        return await _context.Bookmarks
            .AnyAsync(b => b.PropertyId == propertyId && b.UserId == userId);
    }
} 