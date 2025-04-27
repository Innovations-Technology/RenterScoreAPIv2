namespace RenterScoreAPIv2.UserProfile;

using System.ComponentModel.DataAnnotations;

public class UserProfile
{
    [Key]
    public required long ProfileId { get; set; }
    public string? Address { get; set; }
    public string? Biography { get; set; }
    public string? Company { get; set; }
    public string? ContactNumber { get; set; }
    public required DateTime CreatedDate { get; set; }
    public required long CreatedUser { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public required string Gender { get; set; }
    public string? LastName { get; set; }
    public required DateTime ModifiedDate { get; set; }
    public required long ModifiedUser { get; set; }
    public string? ProfileImage { get; set; }
    public required string PropertyRole { get; set; }
    public required long UserId { get; set; }
}
