namespace RenterScoreAPIv2.Test.Unit.Bookmark;

using Microsoft.EntityFrameworkCore;
using RenterScoreAPIv2.Bookmark;
using RenterScoreAPIv2.EntityFramework;
using System.Collections.Generic;

[TestFixture]
public class BookmarkRepositoryTests
{
    private DbContextOptions<AppDbContext> _dbContextOptions;
    private AppDbContext _dbContext;
    private BookmarkRepository _repository;

    [SetUp]
    public void Setup()
    {
        // Setup in-memory database for testing
        _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestBookmarks")
            .Options;
            
        _dbContext = new AppDbContext(_dbContextOptions);
        _repository = new BookmarkRepository(_dbContext);
        
        // Clear database before each test
        _dbContext.Database.EnsureDeleted();
        _dbContext.Database.EnsureCreated();
        
        // Seed test data
        SeedTestData();
    }
    
    private void SeedTestData()
    {
        var bookmarks = new List<RenterScoreAPIv2.Bookmark.Bookmark>
        {
            new RenterScoreAPIv2.Bookmark.Bookmark
            {
                BookmarkId = 1,
                CreatedDate = DateTime.Now,
                PropertyId = 1,
                UserId = 1
            },
            new RenterScoreAPIv2.Bookmark.Bookmark
            {
                BookmarkId = 2,
                CreatedDate = DateTime.Now,
                PropertyId = 2,
                UserId = 1
            },
            new RenterScoreAPIv2.Bookmark.Bookmark
            {
                BookmarkId = 3,
                CreatedDate = DateTime.Now,
                PropertyId = 3,
                UserId = 2
            }
        };
        
        _dbContext.Bookmarks.AddRange(bookmarks);
        _dbContext.SaveChanges();
    }
    
    [TearDown]
    public void TearDown()
    {
        _dbContext.Dispose();
    }
    
    [Test]
    public async Task IsPropertyBookmarkedByUserAsync_WhenBookmarkExists_ReturnsTrue()
    {
        // Arrange
        long propertyId = 1;
        long userId = 1;
        
        // Act
        var result = await _repository.IsPropertyBookmarkedByUserAsync(propertyId, userId);
        
        // Assert
        Assert.That(result, Is.True);
    }
    
    [Test]
    public async Task IsPropertyBookmarkedByUserAsync_WhenBookmarkDoesNotExist_ReturnsFalse()
    {
        // Arrange
        long propertyId = 1;
        long userId = 2; // This user has not bookmarked property 1
        
        // Act
        var result = await _repository.IsPropertyBookmarkedByUserAsync(propertyId, userId);
        
        // Assert
        Assert.That(result, Is.False);
    }
    
    [Test]
    public async Task IsPropertyBookmarkedByUserAsync_WhenPropertyDoesNotExist_ReturnsFalse()
    {
        // Arrange
        long propertyId = 999; // Non-existent property
        long userId = 1;
        
        // Act
        var result = await _repository.IsPropertyBookmarkedByUserAsync(propertyId, userId);
        
        // Assert
        Assert.That(result, Is.False);
    }
    
    [Test]
    public async Task IsPropertyBookmarkedByUserAsync_WhenUserDoesNotExist_ReturnsFalse()
    {
        // Arrange
        long propertyId = 1;
        long userId = 999; // Non-existent user
        
        // Act
        var result = await _repository.IsPropertyBookmarkedByUserAsync(propertyId, userId);
        
        // Assert
        Assert.That(result, Is.False);
    }
    
    [Test]
    public async Task IsPropertyBookmarkedByUserAsync_MultipleBookmarksByUser_WorksCorrectly()
    {
        // Arrange
        long userId = 1; // User 1 has bookmarked properties 1 and 2
        
        // Act & Assert
        // Should return true for property 1
        var result1 = await _repository.IsPropertyBookmarkedByUserAsync(1, userId);
        Assert.That(result1, Is.True);
        
        // Should return true for property 2
        var result2 = await _repository.IsPropertyBookmarkedByUserAsync(2, userId);
        Assert.That(result2, Is.True);
        
        // Should return false for property 3 (bookmarked by user 2, not user 1)
        var result3 = await _repository.IsPropertyBookmarkedByUserAsync(3, userId);
        Assert.That(result3, Is.False);
    }
} 