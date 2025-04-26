namespace RenterScoreAPIv2.Property;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class PropertyViewModel
{
    public required AddressViewModel Address { get; set; }

    public string? Amenities { get; set; }

    [JsonPropertyName("available_date")]
    public string? AvailableDate { get; set; }

    public int? Bathrooms { get; set; }

    public int? Bedrooms { get; set; }

    [JsonPropertyName("created_date")]
    public DateTime CreatedDate { get; set; }

    [JsonPropertyName("created_user")]
    public long CreatedUser { get; set; }

    public string? Currency { get; set; }

    public string? Description { get; set; }

    public string? HeroImage { get; set; }

    public int? Price { get; set; }

    [Required]
    public string PropertyStatus { get; set; } = string.Empty;

    [Required]
    public string PropertyType { get; set; } = string.Empty;

    [Required]
    public string RentType { get; set; } = string.Empty;

    public string? Size { get; set; }

    public string? Title { get; set; }

    [Required]
    public long UserId { get; set; }
}
