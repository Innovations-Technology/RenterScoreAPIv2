namespace RenterScoreAPIv2.Tab.Tabs;

using RenterScoreAPIv2.Tab.Cards;
using RenterScoreAPIv2.UserProfile;

public class ProfileTab : BaseTab
{
    private readonly ProfileCard _profileCard;
    private long _userId;

    public ProfileTab(IUserProfileService userProfileService)
    {
        _profileCard = new ProfileCard(userProfileService);
    }

    public void SetUserId(long userId)
    {
        _userId = userId;
        _profileCard.SetUserId(userId);
    }

    public override async Task LoadDataAsync()
    {
        await _profileCard.LoadDataAsync();
        Cards.Add(_profileCard);
    }
} 