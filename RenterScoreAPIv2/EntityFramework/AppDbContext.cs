namespace RenterScoreAPIv2.EntityFramework;

using Microsoft.EntityFrameworkCore;
using RenterScoreAPIv2.Property;

public class AppDbContext(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<Property> Properties { get; set; }
}