namespace RenterScoreAPIv2.Property;

using System.ComponentModel.DataAnnotations;

public class Property
{
    [Key]
    public required long PropertyId { get; set; }
    public string? Amenities { get; set; }
    public string? AvailableDate { get; set; }
    public int? Bathrooms { get; set; }
    public int? Bedrooms { get; set; }
    public string? BlockNo { get; set; }
    public required DateTime CreatedDate { get; set; }
    public required long CreatedUser { get; set; }
    public string? Currency { get; set; }
    public string? Description { get; set; }
    public string? HeroImage { get; set; }
    public required string PropertyState { get; set; }
    public required string PropertyStatus { get; set; }
    public required string PropertyType { get; set; }
    public int? Price { get; set; }
    public string? PostalCode { get; set; }
    public required string Region { get; set; }
    public required string RentType { get; set; }
    public string? Size { get; set; }
    public string? Street { get; set; }
    public string? Title { get; set; }
    public string? UnitNo { get; set; }
    public required long UserId { get; set; }
    public required DateTime ModifiedDate { get; set; }
    public required long ModifiedUser { get; set; }
}

