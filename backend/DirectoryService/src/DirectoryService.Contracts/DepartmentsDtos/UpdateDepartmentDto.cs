namespace DirectoryService.Contracts.DepartmentsDtos;

public record UpdateDepartmentDto(string? Name, string? Slug, Guid? ParentId) {}