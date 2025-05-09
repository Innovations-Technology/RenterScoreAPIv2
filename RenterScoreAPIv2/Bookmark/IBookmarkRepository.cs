namespace RenterScoreAPIv2.Bookmark;

public interface IBookmarkRepository
{
    Task<bool> IsPropertyBookmarkedByUserAsync(long propertyId, long userId);
} 