namespace RenterScoreAPIv2.Tab;

using RenterScoreAPIv2.Tab.Tabs;
using RenterScoreAPIv2.PropertyDetailsWithImages;

public class TabFactory : ITabFactory
{
    private readonly IPropertyDetailsWithImagesService _propertyDetailsWithImagesService;

    public TabFactory(IPropertyDetailsWithImagesService propertyDetailsWithImagesService)
    {
        _propertyDetailsWithImagesService = propertyDetailsWithImagesService;
    }

    public BaseTab CreateTab(string tabId)
    {
        return tabId.ToLower() switch
        {
            "home" => new HomeTab(_propertyDetailsWithImagesService),
            "bookmarks" => new BookmarksTab(),
            "profile" => new ProfileTab(),
            _ => throw new ArgumentException($"Unknown tab ID: {tabId}")
        };
    }
} 