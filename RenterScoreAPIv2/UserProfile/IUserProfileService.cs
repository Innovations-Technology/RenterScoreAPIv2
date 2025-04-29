namespace RenterScoreAPIv2.UserProfile;

public interface IUserProfileService
{
    Task<UserProfileViewModel?> GetUserProfileByUserIdAsync(long userId);
} 