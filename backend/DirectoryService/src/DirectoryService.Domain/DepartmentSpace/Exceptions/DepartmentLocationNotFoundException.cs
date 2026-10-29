using DirectoryService.Domain.Common.Exceptions;
using DirectoryService.Domain.Common;

namespace DirectoryService.Domain.DepartmentSpace.Exceptions;

public sealed class DepartmentLocationNotFoundException : DomainException
{
    public DepartmentLocationNotFoundException(Guid departmentId, Guid locationId)
        : base(Error.NotFound(
            "department.location.not_found",
            $"Локация '{locationId}' не привязана к отделу '{departmentId}'."))
    {
    }
}
