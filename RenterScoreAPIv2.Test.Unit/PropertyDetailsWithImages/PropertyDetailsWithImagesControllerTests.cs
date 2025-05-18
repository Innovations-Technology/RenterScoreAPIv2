namespace RenterScoreAPIv2.Test.Unit.PropertyDetailsWithImages;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RenterScoreAPIv2.Property;
using RenterScoreAPIv2.PropertyDetailsWithImages;
using RenterScoreAPIv2.PropertyRating;
using RenterScoreAPIv2.User;
using RenterScoreAPIv2.UserProfile;
using RenterScoreAPIv2.PropertyImage;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using NUnit.Framework;

[TestFixture]
public class PropertyDetailsWithImagesControllerTests
{
    private Mock<IPropertyDetailsWithImagesService> _mockService;
    private Mock<global::AutoMapper.IMapper> _mockMapper;
    private PropertyDetailsWithImagesController _controller;
    private List<PropertyDetailsWithImages> _mockPropertyList;
    private PropertyDetailsWithImages _mockProperty;
    private List<PropertyDetailsWithImagesViewModel> _mockViewModelList;
    private PropertyDetailsWithImagesViewModel _mockViewModel;

    [SetUp]
    public void Setup()
    {
        _mockService = new Mock<IPropertyDetailsWithImagesService>();
        _mockMapper = new Mock<global::AutoMapper.IMapper>();
        _controller = new PropertyDetailsWithImagesController(_mockService.Object, _mockMapper.Object);
        
        // Initialize HttpContext with Request
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };
        
