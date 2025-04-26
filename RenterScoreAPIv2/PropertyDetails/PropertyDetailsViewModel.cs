namespace RenterScoreAPIv2.PropertyDetails;

using System.Text.Json.Serialization;
using RenterScoreAPIv2.Property;
using RenterScoreAPIv2.UserProfile;

public class PropertyDetailsViewModel
{
    public required AddressViewModel Address { get; set; }

    public string? Amenities { get; set; }

    [JsonPropertyName("available_date")]
    public string? AvailableDate { get; set; }

    public int? Bathrooms { get; set; }

    public int? Bedrooms { get; set; }

    [JsonPropertyName("created_date")]
    public string? CreatedDate { get; set; }

    [JsonPropertyName("created_user")]
    public long CreatedUser { get; set; }

    public string? Currency { get; set; }

    public string? Description { get; set; }

    [JsonPropertyName("hero_image")]
    public string? HeroImage { get; set; }

    public string[] Images { get; set; } = [];

    [JsonPropertyName("modified_date")]
    public string? ModifiedDate { get; set; }

    [JsonPropertyName("modified_user")]
    public long ModifiedUser { get; set; }

    public int? Price { get; set; }

    [JsonPropertyName("property_id")]
    public long PropertyId { get; set; }

    [JsonPropertyName("property_status")]
    public string? PropertyStatus { get; set; }

    [JsonPropertyName("property_type")]
    public string? PropertyType { get; set; }

    [JsonPropertyName("rent_type")]
    public string? RentType { get; set; }

    public string? Size { get; set; }

    public string? Title { get; set; }

    public required UserProfileViewModel User { get; set; }
}
