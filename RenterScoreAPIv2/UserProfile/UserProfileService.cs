namespace RenterScoreAPIv2.UserProfile;

using global::AutoMapper;

public class UserProfileService : IUserProfileService
{
    private readonly IUserProfileRepository _userProfileRepository;
    private readonly IMapper _mapper;

    public UserProfileService(
        IUserProfileRepository userProfileRepository,
        IMapper mapper)
    {
        _userProfileRepository = userProfileRepository;
        _mapper = mapper;
    }

    public async Task<UserProfileViewModel?> GetUserProfileByUserIdAsync(long userId)
    {
        var userProfile = await _userProfileRepository.GetUserProfileByUserIdAsync(userId);
        
        if (userProfile == null)
        {
            return null;
        }

        return _mapper.Map<UserProfileViewModel>(userProfile);
    }
} 