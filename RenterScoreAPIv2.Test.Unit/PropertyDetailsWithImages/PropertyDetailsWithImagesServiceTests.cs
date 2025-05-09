namespace RenterScoreAPIv2.Test.Unit.PropertyDetailsWithImages;

using Microsoft.AspNetCore.Mvc;
using Moq;
using RenterScoreAPIv2.Bookmark;
using RenterScoreAPIv2.Property;
using RenterScoreAPIv2.PropertyDetails;
using RenterScoreAPIv2.PropertyDetailsWithImages;
using RenterScoreAPIv2.PropertyImage;
using RenterScoreAPIv2.PropertyRating;
using RenterScoreAPIv2.User;
using RenterScoreAPIv2.UserProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

[TestFixture]
public class PropertyDetailsWithImagesServiceTests
{
    private Mock<IPropertyDetailsRepository> _mockPropertyDetailsRepository;
    private Mock<IPropertyImageRepository> _mockPropertyImageRepository;
    private Mock<IPropertyRatingService> _mockPropertyRatingService;
    private Mock<IBookmarkRepository> _mockBookmarkRepository;
    private Mock<global::AutoMapper.IMapper> _mockMapper;
    private PropertyDetailsWithImagesService _service;
    
    private Property _mockProperty;
    private User _mockUser;
    private UserProfile _mockUserProfile;
    private RenterScoreAPIv2.PropertyDetails.PropertyDetails _mockPropertyDetails;
    private PropertyRatingViewModel _mockRating;
    private PropertyDetailsWithImages _mockPropertyDetailsWithImages;
    private List<PropertyImage> _mockPropertyImages;

    [SetUp]
    public void Setup()
    {
        _mockPropertyDetailsRepository = new Mock<IPropertyDetailsRepository>();
        _mockPropertyImageRepository = new Mock<IPropertyImageRepository>();
        _mockPropertyRatingService = new Mock<IPropertyRatingService>();
        _mockBookmarkRepository = new Mock<IBookmarkRepository>();
        _mockMapper = new Mock<global::AutoMapper.IMapper>();

        _service = new PropertyDetailsWithImagesService(
            _mockPropertyDetailsRepository.Object,
            _mockPropertyImageRepository.Object,
            _mockPropertyRatingService.Object,
            _mockBookmarkRepository.Object,
            _mockMapper.Object);
            
        SetupMockData();
    }
    
    private void SetupMockData()
    {
        _mockProperty = new Property
        {
            PropertyId = 1,
            Title = "Luxury Apartment in Pasir Ris",
            CreatedDate = DateTime.Now,
            PropertyState = "ACTIVE",
            PropertyStatus = "AVAILABLE",
            PropertyType = "APARTMENT",
            Region = "East",
            RentType = "WHOLE_UNIT",
            UserId = 1,
            ModifiedDate = DateTime.Now,
            ModifiedUser = 1,
            CreatedUser = 1
        };
        
        _mockUser = new User
        {
            UserId = 1,
            Email = "test@example.com",
            AccountStatus = "Active",
            CreatedDate = DateTime.Now,
            CreatedUser = 1,
            EmailStatus = "Verified",
            ExpiryDate = DateTimeOffset.Now.AddYears(1),
            ModifiedDate = DateTime.Now,
            ModifiedUser = 1,
            Password = "password",
            PropertyRole = "Owner",
            UserRole = "User",
            VerificationToken = "token"
        };
        
        _mockUserProfile = new UserProfile
        {
            UserId = 1,
            ProfileId = 1,
            FirstName = "John",
            LastName = "Doe",
            Gender = "Male",
            CreatedDate = DateTime.Now,
            CreatedUser = 1,
            ModifiedDate = DateTime.Now,
            ModifiedUser = 1,
            PropertyRole = "Owner"
        };
        
        _mockPropertyDetails = new RenterScoreAPIv2.PropertyDetails.PropertyDetails
        {
            Property = _mockProperty,
            User = _mockUser,
            UserProfile = _mockUserProfile
        };
        
        _mockRating = new PropertyRatingViewModel
        {
            Cleanliness = (int)4.5m,
            Traffic = (int)4.0m,
            Amenities = (int)4.2m,
            Safety = (int)4.8m,
            ValueForMoney = (int)3.9m,
            Total = 4.3m
        };
        
        _mockPropertyImages = new List<PropertyImage>
        {
            new PropertyImage
            {
                ImageId = 1,
                PropertyId = 1,
                ImageUrl = "http://example.com/image1.jpg"
            },
            new PropertyImage
            {
                ImageId = 2,
                PropertyId = 1,
                ImageUrl = "http://example.com/image2.jpg"
            }
        };
        
        _mockPropertyDetailsWithImages = new PropertyDetailsWithImages
        {
            Property = _mockProperty,
            User = _mockUser,
            UserProfile = _mockUserProfile,
            PropertyImages = _mockPropertyImages,
            PropertyRating = _mockRating,
            IsBookmarked = false
        };
    }

