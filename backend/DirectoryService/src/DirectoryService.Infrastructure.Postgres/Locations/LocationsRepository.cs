using DirectoryService.Core.Locations;
using DirectoryService.Domain;
using Microsoft.EntityFrameworkCore;

namespace DirectoryService.Infrastructure.Postgres.Locations;

public sealed class LocationsRepository : ILocationsRepository
{
    private readonly AppDbContext _dbContext;

    public LocationsRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Guid> AddAsync(Location location, CancellationToken cancellationToken)
    {
        await _dbContext.Set<Location>().AddAsync(location, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return location.Id;
    }

    public async Task<bool> CheckLocationsExistenceByIdsAsync(IEnumerable<Guid>? locationIds, CancellationToken cancellationToken)
    {
        if (locationIds?.Any() != true)
        {
            return true;
        }

        var ids = locationIds.Distinct().ToList();
        var existingCount = await _dbContext.Set<Location>()
            .CountAsync(location => ids.Contains(location.Id), cancellationToken);

        return existingCount == ids.Count;
    }

    public async Task<bool> CheckNameAsync(string name, CancellationToken cancellationToken)
    {
        return await _dbContext.Set<Location>().AnyAsync(location => location.Name == name, cancellationToken);
    }

    public async Task<Location?> GetByIdAsync(Guid locationId, CancellationToken cancellationToken)
    {
        return await _dbContext.Set<Location>().FirstOrDefaultAsync(l => l.Id == locationId, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
