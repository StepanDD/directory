using System;
using DirectoryService.Domain.DepartmentSpace;

namespace DirectoryService.Core.Departments;

public interface IDepartmentsRepository
{
    Task<Guid> AddAsync(Department department, CancellationToken cancellationToken);
    Task<bool> CheckParentIdAsync(Guid? parentId, CancellationToken cancellationToken);
    Task<Department?> GetByIdAsync(Guid departmentId, CancellationToken cancellationToken);
    Task<Department?> GetByIdIncludeLocationsAsync(Guid departmentId, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
