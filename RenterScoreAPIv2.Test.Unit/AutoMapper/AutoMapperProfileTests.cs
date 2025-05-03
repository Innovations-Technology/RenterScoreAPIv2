namespace RenterScoreAPIv2.Test.Unit.AutoMapper;

using global::AutoMapper;
using RenterScoreAPIv2.AutoMapper;
using RenterScoreAPIv2.PropertyRating;
using RenterScoreAPIv2.PropertyDetailsWithImages;

[TestFixture]
public class AutoMapperProfileTests
{
    private IMapper _mapper;
    
    [SetUp]
    public void Setup()
    {
        var configuration = new MapperConfiguration(cfg => 
        {
            cfg.AddProfile<AutoMapperProfile>();
        });
        _mapper = configuration.CreateMapper();
    }

    [Test]
    public void TestConstructor()
    {
        var profile = new AutoMapperProfile();
        Assert.That(profile, Is.Not.Null);
    }
    
    [Test]
    public void Rating_To_PropertyRatingViewModel_MapsCorrectly()
    {
        // Arrange
        var rating = new Rating
        {
            PropertyId = 1,
            UserId = 2,
            Cleanliness = 4,
            Traffic = 3,
            Amenities = 5,
            Safety = 4,
            ValueForMoney = 3,
            Total = 3.8m
        };
        
        // Act
        var viewModel = _mapper.Map<PropertyRatingViewModel>(rating);
        
        // Assert
        Assert.That(viewModel, Is.Not.Null);
        Assert.That(viewModel.PropertyId, Is.EqualTo(1));
        Assert.That(viewModel.Cleanliness, Is.EqualTo(4));
        Assert.That(viewModel.Traffic, Is.EqualTo(3));
        Assert.That(viewModel.Amenities, Is.EqualTo(5));
        Assert.That(viewModel.Safety, Is.EqualTo(4));
        Assert.That(viewModel.ValueForMoney, Is.EqualTo(3));
        Assert.That(viewModel.Total, Is.EqualTo(3.8m));
    }
    
    [Test]
    public void PropertyDetailsWithImages_To_ViewModel_MapsRatingCorrectly()
    {
        // Arrange
        var ratingViewModel = new PropertyRatingViewModel
        {
            PropertyId = 1,
            Cleanliness = 4,
            Traffic = 3,
            Amenities = 5,
            Safety = 4,
            ValueForMoney = 3,
            Total = 3.8m
        };
        
        var property = new RenterScoreAPIv2.Property.Property
        {
            PropertyId = 1,
            PropertyType = "Apartment",
            Region = "Region",
            RentType = "Monthly",
            PropertyStatus = "Active",
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now,
            CreatedUser = 1,
            ModifiedUser = 1,
            UserId = 1
        };
        
        var propertyDetailsWithImages = new RenterScoreAPIv2.PropertyDetailsWithImages.PropertyDetailsWithImages
        {
            Property = property,
            User = new RenterScoreAPIv2.User.User
            {
                UserId = 1,
                Email = "test@example.com",
                Password = "password",
                UserRole = "User",
                PropertyRole = "Owner",
                AccountStatus = "Active",
                EmailStatus = "Verified",
                VerificationToken = "token",
                ExpiryDate = DateTimeOffset.Now,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedUser = 1,
                ModifiedUser = 1
            },
            UserProfile = new RenterScoreAPIv2.UserProfile.UserProfile
            {
                ProfileId = 1,
                UserId = 1,
                Gender = "Male",
                PropertyRole = "Owner",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedUser = 1,
                ModifiedUser = 1
            },
            PropertyImages = new List<RenterScoreAPIv2.PropertyImage.PropertyImage>(),
            PropertyRating = ratingViewModel
        };
        
        // Act
        var viewModel = _mapper.Map<PropertyDetailsWithImagesViewModel>(propertyDetailsWithImages);
        
        // Assert
        Assert.That(viewModel.Rating, Is.Not.Null);
        Assert.That(viewModel.Rating.PropertyId, Is.EqualTo(1));
        Assert.That(viewModel.Rating.Cleanliness, Is.EqualTo(4));
        Assert.That(viewModel.Rating.Traffic, Is.EqualTo(3));
        Assert.That(viewModel.Rating.Amenities, Is.EqualTo(5));
        Assert.That(viewModel.Rating.Safety, Is.EqualTo(4));
        Assert.That(viewModel.Rating.ValueForMoney, Is.EqualTo(3));
        Assert.That(viewModel.Rating.Total, Is.EqualTo(3.8m));
    }
}