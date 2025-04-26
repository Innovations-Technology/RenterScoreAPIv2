namespace RenterScoreAPIv2.User;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("users", Schema = "dbo")]
public class User
{
    [Key]
    [Column("user_id")]
    public long UserId { get; set; }

    [Column("created_date")]
    public DateTime CreatedDate { get; set; }

    [Column("created_user")]
    public long CreatedUser { get; set; }

    [Column("modified_date")]
    public DateTime ModifiedDate { get; set; }

    [Column("modified_user")]
    public long ModifiedUser { get; set; }

    [Column("account_status")]
    [Required]
    [MaxLength(255)]
    public required string AccountStatus { get; set; }

    [Column("email")]
    [Required]
    [MaxLength(255)]
    public required string Email { get; set; }

    [Column("email_status")]
    [Required]
    [MaxLength(255)]
    public required string EmailStatus { get; set; }

    [Column("expiry_date")]
    public DateTimeOffset ExpiryDate { get; set; }

    [Column("password")]
    [Required]
    [MaxLength(255)]
    public required string Password { get; set; }

    [Column("property_role")]
    [Required]
    [MaxLength(255)]
    public required string PropertyRole { get; set; }

    [Column("user_role")]
    [Required]
    [MaxLength(255)]
    public required string UserRole { get; set; }

    [Column("verification_token")]
    [Required]
    [MaxLength(255)]
    public required string VerificationToken { get; set; }
}