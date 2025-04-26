namespace RenterScoreAPIv2.Property;

using System.Text.Json.Serialization;

public class AddressViewModel
{
    [JsonPropertyName("block_no")]
    public string? BlockNo { get; set; }

    [JsonPropertyName("postal_code")]
    public string? PostalCode { get; set; }

    public string? Region { get; set; }

    public string? Street { get; set; }

    [JsonPropertyName("unit_no")]
    public string? UnitNo { get; set; }
}