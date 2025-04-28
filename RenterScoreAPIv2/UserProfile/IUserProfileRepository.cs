namespace RenterScoreAPIv2.UserProfile;

public interface IUserProfileRepository
{
    Task<UserProfile?> GetUserProfileByUserIdAsync(long userId);
} 