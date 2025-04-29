namespace RenterScoreAPIv2.Tab;

using System.Text.Json.Serialization;

public abstract class BaseCard
{
    [JsonPropertyName("type")]
    public string Type { get; } = "card";

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("resources")]
    public abstract object Resources { get; }

    public abstract Task LoadDataAsync();
} 