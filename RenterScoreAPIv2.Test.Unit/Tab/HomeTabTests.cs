namespace RenterScoreAPIv2.Test.Unit.Tab;

using Moq;
using RenterScoreAPIv2.PropertyDetailsWithImages;
using RenterScoreAPIv2.Tab.Cards;
using RenterScoreAPIv2.Tab.Tabs;

[TestFixture]
public class HomeTabTests
{
    private Mock<IPropertyDetailsWithImagesService> _mockService;
    private HomeTab _homeTab;

    [SetUp]
    public void Setup()
    {
        _mockService = new Mock<IPropertyDetailsWithImagesService>();
        _homeTab = new HomeTab(_mockService.Object);
    }

    [Test]
    public async Task LoadDataAsync_LoadsDataAndAddsCards()
    {
        // Arrange
        _mockService.Setup(s => s.GetPropertyDetailsWithImagesListAsync())
            .ReturnsAsync(new List<PropertyDetailsWithImages>());

        // Act
        await _homeTab.LoadDataAsync();

        // Assert
        Assert.That(_homeTab.Cards, Has.Count.EqualTo(2));
        Assert.That(_homeTab.Cards[0], Is.TypeOf<PropertyBannerCard>());
        Assert.That(_homeTab.Cards[1], Is.TypeOf<PropertyCategoryCard>());
    }
} 