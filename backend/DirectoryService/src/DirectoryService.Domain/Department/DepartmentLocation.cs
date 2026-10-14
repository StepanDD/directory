using System;

namespace DirectoryService.Domain.Department;

public class DepartmentLocation
{
    public Guid Id { get; }
    public Department Department { get; private set; }
    public Guid LocationId { get; private set; }
    public bool IsPrimary { get; private set; }
    public DepartmentLocation(Guid id, Department department, Guid locationId, bool isPrimary)
    {
        Id = id;
        Department = department;
        LocationId = locationId;
        IsPrimary = isPrimary;
    }
    public void SetPrimary(bool isPrimary)
    {
        IsPrimary = isPrimary;
    }
}
