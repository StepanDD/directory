using System;
using DirectoryService.Domain.Common.Exceptions;
using DirectoryService.Domain.Common;

namespace DirectoryService.Domain.DepartmentSpace.Exceptions;

public sealed class DepartmentNotFoundException : DomainException
{
    public DepartmentNotFoundException(Guid departmentId)
        : base(Error.NotFound(
            "department.not_found",
            $"Отдел '{departmentId}' не найден."))
    {
    }
}
