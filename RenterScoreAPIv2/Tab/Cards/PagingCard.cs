namespace RenterScoreAPIv2.Tab.Cards;

using RenterScoreAPIv2.PropertyDetailsWithImages;

public class PagingCard : BaseCard
{
    private readonly string _propertyType;
    private readonly IPropertyDetailsWithImagesService _propertyDetailsWithImagesService;
    private List<PropertyDetailsWithImages> _properties = [];

    public PagingCard(string name, string propertyType, IPropertyDetailsWithImagesService propertyDetailsWithImagesService)
    {
        Name = name;
        _propertyType = propertyType;
        _propertyDetailsWithImagesService = propertyDetailsWithImagesService;
    }

    public override object Resources => _properties;

    public override async Task LoadDataAsync()
    {
        var allProperties = await _propertyDetailsWithImagesService.GetPropertyDetailsWithImagesListAsync();
        _properties = allProperties
            .Where(p => p.Property.PropertyType.Equals(_propertyType, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
} 