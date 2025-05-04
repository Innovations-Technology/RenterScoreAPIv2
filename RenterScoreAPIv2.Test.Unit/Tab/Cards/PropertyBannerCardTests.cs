namespace RenterScoreAPIv2.Test.Unit.Tab.Cards;

using Moq;
using RenterScoreAPIv2.Property;
using RenterScoreAPIv2.PropertyDetailsWithImages;
using RenterScoreAPIv2.PropertyImage;
using RenterScoreAPIv2.Tab.Cards;
using RenterScoreAPIv2.User;
using RenterScoreAPIv2.UserProfile;

[TestFixture]
public class PropertyBannerCardTests
{
    private Mock<IPropertyDetailsWithImagesService> _mockService;
    private PropertyBannerCard _card;
    private List<PropertyDetailsWithImages> _testProperties;

    [SetUp]
    public void Setup()
    {
        _mockService = new Mock<IPropertyDetailsWithImagesService>();
        _card = new PropertyBannerCard(_mockService.Object);
        
        // Create test properties
        _testProperties = new List<PropertyDetailsWithImages>();
        for (int i = 1; i <= 5; i++)
        {
            var now = DateTime.Now;
            _testProperties.Add(new PropertyDetailsWithImages
            {
                Property = new Property { 
                    PropertyId = i, 
                    PropertyType = "HDB",
                    PropertyState = "AVAILABLE",
                    PropertyStatus = "Active", 
                    Region = "North", 
                    RentType = "Room",
                    CreatedDate = now,
                    CreatedUser = 1,
                    UserId = i,
                    ModifiedDate = now,
                    ModifiedUser = 1
                },
                User = new User { 
                    UserId = i,
                    AccountStatus = "Active",
                    CreatedDate = now,
                    CreatedUser = 1,
                    Email = $"user{i}@example.com",
                    EmailStatus = "Verified",
                    ExpiryDate = now.AddYears(1),
                    ModifiedDate = now,
                    ModifiedUser = 1,
                    Password = "password123",
                    PropertyRole = "Agent",
                    UserRole = "User",
                    VerificationToken = Guid.NewGuid().ToString()
                },
                UserProfile = new UserProfile { 
                    ProfileId = i, 
                    UserId = i, 
                    Gender = "Male", 
                    PropertyRole = "Agent", 
                    CreatedDate = now, 
                    ModifiedDate = now, 
                    CreatedUser = 1, 
                    ModifiedUser = 1 
                },
                PropertyImages = new List<PropertyImage>()
            });
        }
    }

    [Test]
    public void Constructor_SetsNameCorrectly()
    {
        Assert.That(_card.Name, Is.EqualTo("banner"));
        Assert.That(_card.Type, Is.EqualTo("card"));
    }

    [Test]
    public async Task LoadDataAsync_LoadsPropertiesFromService()
    {
        // Arrange
        _mockService.Setup(s => s.GetPropertyDetailsWithImagesListAsync())
            .ReturnsAsync(_testProperties);

        // Act
        await _card.LoadDataAsync();

        // Assert
        var resources = _card.Resources as IEnumerable<PropertyDetailsWithImages>;
        Assert.That(resources, Is.Not.Null);
        
        // Should return at most 4 properties
        Assert.That(resources.Count(), Is.EqualTo(4));
        
        // Verify the properties are the first 4 from our test data
        Assert.That(resources.First().Property.PropertyId, Is.EqualTo(1));
    }

    [Test]
    public async Task LoadDataAsync_WithFewerThanFourProperties_ReturnsAllProperties()
    {
        // Arrange
        var fewProperties = _testProperties.Take(2).ToList();
        _mockService.Setup(s => s.GetPropertyDetailsWithImagesListAsync())
            .ReturnsAsync(fewProperties);

        // Act
        await _card.LoadDataAsync();

        // Assert
        var resources = _card.Resources as IEnumerable<PropertyDetailsWithImages>;
        Assert.That(resources, Is.Not.Null);
        Assert.That(resources.Count(), Is.EqualTo(2));
    }
} 