    [Test]
    public async Task GetPropertyDetailsWithImagesListAsync_ReturnsExpectedList()
    {
        // Arrange
        var propertyDetailsList = new List<RenterScoreAPIv2.PropertyDetails.PropertyDetails> { _mockPropertyDetails };
        _mockPropertyDetailsRepository.Setup(r => r.GetPropertiesWithUserProfilesAsync())
            .ReturnsAsync(propertyDetailsList);
            
        _mockPropertyImageRepository.Setup(r => r.GetPropertyImagesByIdsAsync(It.IsAny<IEnumerable<long>>()))
            .ReturnsAsync(_mockPropertyImages);
            
        _mockPropertyRatingService.Setup(s => s.GetAveragePropertyRatingAsync(It.IsAny<long>()))
            .ReturnsAsync(_mockRating);
            
        _mockMapper.Setup(m => m.Map<PropertyDetailsWithImages>(It.IsAny<RenterScoreAPIv2.PropertyDetails.PropertyDetails>()))
            .Returns(_mockPropertyDetailsWithImages);

        // Act
        var result = await _service.GetPropertyDetailsWithImagesListAsync();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(1));
        
        var propertyDetails = result.First();
        Assert.That(propertyDetails.Property.PropertyId, Is.EqualTo(_mockProperty.PropertyId));
        Assert.That(propertyDetails.Property.Title, Is.EqualTo(_mockProperty.Title));
        Assert.That(propertyDetails.PropertyImages, Is.EqualTo(_mockPropertyImages));
        Assert.That(propertyDetails.PropertyRating, Is.EqualTo(_mockRating));
        Assert.That(propertyDetails.IsBookmarked, Is.False);
    }
    
    [Test]
    public async Task GetPropertyDetailsWithImagesListAsync_WithUserId_ChecksBookmarkStatus()
    {
        // Arrange
        long userId = 1;
        var propertyDetailsList = new List<RenterScoreAPIv2.PropertyDetails.PropertyDetails> { _mockPropertyDetails };
        
        _mockPropertyDetailsRepository.Setup(r => r.GetPropertiesWithUserProfilesAsync())
            .ReturnsAsync(propertyDetailsList);
            
        _mockPropertyImageRepository.Setup(r => r.GetPropertyImagesByIdsAsync(It.IsAny<IEnumerable<long>>()))
            .ReturnsAsync(_mockPropertyImages);
            
        _mockPropertyRatingService.Setup(s => s.GetAveragePropertyRatingAsync(It.IsAny<long>()))
            .ReturnsAsync(_mockRating);
            
        _mockBookmarkRepository.Setup(b => b.IsPropertyBookmarkedByUserAsync(_mockProperty.PropertyId, userId))
            .ReturnsAsync(true);
            
        _mockMapper.Setup(m => m.Map<PropertyDetailsWithImages>(It.IsAny<RenterScoreAPIv2.PropertyDetails.PropertyDetails>()))
            .Returns(_mockPropertyDetailsWithImages);

        // Act
        var result = await _service.GetPropertyDetailsWithImagesListAsync(userId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(1));
        
        _mockBookmarkRepository.Verify(b => b.IsPropertyBookmarkedByUserAsync(_mockProperty.PropertyId, userId), Times.Once);
    }
    
    [Test]
    public async Task GetPropertyDetailsWithImagesAsync_ReturnsExpectedProperty()
    {
        // Arrange
        long propertyId = 1;
        
        _mockPropertyDetailsRepository.Setup(r => r.GetPropertiesWithUserProfilesByIdAsync(propertyId))
            .ReturnsAsync(_mockPropertyDetails);
            
        _mockPropertyImageRepository.Setup(r => r.GetPropertyImagesByIdAsync(propertyId))
            .ReturnsAsync(_mockPropertyImages);
            
        _mockPropertyRatingService.Setup(s => s.GetAveragePropertyRatingAsync(propertyId))
            .ReturnsAsync(_mockRating);
            
        _mockMapper.Setup(m => m.Map<PropertyDetailsWithImages>(It.IsAny<RenterScoreAPIv2.PropertyDetails.PropertyDetails>()))
            .Returns(_mockPropertyDetailsWithImages);

        // Act
        var result = await _service.GetPropertyDetailsWithImagesAsync(propertyId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Property.PropertyId, Is.EqualTo(_mockProperty.PropertyId));
        Assert.That(result.Property.Title, Is.EqualTo(_mockProperty.Title));
        Assert.That(result.PropertyImages, Is.EqualTo(_mockPropertyImages));
        Assert.That(result.PropertyRating, Is.EqualTo(_mockRating));
    }
    
    [Test]
    public async Task GetPropertyDetailsWithImagesAsync_WithNonExistentId_ReturnsNull()
    {
        // Arrange
        long propertyId = 999;
        
        _mockPropertyDetailsRepository.Setup(r => r.GetPropertiesWithUserProfilesByIdAsync(propertyId))
            .ReturnsAsync((RenterScoreAPIv2.PropertyDetails.PropertyDetails)null);

        // Act
        var result = await _service.GetPropertyDetailsWithImagesAsync(propertyId);

        // Assert
        Assert.That(result, Is.Null);
    }
    
    [Test]
    public async Task GetPropertyDetailsWithImagesAsync_WithUserId_ChecksBookmarkStatus()
    {
        // Arrange
        long propertyId = 1;
        long userId = 1;
        
        _mockPropertyDetailsRepository.Setup(r => r.GetPropertiesWithUserProfilesByIdAsync(propertyId))
            .ReturnsAsync(_mockPropertyDetails);
            
        _mockPropertyImageRepository.Setup(r => r.GetPropertyImagesByIdAsync(propertyId))
            .ReturnsAsync(_mockPropertyImages);
            
        _mockPropertyRatingService.Setup(s => s.GetAveragePropertyRatingAsync(propertyId))
            .ReturnsAsync(_mockRating);
            
        _mockBookmarkRepository.Setup(b => b.IsPropertyBookmarkedByUserAsync(propertyId, userId))
            .ReturnsAsync(true);
            
        _mockMapper.Setup(m => m.Map<PropertyDetailsWithImages>(It.IsAny<RenterScoreAPIv2.PropertyDetails.PropertyDetails>()))
            .Returns(_mockPropertyDetailsWithImages);

        // Act
        var result = await _service.GetPropertyDetailsWithImagesAsync(propertyId, userId);

        // Assert
        Assert.That(result, Is.Not.Null);
        _mockBookmarkRepository.Verify(b => b.IsPropertyBookmarkedByUserAsync(propertyId, userId), Times.Once);
    }
    
    [Test]
    public async Task SearchPropertyDetailsWithImagesByTitleAsync_ReturnsMatchingProperties()
    {
        // Arrange
        string searchTerm = "Pasir Ris";
        var propertyDetailsList = new List<RenterScoreAPIv2.PropertyDetails.PropertyDetails> { _mockPropertyDetails };
        
        _mockPropertyDetailsRepository.Setup(r => r.SearchPropertiesByTitleAsync(searchTerm))
            .ReturnsAsync(propertyDetailsList);
            
        _mockPropertyImageRepository.Setup(r => r.GetPropertyImagesByIdsAsync(It.IsAny<IEnumerable<long>>()))
            .ReturnsAsync(_mockPropertyImages);
            
        _mockPropertyRatingService.Setup(s => s.GetAveragePropertyRatingAsync(It.IsAny<long>()))
            .ReturnsAsync(_mockRating);
            
        _mockMapper.Setup(m => m.Map<PropertyDetailsWithImages>(It.IsAny<RenterScoreAPIv2.PropertyDetails.PropertyDetails>()))
            .Returns(_mockPropertyDetailsWithImages);

        // Act
        var result = await _service.SearchPropertyDetailsWithImagesByTitleAsync(searchTerm, null);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(1));
        
        var propertyDetails = result.First();
        Assert.That(propertyDetails.Property.Title, Does.Contain(searchTerm));
    }
    
    [Test]
    public async Task SearchPropertyDetailsWithImagesByTitleAsync_WithNoMatches_ReturnsEmptyList()
    {
        // Arrange
        string searchTerm = "nonexistentpropertytitle12345";
        
        _mockPropertyDetailsRepository.Setup(r => r.SearchPropertiesByTitleAsync(searchTerm))
            .ReturnsAsync(new List<RenterScoreAPIv2.PropertyDetails.PropertyDetails>());

        // Act
        var result = await _service.SearchPropertyDetailsWithImagesByTitleAsync(searchTerm, null);

        // Assert
        Assert.That(result, Is.Empty);
    }
    
    [Test]
    public async Task SearchPropertyDetailsWithImagesByTitleAsync_WithUserId_ChecksBookmarkStatus()
    {
        // Arrange
        string searchTerm = "Pasir Ris";
        long userId = 1;
        var propertyDetailsList = new List<RenterScoreAPIv2.PropertyDetails.PropertyDetails> { _mockPropertyDetails };
        
        _mockPropertyDetailsRepository.Setup(r => r.SearchPropertiesByTitleAsync(searchTerm))
            .ReturnsAsync(propertyDetailsList);
            
        _mockPropertyImageRepository.Setup(r => r.GetPropertyImagesByIdsAsync(It.IsAny<IEnumerable<long>>()))
            .ReturnsAsync(_mockPropertyImages);
            
        _mockPropertyRatingService.Setup(s => s.GetAveragePropertyRatingAsync(It.IsAny<long>()))
            .ReturnsAsync(_mockRating);
            
        _mockBookmarkRepository.Setup(b => b.IsPropertyBookmarkedByUserAsync(_mockProperty.PropertyId, userId))
            .ReturnsAsync(true);
            
        _mockMapper.Setup(m => m.Map<PropertyDetailsWithImages>(It.IsAny<RenterScoreAPIv2.PropertyDetails.PropertyDetails>()))
            .Returns(_mockPropertyDetailsWithImages);

        // Act
        var result = await _service.SearchPropertyDetailsWithImagesByTitleAsync(searchTerm, userId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(1));
        
        _mockBookmarkRepository.Verify(b => b.IsPropertyBookmarkedByUserAsync(_mockProperty.PropertyId, userId), Times.Once);
    }
} 