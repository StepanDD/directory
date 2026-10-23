using DirectoryService.Core.Locations;
using DirectoryService.Domain;
using Microsoft.EntityFrameworkCore;

namespace DirectoryService.Infrastructure.Postgres.Locations;

public sealed class LocationsRepository(AppDbContext dbContext) : ILocationsRepository
{
    public async Task<Guid> CreateAsync(Location location, CancellationToken cancellationToken)
    {
        //await dbContext.Set<Location>().AddAsync(location, cancellationToken);
        //await dbContext.SaveChangesAsync(cancellationToken);
        return Guid.Empty;
    }

    public Task<bool> CheckNameAsync(string name, CancellationToken cancellationToken)
    {
        //return dbContext.Set<Location>()
        //    .AnyAsync(location => location.Name == name, cancellationToken);
        return Task.FromResult(false);
    }
}
