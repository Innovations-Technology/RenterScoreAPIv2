namespace RenterScoreAPIv2.Tab;

using RenterScoreAPIv2.Tab.Tabs;
using RenterScoreAPIv2.PropertyDetailsWithImages;
using RenterScoreAPIv2.UserProfile;

public class TabFactory : ITabFactory
{
    private readonly IPropertyDetailsWithImagesService _propertyDetailsWithImagesService;
    private readonly IUserProfileService _userProfileService;

    public TabFactory(IPropertyDetailsWithImagesService propertyDetailsWithImagesService, IUserProfileService userProfileService)
    {
        _propertyDetailsWithImagesService = propertyDetailsWithImagesService;
        _userProfileService = userProfileService;
    }

    public BaseTab CreateTab(string tabId)
    {
        return tabId.ToLower() switch
        {
            "home" => new HomeTab(_propertyDetailsWithImagesService),
            "bookmarks" => new BookmarksTab(),
            "profile" => new ProfileTab(_userProfileService),
            _ => throw new ArgumentException($"Unknown tab ID: {tabId}")
        };
    }
} 