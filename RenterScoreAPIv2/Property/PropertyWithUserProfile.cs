namespace RenterScoreAPIv2.Property;

using RenterScoreAPIv2.UserProfile;

public class PropertyWithUserProfile
{
    public required Property Property { get; set; }
    public UserProfile? UserProfile { get; set; }
}