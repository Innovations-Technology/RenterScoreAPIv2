namespace RenterScoreAPIv2.Test.Unit.Tab;

using Moq;
using RenterScoreAPIv2.PropertyDetailsWithImages;
using RenterScoreAPIv2.Tab;
using RenterScoreAPIv2.Tab.Tabs;
using RenterScoreAPIv2.UserProfile;

[TestFixture]
public class TabFactoryTests
{
    private Mock<IPropertyDetailsWithImagesService> _mockPropertyDetailsService;
    private Mock<IUserProfileService> _mockUserProfileService;
    private TabFactory _tabFactory;

    [SetUp]
    public void Setup()
    {
        _mockPropertyDetailsService = new Mock<IPropertyDetailsWithImagesService>();
        _mockUserProfileService = new Mock<IUserProfileService>();
        _tabFactory = new TabFactory(_mockPropertyDetailsService.Object, _mockUserProfileService.Object);
    }

    [Test]
    public void CreateTab_WithHomeId_ReturnsHomeTab()
    {
        var result = _tabFactory.CreateTab("home");

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<HomeTab>());
    }

    [Test]
    public void CreateTab_WithBookmarksId_ReturnsBookmarksTab()
    {
        var result = _tabFactory.CreateTab("bookmarks");

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<BookmarksTab>());
    }

    [Test]
    public void CreateTab_WithProfileId_ReturnsProfileTab()
    {
        var result = _tabFactory.CreateTab("profile");

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<ProfileTab>());
    }

    [Test]
    public void CreateTab_WithInvalidId_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => _tabFactory.CreateTab("invalid"));
    }
} 