namespace RenterScoreAPIv2.Tab.Cards;

using RenterScoreAPIv2.UserProfile;
using System.Text.Json.Serialization;

public class ProfileCard : BaseCard
{
    private readonly IUserProfileService _userProfileService;
    private long _userId;
    private UserProfileViewModel? _userProfile;

    public ProfileCard(IUserProfileService userProfileService)
    {
        _userProfileService = userProfileService;
        Name = "profile";
    }

    public void SetUserId(long userId)
    {
        _userId = userId;
    }

    [JsonPropertyName("items")]
    public List<UserProfileViewModel> Items { get; set; } = [];

    public override object Resources => new { };

    public override async Task LoadDataAsync()
    {
        _userProfile = await _userProfileService.GetUserProfileByUserIdAsync(_userId);
        if (_userProfile != null)
        {
            Items = [_userProfile];
        }
    }
} 