        SetupMockData();
    }
    
    private void SetupMockData()
    {
        // Setup mock property data
        _mockProperty = new PropertyDetailsWithImages
        {
            Property = new Property 
            {
                PropertyId = 1, 
                Title = "Luxury Apartment in Pasir Ris",
                CreatedDate = DateTime.Now,
                CreatedUser = 1,
                ModifiedDate = DateTime.Now,
                ModifiedUser = 1,
                PropertyState = "Available",
                PropertyStatus = "Active",
                PropertyType = "Apartment",
                Region = "East",
                RentType = "Room",
                UserId = 1
            },
            PropertyRating = new PropertyRatingViewModel 
            {
                Total = 4.5m 
            },
            IsBookmarked = false,
            User = new User
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
            },
            UserProfile = new UserProfile
            {
                UserId = 1,
                ProfileId = 1,
                Gender = "Male",
                CreatedDate = DateTime.Now,
                CreatedUser = 1,
                ModifiedDate = DateTime.Now,
                ModifiedUser = 1,
                PropertyRole = "Owner"
            },
            PropertyImages = new List<PropertyImage>
            {
                new PropertyImage
                {
                    ImageId = 1,
                    PropertyId = 1,
                    ImageUrl = "image1.jpg"
                }
            }
        };
        
        _mockPropertyList = new List<PropertyDetailsWithImages> { _mockProperty };
        
        // Setup mock view model data
        _mockViewModel = new PropertyDetailsWithImagesViewModel
        {
            PropertyId = 1,
            Title = "Luxury Apartment in Pasir Ris",
            Rating = new PropertyRatingViewModel { Total = 4.5m },
            IsBookmarked = false,
            Images = Array.Empty<string>(),
            Address = new AddressViewModel(),
            User = new UserProfileViewModel
            {
                UserId = 1,
                Email = "test@example.com",
                FirstName = "Test",
                LastName = "User",
                PropertyRole = "Owner"
            }
        };
        
        _mockViewModelList = new List<PropertyDetailsWithImagesViewModel> { _mockViewModel };
    }
    
    [Test]
    public async Task GetProperties_ReturnsOkResult()
    {
        // Arrange
        _mockService
            .Setup(s => s.GetPropertyDetailsWithImagesListAsync(It.IsAny<long?>()))
            .ReturnsAsync(_mockPropertyList);
            
        _mockMapper
            .Setup(m => m.Map<IEnumerable<PropertyDetailsWithImagesViewModel>>(It.IsAny<IEnumerable<PropertyDetailsWithImages>>()))
            .Returns(_mockViewModelList);
        long? userId = null;

        // Act
        var result = await _controller.GetProperties(userId);

        // Assert
        Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        var returnedList = okResult.Value as IEnumerable<PropertyDetailsWithImagesViewModel>;
        Assert.That(returnedList, Is.Not.Null);
        Assert.That(returnedList, Is.EqualTo(_mockViewModelList));
    }
    
    [Test]
    public async Task GetProperties_WithUserId_CallsServiceWithUserId()
    {
        // Arrange
        long userId = 1;
        
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers["x-userid"] = userId.ToString();
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = httpContext
        };
        
        _mockService
            .Setup(s => s.GetPropertyDetailsWithImagesListAsync(userId))
            .ReturnsAsync(_mockPropertyList)
            .Verifiable();
            
        _mockMapper
            .Setup(m => m.Map<IEnumerable<PropertyDetailsWithImagesViewModel>>(It.IsAny<IEnumerable<PropertyDetailsWithImages>>()))
            .Returns(_mockViewModelList);

        // Act
        var result = await _controller.GetProperties();

        // Assert
        _mockService.Verify(s => s.GetPropertyDetailsWithImagesListAsync(userId), Times.Once);
    }
    
    [Test]
    public async Task GetPropertiesById_WithValidId_ReturnsOkResult()
    {
        // Arrange
        long propertyId = 1;
        
        _mockService
            .Setup(s => s.GetPropertyDetailsWithImagesAsync(propertyId, It.IsAny<long?>()))
            .ReturnsAsync(_mockProperty);
            
        _mockMapper
            .Setup(m => m.Map<PropertyDetailsWithImagesViewModel>(It.IsAny<PropertyDetailsWithImages>()))
            .Returns(_mockViewModel);

        // Act
        var result = await _controller.GetProperties(propertyId);

        // Assert
        Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        var returnedViewModel = okResult.Value as PropertyDetailsWithImagesViewModel;
        Assert.That(returnedViewModel, Is.Not.Null);
        Assert.That(returnedViewModel, Is.EqualTo(_mockViewModel));
    }
    
    [Test]
    public async Task GetPropertiesById_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        long propertyId = 999;
        
        _mockService
            .Setup(s => s.GetPropertyDetailsWithImagesAsync(propertyId, It.IsAny<long?>()))
            .ReturnsAsync((PropertyDetailsWithImages)null);

        // Act
        var result = await _controller.GetProperties(propertyId);

        // Assert
        Assert.That(result.Result, Is.TypeOf<NotFoundResult>());
    }
    
    [Test]
    public async Task GetPropertiesById_WithUserId_CallsServiceWithUserId()
    {
        // Arrange
        long propertyId = 1;
        long userId = 1;
        
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers["x-userid"] = userId.ToString();
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = httpContext
        };
        
        _mockService
            .Setup(s => s.GetPropertyDetailsWithImagesAsync(propertyId, userId))
            .ReturnsAsync(_mockProperty)
            .Verifiable();
            
        _mockMapper
            .Setup(m => m.Map<PropertyDetailsWithImagesViewModel>(It.IsAny<PropertyDetailsWithImages>()))
            .Returns(_mockViewModel);

        // Act
        var result = await _controller.GetProperties(propertyId);

        // Assert
        _mockService.Verify(s => s.GetPropertyDetailsWithImagesAsync(propertyId, userId), Times.Once);
    }
    
    [Test]
    public async Task SearchProperties_WithValidTitle_ReturnsOkResult()
    {
        // Arrange
        string searchTitle = "Pasir Ris";
        long? userId = null;

        _mockService
            .Setup(s => s.SearchPropertyDetailsWithImagesByTitleAsync(searchTitle, It.IsAny<long?>()))
            .ReturnsAsync(_mockPropertyList);
            
        _mockMapper
            .Setup(m => m.Map<IEnumerable<PropertyDetailsWithImagesViewModel>>(It.IsAny<IEnumerable<PropertyDetailsWithImages>>()))
            .Returns(_mockViewModelList);
        

        // Act
        var result = await _controller.SearchProperties(searchTitle, userId);

        // Assert
        Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        var returnedList = okResult.Value as IEnumerable<PropertyDetailsWithImagesViewModel>;
        Assert.That(returnedList, Is.Not.Null);
        Assert.That(returnedList, Is.EqualTo(_mockViewModelList));
    }
    
    [Test]
    public async Task SearchProperties_WithEmptyTitle_ReturnsBadRequest()
    {
        // Arrange
        string searchTitle = "";

        // Act
        var result = await _controller.SearchProperties(searchTitle);

        // Assert
        Assert.That(result.Result, Is.TypeOf<BadRequestObjectResult>());
    }
    
    [Test]
    public async Task SearchProperties_WithNullTitle_ReturnsBadRequest()
    {
        // Arrange
        string searchTitle = null;

        // Act
        var result = await _controller.SearchProperties(searchTitle);

        // Assert
        Assert.That(result.Result, Is.TypeOf<BadRequestObjectResult>());
    }
    
    [Test]
    public async Task SearchProperties_WithUserId_CallsServiceWithUserId()
    {
        // Arrange
        string searchTitle = "Pasir Ris";
        long userId = 1;
        
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers["x-userid"] = userId.ToString();
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = httpContext
        };
        
        _mockService
            .Setup(s => s.SearchPropertyDetailsWithImagesByTitleAsync(searchTitle, userId))
            .ReturnsAsync(_mockPropertyList)
            .Verifiable();
            
        _mockMapper
            .Setup(m => m.Map<IEnumerable<PropertyDetailsWithImagesViewModel>>(It.IsAny<IEnumerable<PropertyDetailsWithImages>>()))
            .Returns(_mockViewModelList);

        // Act
        var result = await _controller.SearchProperties(searchTitle, userId);

        // Assert
        _mockService.Verify(s => s.SearchPropertyDetailsWithImagesByTitleAsync(searchTitle, userId), Times.Once);
    }
    
    [Test]
    public async Task SearchProperties_WithNoResults_ReturnsEmptyList()
    {
        // Arrange
        string searchTitle = "nonexistentpropertytitle";
        var emptyList = new List<PropertyDetailsWithImages>();
        var emptyViewModelList = new List<PropertyDetailsWithImagesViewModel>();
        long? userId = null;
        
        _mockService
            .Setup(s => s.SearchPropertyDetailsWithImagesByTitleAsync(searchTitle, It.IsAny<long?>()))
            .ReturnsAsync(emptyList);
            
        _mockMapper
            .Setup(m => m.Map<IEnumerable<PropertyDetailsWithImagesViewModel>>(It.IsAny<IEnumerable<PropertyDetailsWithImages>>()))
            .Returns(emptyViewModelList);

        // Act
        var result = await _controller.SearchProperties(searchTitle, userId);

        // Assert
        Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        var returnedList = okResult.Value as IEnumerable<PropertyDetailsWithImagesViewModel>;
        Assert.That(returnedList, Is.Not.Null);
        Assert.That(returnedList, Is.Empty);
    }
} 