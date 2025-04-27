namespace RenterScoreAPIv2.User;

using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public required long UserId { get; set; }
    public required string AccountStatus { get; set; }
    public required DateTime CreatedDate { get; set; }
    public required long CreatedUser { get; set; }
    public required string Email { get; set; }
    public required string EmailStatus { get; set; }
    public required DateTimeOffset ExpiryDate { get; set; }
    public required DateTime ModifiedDate { get; set; }
    public required long ModifiedUser { get; set; }
    public required string Password { get; set; }
    public required string PropertyRole { get; set; }
    public required string UserRole { get; set; }
    public required string VerificationToken { get; set; }
}