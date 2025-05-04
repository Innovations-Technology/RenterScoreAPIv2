namespace RenterScoreAPIv2.PropertyRating;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Rating
{
    [Column(Order = 0)]
    public required long UserId { get; set; }
    
    [Column(Order = 1)]
    public required long PropertyId { get; set; }
    
    public required int Cleanliness { get; set; }
    public required int Traffic { get; set; }
    public required int Amenities { get; set; }
    public required int Safety { get; set; }
    public required int ValueForMoney { get; set; }
    
    [Column(TypeName = "decimal(3,2)")]
    public decimal Total { get; set; }
} 