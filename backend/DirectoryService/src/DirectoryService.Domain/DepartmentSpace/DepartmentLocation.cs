using System;

namespace DirectoryService.Domain.DepartmentSpace;

public class DepartmentLocation
{
    public Guid Id { get; }
    public Department Department { get; private set; } = null!;
    public Guid DepartmentId { get; private set; }
    public Guid LocationId { get; private set; }
    public bool IsPrimary { get; private set; }
    private DepartmentLocation() { } // Для EF Core
    public DepartmentLocation(Guid id, Department department, Guid locationId, bool isPrimary)
    {
        Id = id;
        Department = department;
        DepartmentId = department.Id;
        LocationId = locationId;
        IsPrimary = isPrimary;
    }
    public void SetPrimary(bool isPrimary)
    {
        IsPrimary = isPrimary;
    }
}
