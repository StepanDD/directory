using DirectoryService.Domain.Common.Exceptions;
using DirectoryService.Domain.Common;

namespace DirectoryService.Domain.Locations.Exceptions;

public sealed class LocationNotFoundException : DomainException
{
    public LocationNotFoundException(Guid locationId)
        : base(Error.NotFound(
            "location.not_found",
            $"Локация '{locationId}' не найдена."))
    {
    }
    public LocationNotFoundException()
        : base(Error.NotFound(
            "location.not_found",
            $"1 или несколько локаций не найдены."))
    {
    }
}
