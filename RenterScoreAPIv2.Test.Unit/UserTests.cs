
using System.ComponentModel.DataAnnotations;
using RenterScoreAPIv2.User;

public class UserTests
{
    [Test]
    public void User_Should_Have_Valid_Required_Properties()
    {
        // Arrange
        var user = new User
        {
            UserId = 1,
            CreatedDate = DateTime.Now,
            CreatedUser = 1,
            ModifiedDate = DateTime.Now,
            ModifiedUser = 1,
            AccountStatus = "Active",
            Email = "test@example.com",
            EmailStatus = "Verified",
            ExpiryDate = DateTimeOffset.Now.AddYears(1),
            Password = "SecurePassword123",
            PropertyRole = "Admin",
            UserRole = "User",
            VerificationToken = "Token123"
        };

        // Act
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(user);
        var isValid = Validator.TryValidateObject(user, validationContext, validationResults, true);

        // Assert
        Assert.True(isValid, "User model should be valid with all required properties set.");
    }

    [Test]
    public void User_Should_Enforce_MaxLength_Constraints()
    {
        // Arrange
        var user = new User
        {
            AccountStatus = new string('A', 256), // Exceeds max length
            Email = new string('E', 256),        // Exceeds max length
            EmailStatus = new string('S', 256),  // Exceeds max length
            Password = new string('P', 256),     // Exceeds max length
            PropertyRole = new string('R', 256), // Exceeds max length
            UserRole = new string('U', 256),     // Exceeds max length
            VerificationToken = new string('T', 256) // Exceeds max length
        };

        // Act
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(user);
        var isValid = Validator.TryValidateObject(user, validationContext, validationResults, true);

        // Assert
        Assert.False(isValid, "User model should be invalid when max length constraints are violated.");
    }

    [Test]
    public void User_Should_Allow_Default_Values_For_Optional_Properties()
    {
        // Arrange
        var user = new User
        {
            AccountStatus = "Active",
            Email = "test@example.com",
            EmailStatus = "Verified",
            Password = "SecurePassword123",
            PropertyRole = "Admin",
            UserRole = "User",
            VerificationToken = "Token123"
        };

        // Act
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(user);
        var isValid = Validator.TryValidateObject(user, validationContext, validationResults, true);

        // Assert
        Assert.True(isValid, "User model should be valid even if optional properties are not set.");
    }
}