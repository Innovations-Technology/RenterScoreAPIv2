namespace RenterScoreAPIv2.Test.Unit.Tab;

using Moq;
using RenterScoreAPIv2.PropertyDetailsWithImages;
using RenterScoreAPIv2.PropertyRating;
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
        var testPropertyRating = new PropertyRatingViewModel
        {
            PropertyId = 1,
            Cleanliness = 4,
            Traffic = 3,
            Amenities = 5,
            Safety = 4,
            ValueForMoney = 3,
            Total = 3.8m
        };
        
        var testProperty = new RenterScoreAPIv2.Property.Property
        {
            PropertyId = 1,
            Title = "Test Property",
            PropertyType = "Apartment",
            UserId = 1,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now,
            CreatedUser = 1,
            ModifiedUser = 1,
            PropertyStatus = "Active",
            Region = "Test Region",
            RentType = "Monthly"
        };
        
        var testUser = new RenterScoreAPIv2.User.User
        {
            UserId = 1,
            Email = "test@example.com",
            Password = "password",
            UserRole = "User",
            PropertyRole = "Owner",
            AccountStatus = "Active",
            EmailStatus = "Verified",
            VerificationToken = "token",
            ExpiryDate = DateTimeOffset.Now.AddDays(7),
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now,
            CreatedUser = 1,
            ModifiedUser = 1
        };
        
        var testUserProfile = new RenterScoreAPIv2.UserProfile.UserProfile
        {
            ProfileId = 1,
            UserId = 1,
            Gender = "Male",
            PropertyRole = "Owner",
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now,
            CreatedUser = 1,
            ModifiedUser = 1
        };
        
        var testProperty2 = new PropertyDetailsWithImages
        {
            Property = testProperty,
            User = testUser,
            UserProfile = testUserProfile,
            PropertyImages = new List<RenterScoreAPIv2.PropertyImage.PropertyImage>(),
            PropertyRating = testPropertyRating
        };
        
        _mockService.Setup(s => s.GetPropertyDetailsWithImagesListAsync())
            .ReturnsAsync(new List<PropertyDetailsWithImages> { testProperty2 });

        // Act
        await _homeTab.LoadDataAsync();

        // Assert
        Assert.That(_homeTab.Cards, Has.Count.EqualTo(2));
        Assert.That(_homeTab.Cards[0], Is.TypeOf<PropertyBannerCard>());
        Assert.That(_homeTab.Cards[1], Is.TypeOf<PropertyCategoryCard>());
    }
}