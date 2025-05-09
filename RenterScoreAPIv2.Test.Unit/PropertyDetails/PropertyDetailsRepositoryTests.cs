namespace RenterScoreAPIv2.Test.Unit.PropertyDetails;

using Microsoft.EntityFrameworkCore;
using Moq;
using RenterScoreAPIv2.EntityFramework;
using RenterScoreAPIv2.Property;
using RenterScoreAPIv2.PropertyDetails;
using RenterScoreAPIv2.User;
using RenterScoreAPIv2.UserProfile;
using System.Collections.Generic;
using System.Linq;

[TestFixture]
public class PropertyDetailsRepositoryTests
{
    private DbContextOptions<AppDbContext> _dbContextOptions;
    private AppDbContext _dbContext;
    private PropertyDetailsRepository _repository;

    [SetUp]
    public void Setup()
    {
        // Setup in-memory database for testing
        _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestPropertyDetails")
            .Options;
            
        _dbContext = new AppDbContext(_dbContextOptions);
        _repository = new PropertyDetailsRepository(_dbContext);
        
        // Clear database before each test
        _dbContext.Database.EnsureDeleted();
        _dbContext.Database.EnsureCreated();
        
        // Seed test data
        SeedTestData();
    }
    
    private void SeedTestData()
    {
        var users = new List<User>
        {
            new User { 
                UserId = 1, 
                Email = "user1@example.com",
                AccountStatus = "Active",
                CreatedDate = DateTime.Now,
                CreatedUser = 1,
                EmailStatus = "Verified",
                ExpiryDate = DateTimeOffset.Now.AddYears(1),
                ModifiedDate = DateTime.Now,
                ModifiedUser = 1,
                Password = "password1",
                PropertyRole = "Owner",
                UserRole = "User",
                VerificationToken = "token1"
            },
            new User { 
                UserId = 2, 
                Email = "user2@example.com",
                AccountStatus = "Active",
                CreatedDate = DateTime.Now,
                CreatedUser = 1,
                EmailStatus = "Verified",
                ExpiryDate = DateTimeOffset.Now.AddYears(1),
                ModifiedDate = DateTime.Now,
                ModifiedUser = 1,
                Password = "password2",
                PropertyRole = "Owner",
                UserRole = "User",
                VerificationToken = "token2"
            }
        };
        
        var userProfiles = new List<UserProfile>
        {
            new UserProfile { 
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
            },
            new UserProfile { 
                UserId = 2, 
                ProfileId = 2,
                FirstName = "Jane", 
                LastName = "Smith",
                Gender = "Female",
                CreatedDate = DateTime.Now,
                CreatedUser = 1,
                ModifiedDate = DateTime.Now,
                ModifiedUser = 1,
                PropertyRole = "Owner"
            }
        };
        
        var properties = new List<Property>
        {
            new Property 
            { 
                PropertyId = 1, 
                Title = "Luxury Apartment in Pasir Ris", 
                PropertyState = "ACTIVE",
                PropertyStatus = "AVAILABLE",
                PropertyType = "APARTMENT",
                Region = "East",
                RentType = "WHOLE_UNIT",
                UserId = 1,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                ModifiedUser = 1,
                CreatedUser = 1
            },
            new Property 
            { 
                PropertyId = 2, 
                Title = "Cozy Studio in Tampines", 
                PropertyState = "ACTIVE",
                PropertyStatus = "AVAILABLE",
                PropertyType = "STUDIO",
                Region = "East",
                RentType = "WHOLE_UNIT",
                UserId = 1,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                ModifiedUser = 1,
                CreatedUser = 1
            },
            new Property 
            { 
                PropertyId = 3, 
                Title = "Spacious House in Bedok", 
                PropertyState = "ACTIVE",
                PropertyStatus = "AVAILABLE",
                PropertyType = "HOUSE",
                Region = "East",
                RentType = "WHOLE_UNIT",
                UserId = 2,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                ModifiedUser = 2,
                CreatedUser = 2
            },
            new Property 
            { 
                PropertyId = 4, 
                Title = "Suspended Condo Unit", 
                PropertyState = "SUSPENDED",
                PropertyStatus = "AVAILABLE",
                PropertyType = "CONDO",
                Region = "Central",
                RentType = "WHOLE_UNIT",
                UserId = 2,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                ModifiedUser = 2,
                CreatedUser = 2
            }
        };
        
        _dbContext.Users.AddRange(users);
        _dbContext.UserProfiles.AddRange(userProfiles);
        _dbContext.Properties.AddRange(properties);
        _dbContext.SaveChanges();
    }
    
    [TearDown]
    public void TearDown()
    {
        _dbContext.Dispose();
    }
    
