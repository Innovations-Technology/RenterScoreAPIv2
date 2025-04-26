namespace RenterScoreAPIv2.Property;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("property", Schema = "dbo")]
public class Property
{
    [Key]
    [Column("property_id")]
    public long PropertyId { get; set; }

    [Required]
    [Column("created_date")]
    public DateTime CreatedDate { get; set; }

    [Required]
    [Column("created_user")]
    public long CreatedUser { get; set; }

    [Required]
    [Column("modified_date")]
    public DateTime ModifiedDate { get; set; }

    [Required]
    [Column("modified_user")]
    public long ModifiedUser { get; set; }

    [Column("block_no")]
    public string? BlockNo { get; set; }

    [Column("postal_code")]
    public string? PostalCode { get; set; }

    [Required]
    [Column("region")]
    public string Region { get; set; } = string.Empty;

    [Column("street")]
    public string? Street { get; set; }

    [Column("unit_no")]
    public string? UnitNo { get; set; }

    [Column("amenities")]
    public string? Amenities { get; set; }

    [Column("available_date")]
    public string? AvailableDate { get; set; }

    [Column("bathrooms")]
    public int? Bathrooms { get; set; }

    [Column("bedrooms")]
    public int? Bedrooms { get; set; }

    [Column("currency")]
    public string? Currency { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Column("hero_image")]
    public string? HeroImage { get; set; }

    [Column("price")]
    public int? Price { get; set; }

    [Required]
    [Column("property_status")]
    public string PropertyStatus { get; set; } = string.Empty;

    [Required]
    [Column("property_type")]
    public string PropertyType { get; set; } = string.Empty;

    [Required]
    [Column("rent_type")]
    public string RentType { get; set; } = string.Empty;

    [Column("size")]
    public string? Size { get; set; }

    [Column("title")]
    public string? Title { get; set; }

    [Required]
    [Column("user_id")]
    public long UserId { get; set; }
}

