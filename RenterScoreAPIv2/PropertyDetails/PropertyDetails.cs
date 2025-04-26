namespace RenterScoreAPIv2.PropertyDetails;

using RenterScoreAPIv2.Property;
using RenterScoreAPIv2.UserProfile;
using RenterScoreAPIv2.User;

public class PropertyDetails
{
    public required Property Property { get; set; }
    public required UserProfile UserProfile { get; set; }
    public required User User { get; set; }
}