    [Test]
    public async Task GetPropertiesWithUserProfilesAsync_ReturnsActiveProperties()
    {
        // Act
        var result = await _repository.GetPropertiesWithUserProfilesAsync();
        
        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(3)); // Only active properties
        
        foreach (var property in result)
        {
            Assert.That(property.Property.PropertyState, Is.Not.EqualTo("SUSPENDED"));
        }
    }
    
    [Test]
    public async Task GetPropertiesWithUserProfilesByIdAsync_WithValidId_ReturnsProperty()
    {
        // Arrange
        long propertyId = 1;
        
        // Act
        var result = await _repository.GetPropertiesWithUserProfilesByIdAsync(propertyId);
        
        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Property.PropertyId, Is.EqualTo(propertyId));
        Assert.That(result.Property.Title, Is.EqualTo("Luxury Apartment in Pasir Ris"));
        Assert.That(result.User, Is.Not.Null);
        Assert.That(result.UserProfile, Is.Not.Null);
    }
    
    [Test]
    public async Task GetPropertiesWithUserProfilesByIdAsync_WithInvalidId_ReturnsNull()
    {
        // Arrange
        long propertyId = 999;
        
        // Act
        var result = await _repository.GetPropertiesWithUserProfilesByIdAsync(propertyId);
        
        // Assert
        Assert.That(result, Is.Null);
    }
    
    [Test]
    public async Task SearchPropertiesByTitleAsync_ExactMatch_ReturnsMatchingProperty()
    {
        // Arrange
        string searchTerm = "Luxury Apartment in Pasir Ris";
        
        // Act
        var result = await _repository.SearchPropertiesByTitleAsync(searchTerm);
        
        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(1));
        Assert.That(result.First().Property.Title, Is.EqualTo(searchTerm));
    }
    
    [Test]
    public async Task SearchPropertiesByTitleAsync_PartialMatch_ReturnsMatchingProperties()
    {
        // Arrange
        string searchTerm = "Pasir Ris";
        
        // Act
        var result = await _repository.SearchPropertiesByTitleAsync(searchTerm);
        
        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(1));
        Assert.That(result.First().Property.Title, Does.Contain(searchTerm));
    }
    
    [Test]
    public async Task SearchPropertiesByTitleAsync_CaseInsensitive_ReturnsMatchingProperties()
    {
        // Arrange
        string searchTerm = "pasir ris"; // lowercase
        
        // Act
        var result = await _repository.SearchPropertiesByTitleAsync(searchTerm);
        
        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(1));
        Assert.That(result.First().Property.Title.ToLower(), Does.Contain(searchTerm.ToLower()));
    }
    
    [Test]
    public async Task SearchPropertiesByTitleAsync_MultipleMatches_ReturnsAllMatchingProperties()
    {
        // Arrange
        string searchTerm = "in"; // Matches multiple properties
        
        // Act
        var result = await _repository.SearchPropertiesByTitleAsync(searchTerm);
        
        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(3)); // All active properties with "in" in title
        
        foreach (var property in result)
        {
            Assert.That(property.Property.Title.ToLower(), Does.Contain(searchTerm.ToLower()));
            Assert.That(property.Property.PropertyState, Is.Not.EqualTo("SUSPENDED"));
        }
    }
    
    [Test]
    public async Task SearchPropertiesByTitleAsync_NoMatches_ReturnsEmptyList()
    {
        // Arrange
        string searchTerm = "nonexistentpropertytitle12345";
        
        // Act
        var result = await _repository.SearchPropertiesByTitleAsync(searchTerm);
        
        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Empty);
    }
    
    [Test]
    public async Task SearchPropertiesByTitleAsync_ExcludesSuspendedProperties()
    {
        // Arrange
        string searchTerm = "Condo"; // Matches a suspended property
        
        // Act
        var result = await _repository.SearchPropertiesByTitleAsync(searchTerm);
        
        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Empty); // Should not return suspended property
    }
    
    [Test]
    public async Task SearchPropertiesByTitleAsync_IncludesUserAndUserProfile()
    {
        // Arrange
        string searchTerm = "Pasir Ris";
        
        // Act
        var result = await _repository.SearchPropertiesByTitleAsync(searchTerm);
        
        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(1));
        
        var propertyDetails = result.First();
        Assert.That(propertyDetails.User, Is.Not.Null);
        Assert.That(propertyDetails.UserProfile, Is.Not.Null);
        Assert.That(propertyDetails.User.UserId, Is.EqualTo(propertyDetails.Property.UserId));
        Assert.That(propertyDetails.UserProfile.UserId, Is.EqualTo(propertyDetails.Property.UserId));
    }
} 