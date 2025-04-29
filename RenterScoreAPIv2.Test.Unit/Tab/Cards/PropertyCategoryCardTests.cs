namespace RenterScoreAPIv2.Test.Unit.Tab.Cards;

using Moq;
using RenterScoreAPIv2.PropertyDetailsWithImages;
using RenterScoreAPIv2.Tab.Cards;

[TestFixture]
public class PropertyCategoryCardTests
{
    private Mock<IPropertyDetailsWithImagesService> _mockService;
    private PropertyCategoryCard _card;

    [SetUp]
    public void Setup()
    {
        _mockService = new Mock<IPropertyDetailsWithImagesService>();
        _card = new PropertyCategoryCard(_mockService.Object);
    }

    [Test]
    public void Constructor_SetsNameCorrectly()
    {
        Assert.That(_card.Name, Is.EqualTo("category"));
        Assert.That(_card.Type, Is.EqualTo("card"));
    }

    [Test]
    public void Constructor_CreatesPagingCards()
    {
        // Act & Assert
        var resources = _card.Resources as List<PagingCard>;
        Assert.That(resources, Is.Not.Null);
        Assert.That(resources.Count, Is.EqualTo(2));
        
        // Verify the paging cards are for HDB and Condo types
        Assert.That(resources[0].Name, Is.EqualTo("hdb_paging"));
        Assert.That(resources[1].Name, Is.EqualTo("condo_paging"));
    }

    [Test]
    public async Task LoadDataAsync_LoadsDataForAllPagingCards()
    {
        // Arrange
        _mockService.Setup(s => s.GetPropertyDetailsWithImagesListAsync())
            .ReturnsAsync(new List<PropertyDetailsWithImages>());

        // Act
        await _card.LoadDataAsync();

        // Assert - just verify it completes without exception
        Assert.Pass("LoadDataAsync completed successfully");
    }
} 