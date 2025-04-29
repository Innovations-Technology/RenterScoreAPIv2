namespace RenterScoreAPIv2.Tab.Tabs;

using RenterScoreAPIv2.Tab.Cards;
using RenterScoreAPIv2.PropertyDetailsWithImages;

public class HomeTab : BaseTab
{
    private readonly PropertyBannerCard _bannerCard;
    private readonly PropertyCategoryCard _categoryCard;

    public HomeTab(IPropertyDetailsWithImagesService propertyDetailsWithImagesService)
    {
        _bannerCard = new PropertyBannerCard(propertyDetailsWithImagesService);
        _categoryCard = new PropertyCategoryCard(propertyDetailsWithImagesService);
    }

    public override async Task LoadDataAsync()
    {
        await _bannerCard.LoadDataAsync();
        await _categoryCard.LoadDataAsync();
        
        Cards.Add(_bannerCard);
        Cards.Add(_categoryCard);
    }
} 