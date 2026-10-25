namespace DirectoryService.Contracts.DepartmentsDtos;

public record CreateDepartmentDto(
    string Name,
    string Slug,
    Guid? ParentId,
    IEnumerable<Guid>? LocationIds,
    IEnumerable<Guid>? PositionIds) {}
