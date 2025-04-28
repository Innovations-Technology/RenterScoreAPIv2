namespace RenterScoreAPIv2.Tab.Cards;

using RenterScoreAPIv2.UserProfile;
using System.Text.Json.Serialization;

public class ProfileCard : BaseCard
{
    private readonly IUserProfileService _userProfileService;
    private long _userId;
    private List<UserProfileViewModel> _userProfiles = [];

    public ProfileCard(IUserProfileService userProfileService)
    {
        _userProfileService = userProfileService;
        Name = "profile";
    }

    public void SetUserId(long userId)
    {
        _userId = userId;
    }

    // [JsonPropertyName("items")]
    // public List<UserProfileViewModel> Items { get; set; } = [];

    public override object Resources => _userProfiles;

    public override async Task LoadDataAsync()
    {
        var userProfile = await _userProfileService.GetUserProfileByUserIdAsync(_userId);
        if (userProfile != null)
        {
            _userProfiles = [userProfile];
            //Items = [userProfile];
        }
    }
} 