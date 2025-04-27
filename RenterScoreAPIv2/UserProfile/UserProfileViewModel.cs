namespace RenterScoreAPIv2.UserProfile;

public class UserProfileViewModel
{
    public long UserId { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? ProfileImage { get; set; }
    public string? ContactNumber { get; set; }
    public string? Company { get; set; }
    public string? PropertyRole { get; set; }
}