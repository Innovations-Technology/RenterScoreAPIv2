namespace RenterScoreAPIv2.UserProfile;

using System.Text.Json.Serialization;

public class UserProfileViewModel
{
    [JsonPropertyName("user_id")]
    public long UserId { get; set; }

    public string? Email { get; set; }

    [JsonPropertyName("first_name")]
    public string? FirstName { get; set; }

    [JsonPropertyName("last_name")]
    public string? LastName { get; set; }

    [JsonPropertyName("profile_image")]
    public string? ProfileImage { get; set; }

    [JsonPropertyName("contact_number")]
    public string? ContactNumber { get; set; }

    public string? Company { get; set; }

    [JsonPropertyName("property_role")]
    public string? PropertyRole { get; set; }
}