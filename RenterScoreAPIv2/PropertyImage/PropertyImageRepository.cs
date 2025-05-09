namespace RenterScoreAPIv2.PropertyImage;

using Microsoft.EntityFrameworkCore;
using RenterScoreAPIv2.EntityFramework;

public class PropertyImageRepository : IPropertyImageRepository
{
    private readonly AppDbContext _context;

    public PropertyImageRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PropertyImage>> GetPropertyImagesByIdsAsync(IEnumerable<long> propertyIds)
    {
        return await _context.PropertyImages
            .Where(pi => propertyIds.Contains(pi.PropertyId))
            .ToListAsync();
    }

    public async Task<IEnumerable<PropertyImage>> GetPropertyImagesByIdAsync(long propertyId)
    {
        return await _context.PropertyImages
            .Where(pi => pi.PropertyId == propertyId)
            .ToListAsync();
    }
}