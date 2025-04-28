namespace RenterScoreAPIv2.Test.Unit.Tab;

using Moq;
using RenterScoreAPIv2.PropertyDetailsWithImages;
using RenterScoreAPIv2.Tab;
using RenterScoreAPIv2.Tab.Tabs;

[TestFixture]
public class TabFactoryTests
{
    private Mock<IPropertyDetailsWithImagesService> _mockService;
    private TabFactory _tabFactory;

    [SetUp]
    public void Setup()
    {
        _mockService = new Mock<IPropertyDetailsWithImagesService>();
        _tabFactory = new TabFactory(_mockService.Object);
    }

    [Test]
    public void CreateTab_WithHomeId_ReturnsHomeTab()
    {
        // Act
        var result = _tabFactory.CreateTab("home");

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<HomeTab>());
    }

    [Test]
    public void CreateTab_WithBookmarksId_ReturnsBookmarksTab()
    {
        // Act
        var result = _tabFactory.CreateTab("bookmarks");

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<BookmarksTab>());
    }

    [Test]
    public void CreateTab_WithProfileId_ReturnsProfileTab()
    {
        // Act
        var result = _tabFactory.CreateTab("profile");

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<ProfileTab>());
    }

    [Test]
    public void CreateTab_WithInvalidId_ThrowsArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _tabFactory.CreateTab("invalid"));
    }
} 