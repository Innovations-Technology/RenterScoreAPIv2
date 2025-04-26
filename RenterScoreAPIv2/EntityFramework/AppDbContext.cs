namespace RenterScoreAPIv2.EntityFramework;

using Microsoft.EntityFrameworkCore;
using RenterScoreAPIv2.Property;
using RenterScoreAPIv2.UserProfile;
using RenterScoreAPIv2.User;

public class AppDbContext(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<Property> Properties { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<User> Users { get; set; }
}