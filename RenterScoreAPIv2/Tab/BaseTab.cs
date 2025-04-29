namespace RenterScoreAPIv2.Tab;

using System.Text.Json;
using System.Text.Json.Serialization;

public abstract class BaseTab
{
    [JsonPropertyName("cards")]
    public List<BaseCard> Cards { get; set; } = [];

    public abstract Task LoadDataAsync();
} 