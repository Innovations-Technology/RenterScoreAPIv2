namespace RenterScoreAPIv2.UserProfile;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("user_profile", Schema = "dbo")]
public class UserProfile
{
    [Key]
    [Column("profile_id")]
    public long ProfileId { get; set; }

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

    [Column("address")]
    public string? Address { get; set; }

    [Column("biography")]
    public string? Biography { get; set; }

    [Column("company")]
    public string? Company { get; set; }

    [Column("contact_number")]
    public string? ContactNumber { get; set; }

    [Column("date_of_birth")]
    public DateTime? DateOfBirth { get; set; }

    [Column("email")]
    public string? Email { get; set; }

    [Column("first_name")]
    public string? FirstName { get; set; }

    [Required]
    [Column("gender")]
    public string Gender { get; set; } = string.Empty;

    [Column("last_name")]
    public string? LastName { get; set; }

    [Column("profile_image")]
    public string? ProfileImage { get; set; }

    [Required]
    [Column("property_role")]
    public string PropertyRole { get; set; } = string.Empty;

    [Required]
    [Column("user_id")]
    public long UserId { get; set; }
}