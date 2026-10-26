using DirectoryService.Contracts.DepartmentsDtos;

namespace DirectoryService.Core.Departments;

public interface IDepartmentsService
{
    Task<Guid> Create(CreateDepartmentDto departmentDto, CancellationToken cancellationToken);
    Task<Guid> Update(Guid departmentId, UpdateDepartmentDto departmentDto, CancellationToken cancellationToken);
    Task BindLocation(Guid departmentId, Guid locationId, bool isPrimary, CancellationToken cancellationToken);
}
