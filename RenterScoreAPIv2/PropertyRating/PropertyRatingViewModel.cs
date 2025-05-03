namespace RenterScoreAPIv2.PropertyRating;

public class PropertyRatingViewModel
{
    public long PropertyId { get; set; }
    public int Cleanliness { get; set; }
    public int Traffic { get; set; }
    public int Amenities { get; set; }
    public int Safety { get; set; }
    public int ValueForMoney { get; set; }
    public decimal Total { get; set; }
} 