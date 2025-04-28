namespace RenterScoreAPIv2.Tab.Cards;

using RenterScoreAPIv2.PropertyDetailsWithImages;

public class PropertyCategoryCard : BaseCard
{
    private readonly IPropertyDetailsWithImagesService _propertyDetailsWithImagesService;
    private readonly List<PagingCard> _pagingCards = [];

    public PropertyCategoryCard(IPropertyDetailsWithImagesService propertyDetailsWithImagesService)
    {
        Name = "category";
        _propertyDetailsWithImagesService = propertyDetailsWithImagesService;
        
        // Initialize paging cards for different property types
        _pagingCards.Add(new PagingCard("hdb_paging", "HDB", _propertyDetailsWithImagesService));
        _pagingCards.Add(new PagingCard("condo_paging", "Condo", _propertyDetailsWithImagesService));
    }

    public override object Resources => _pagingCards;

    public override async Task LoadDataAsync()
    {
        var tasks = _pagingCards.Select(card => card.LoadDataAsync());
        await Task.WhenAll(tasks);
    }
} 