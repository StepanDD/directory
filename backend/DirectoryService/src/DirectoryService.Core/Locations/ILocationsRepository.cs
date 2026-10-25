using System;
using DirectoryService.Domain;

namespace DirectoryService.Core.Locations;

public interface ILocationsRepository
{
    Task<Guid> AddAsync(Location location, CancellationToken cancellationToken);
    Task<bool> CheckNameAsync(string name, CancellationToken cancellationToken);
    Task<bool> CheckLocationsExistenceByIdsAsync(IEnumerable<Guid>? locationIds, CancellationToken cancellationToken);
}
