namespace RenterScoreAPIv2.EntityFramework;

using Microsoft.EntityFrameworkCore;
using RenterScoreAPIv2.Property;
using RenterScoreAPIv2.UserProfile;
using RenterScoreAPIv2.User;
using RenterScoreAPIv2.Utilities;
using RenterScoreAPIv2.PropertyImage;
using RenterScoreAPIv2.PropertyRating;

public class AppDbContext(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<Property> Properties { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<PropertyImage> PropertyImages { get; set; }
    public DbSet<Rating> Ratings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure the composite primary key for Rating
        modelBuilder.Entity<Rating>()
            .HasKey(r => new { r.UserId, r.PropertyId });
            
        SetDefaultSchema(modelBuilder);
        SetTableName(modelBuilder);
        SetSetColumnName(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }

    private static void SetDefaultSchema(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("dbo");
    }

    private static void SetTableName(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (entityType.ClrType == typeof(User))
            {
                entityType.SetTableName("users");
            }
            else if (entityType.ClrType == typeof(Rating))
            {
                entityType.SetTableName("user_rating");
            }
            else
            {
                entityType.SetTableName(entityType.ClrType.Name.ToSnakeCase());
            }
        }
    }

    private static void SetSetColumnName(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                property.SetColumnName(property.Name.ToSnakeCase());
            }
        }
    }
}