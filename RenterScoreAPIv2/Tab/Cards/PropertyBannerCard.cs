namespace RenterScoreAPIv2.Tab.Cards;

using RenterScoreAPIv2.PropertyDetailsWithImages;

public class PropertyBannerCard : BaseCard
{
    private readonly IPropertyDetailsWithImagesService _propertyDetailsWithImagesService;
    private List<PropertyDetailsWithImages> _properties = [];

    public PropertyBannerCard(IPropertyDetailsWithImagesService propertyDetailsWithImagesService)
    {
        Name = "banner";
        _propertyDetailsWithImagesService = propertyDetailsWithImagesService;
    }

    public override object Resources => _properties.Take(4).ToList();

    public override async Task LoadDataAsync()
    {
        var properties = await _propertyDetailsWithImagesService.GetPropertyDetailsWithImagesListAsync();
        _properties = properties.ToList();
    }
} 