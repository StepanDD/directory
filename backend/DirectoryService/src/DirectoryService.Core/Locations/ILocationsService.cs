using DirectoryService.Contracts.LocationsDtos;

namespace DirectoryService.Core.Locations;

public interface ILocationsService
{
    Task<Guid> Create(CreateLocationDto locationDto, CancellationToken cancellationToken);
    Task<Guid> Update(Guid locationId, UpdateLocationDto locationDto, CancellationToken cancellationToken);
}
