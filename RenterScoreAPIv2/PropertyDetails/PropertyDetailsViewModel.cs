namespace RenterScoreAPIv2.PropertyDetails;

using RenterScoreAPIv2.Property;
using RenterScoreAPIv2.UserProfile;

public class PropertyDetailsViewModel
{
    public required AddressViewModel Address { get; set; }
    public required UserProfileViewModel User { get; set; }

    public string? Amenities { get; set; }
    public string? AvailableDate { get; set; }
    public int? Bathrooms { get; set; }
    public int? Bedrooms { get; set; }
    public string? CreatedDate { get; set; }
    public long CreatedUser { get; set; }
    public string? Currency { get; set; }
    public string? Description { get; set; }
    public string? HeroImage { get; set; }
    public string[] Images { get; set; } = [];
    public string? ModifiedDate { get; set; }
    public long ModifiedUser { get; set; }
    public int? Price { get; set; }
    public long PropertyId { get; set; }
    public string? PropertyStatus { get; set; }
    public string? PropertyType { get; set; }
    public string? RentType { get; set; }
    public string? Size { get; set; }
    public string? Title { get; set; }
}
