namespace RenterScoreAPIv2.PropertyImage;

using System.ComponentModel.DataAnnotations;

public class PropertyImage
{
    [Key]
    public required long ImageId { get; set; }
    public string? ImageUrl { get; set; }
    public required long PropertyId { get; set; }
}