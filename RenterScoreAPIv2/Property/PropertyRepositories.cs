namespace RenterScoreAPIv2.Property;

using Microsoft.EntityFrameworkCore;
using RenterScoreAPIv2.EntityFramework;

public class PropertyRepository(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<Property>> GetAllPropertiesAsync()
    {
        return await _context.Properties.ToListAsync();
    }

    public async Task<Property?> GetPropertyByIdAsync(int id)
    {
        return await _context.Properties.FindAsync(id);
    }

    public async Task AddPropertyAsync(Property property)
    {
        await _context.Properties.AddAsync(property);
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePropertyAsync(Property property)
    {
        _context.Properties.Update(property);
        await _context.SaveChangesAsync();
    }
}
