namespace RenterScoreAPIv2.Test.Unit.Tab.Cards;

using Moq;
using RenterScoreAPIv2.Property;
using RenterScoreAPIv2.PropertyDetailsWithImages;
using RenterScoreAPIv2.PropertyImage;
using RenterScoreAPIv2.PropertyRating;
using RenterScoreAPIv2.Tab.Cards;
using RenterScoreAPIv2.User;
using RenterScoreAPIv2.UserProfile;

[TestFixture]
public class PagingCardTests
{
    private Mock<IPropertyDetailsWithImagesService> _mockService;
    private PagingCard _hdbPagingCard;
    private PagingCard _condoPagingCard;
    private List<PropertyDetailsWithImages> _testProperties;

    [SetUp]
    public void Setup()
    {
        _mockService = new Mock<IPropertyDetailsWithImagesService>();
        _hdbPagingCard = new PagingCard("hdb_paging", "HDB", _mockService.Object);
        _condoPagingCard = new PagingCard("condo_paging", "Condo", _mockService.Object);
        
        // Create test properties with different property types
        var now = DateTime.Now;
        
        // Create a default rating for test properties
        var defaultRating = new PropertyRatingViewModel
        {
            //PropertyId = 1,
            Cleanliness = 4,
            Traffic = 3,
            Amenities = 5,
            Safety = 4,
            ValueForMoney = 3,
            Total = 3.8m
        };
        
        var defaultRating2 = new PropertyRatingViewModel
        {
            //PropertyId = 2,
            Cleanliness = 3,
            Traffic = 4,
            Amenities = 4,
            Safety = 5,
            ValueForMoney = 3,
            Total = 3.8m
        };
        
        var defaultRating3 = new PropertyRatingViewModel
        {
            //PropertyId = 3,
            Cleanliness = 5,
            Traffic = 2,
            Amenities = 4,
            Safety = 3,
            ValueForMoney = 5,
            Total = 3.8m
        };
        
        _testProperties = new List<PropertyDetailsWithImages>
        {
            new PropertyDetailsWithImages
            {
                Property = new Property { 
                    PropertyId = 1, 
                    PropertyType = "HDB",
                    PropertyState = "AVAILABLE",
                    PropertyStatus = "Active", 
                    Region = "North", 
                    RentType = "Room",
                    CreatedDate = now,
                    CreatedUser = 1,
                    UserId = 1,
                    ModifiedDate = now,
                    ModifiedUser = 1
                },
                User = new User { 
                    UserId = 1,
                    AccountStatus = "Active",
                    CreatedDate = now,
                    CreatedUser = 1,
                    Email = "user1@example.com",
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
                    ProfileId = 1, 
                    UserId = 1, 
                    Gender = "Male", 
                    PropertyRole = "Agent", 
                    CreatedDate = now, 
                    ModifiedDate = now, 
                    CreatedUser = 1, 
                    ModifiedUser = 1 
                },
                PropertyImages = new List<PropertyImage>(),
                PropertyRating = defaultRating
            },
            new PropertyDetailsWithImages
            {
                Property = new Property { 
                    PropertyId = 2, 
                    PropertyType = "HDB",
                    PropertyState = "AVAILABLE",
                    PropertyStatus = "Active", 
                    Region = "East", 
                    RentType = "Room",
                    CreatedDate = now,
                    CreatedUser = 1,
                    UserId = 1,
                    ModifiedDate = now,
                    ModifiedUser = 1
                },
                User = new User { 
                    UserId = 1,
                    AccountStatus = "Active",
                    CreatedDate = now,
                    CreatedUser = 1,
                    Email = "user1@example.com",
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
                    ProfileId = 1, 
                    UserId = 1, 
                    Gender = "Male", 
                    PropertyRole = "Agent", 
                    CreatedDate = now, 
                    ModifiedDate = now, 
                    CreatedUser = 1, 
                    ModifiedUser = 1 
                },
                PropertyImages = new List<PropertyImage>(),
                PropertyRating = defaultRating2
            },
            new PropertyDetailsWithImages
            {
                Property = new Property { 
                    PropertyId = 3, 
                    PropertyType = "Condo", 
                    PropertyStatus = "Active",
                    PropertyState = "AVAILABLE",
                    Region = "Central", 
                    RentType = "Whole",
                    CreatedDate = now,
                    CreatedUser = 1,
                    UserId = 2,
                    ModifiedDate = now,
                    ModifiedUser = 1
                },
                User = new User { 
                    UserId = 2,
                    AccountStatus = "Active",
                    CreatedDate = now,
                    CreatedUser = 1,
                    Email = "user2@example.com",
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
                    ProfileId = 2, 
                    UserId = 2, 
                    Gender = "Female", 
                    PropertyRole = "Agent", 
                    CreatedDate = now, 
                    ModifiedDate = now, 
                    CreatedUser = 1, 
                    ModifiedUser = 1 
                },
                PropertyImages = new List<PropertyImage>(),
                PropertyRating = defaultRating3
            }
        };
    }

    [Test]
    public void Constructor_SetsNameCorrectly()
    {
        Assert.That(_hdbPagingCard.Name, Is.EqualTo("hdb_paging"));
        Assert.That(_condoPagingCard.Name, Is.EqualTo("condo_paging"));
        Assert.That(_hdbPagingCard.Type, Is.EqualTo("card"));
    }

    [Test]
    public async Task LoadDataAsync_FiltersByPropertyType_HDB()
    {
        // Arrange
        _mockService.Setup(s => s.GetPropertyDetailsWithImagesListAsync())
            .ReturnsAsync(_testProperties);

        // Act
        await _hdbPagingCard.LoadDataAsync();

        // Assert
        var resources = _hdbPagingCard.Resources as IEnumerable<PropertyDetailsWithImages>;
        Assert.That(resources, Is.Not.Null);
        Assert.That(resources.Count(), Is.EqualTo(2));
        Assert.That(resources.All(p => p.Property.PropertyType == "HDB"), Is.True);
    }

    [Test]
    public async Task LoadDataAsync_FiltersByPropertyType_Condo()
    {
        // Arrange
        _mockService.Setup(s => s.GetPropertyDetailsWithImagesListAsync())
            .ReturnsAsync(_testProperties);

        // Act
        await _condoPagingCard.LoadDataAsync();

        // Assert
        var resources = _condoPagingCard.Resources as IEnumerable<PropertyDetailsWithImages>;
        Assert.That(resources, Is.Not.Null);
        Assert.That(resources.Count(), Is.EqualTo(1));
        Assert.That(resources.All(p => p.Property.PropertyType == "Condo"), Is.True);
    }

    [Test]
    public async Task LoadDataAsync_WhenNoMatchingProperties_ReturnsEmptyList()
    {
        // Arrange
        var pagingCard = new PagingCard("landed_paging", "Landed", _mockService.Object);
        _mockService.Setup(s => s.GetPropertyDetailsWithImagesListAsync())
            .ReturnsAsync(_testProperties);

        // Act
        await pagingCard.LoadDataAsync();

        // Assert
        var resources = pagingCard.Resources as IEnumerable<PropertyDetailsWithImages>;
        Assert.That(resources, Is.Not.Null);
        Assert.That(resources.Count(), Is.EqualTo(0));
    }
} 