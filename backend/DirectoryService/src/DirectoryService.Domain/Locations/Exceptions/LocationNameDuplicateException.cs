using System;
using DirectoryService.Domain.Common.Exceptions;
using DirectoryService.Domain.Common;

namespace DirectoryService.Domain.Locations.Exceptions;

public sealed class LocationNameDuplicateException : DomainException
{
    public LocationNameDuplicateException(string locationName)
        : base(Error.Conflict(
            "location.name.duplicate",
            $"Локация с именем '{locationName}' уже существует."))
    {
    }
}
