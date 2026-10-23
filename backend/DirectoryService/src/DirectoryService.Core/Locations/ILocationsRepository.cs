using System;
using DirectoryService.Domain;

namespace DirectoryService.Core.Locations;

public interface ILocationsRepository
{
    Task<Guid> CreateAsync(Location location, CancellationToken cancellationToken);
    Task<bool> CheckNameAsync(string name, CancellationToken cancellationToken);
}
