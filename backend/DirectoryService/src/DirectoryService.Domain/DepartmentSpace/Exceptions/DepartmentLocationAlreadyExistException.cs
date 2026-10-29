using DirectoryService.Domain.Common.Exceptions;
using DirectoryService.Domain.Common;

namespace DirectoryService.Domain.DepartmentSpace.Exceptions;

public sealed class DepartmentLocationAlreadyExistException : DomainException
{
    public DepartmentLocationAlreadyExistException(Guid departmentId, Guid locationId)
        : base(Error.Conflict(
            "department.location.duplicate",
            $"Локация '{locationId}' уже привязана к отделу '{departmentId}'."))
    {
    }
}
