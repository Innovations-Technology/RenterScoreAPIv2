namespace RenterScoreAPIv2.PropertyDetailsWithImages;

using RenterScoreAPIv2.Property;
using RenterScoreAPIv2.UserProfile;
using RenterScoreAPIv2.User;
using RenterScoreAPIv2.PropertyImage;

public class PropertyDetailsWithImages
{
    public required Property Property { get; set; }
    public required UserProfile UserProfile { get; set; }
    public required User User { get; set; }
    public required IEnumerable<PropertyImage> PropertyImages { get; set; }
}