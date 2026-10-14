using System;

namespace DirectoryService.Domain.Department;

public class DepartmentLocation
{
    public Guid Id { get; }
    public Department Department { get; private set; }
    public Guid LocationId { get; private set; }
    public bool IsPrimaty { get; private set; }
    public DepartmentLocation(Guid id, Department department, Guid locationId, bool isPrimary)
    {
        Id = id;
        Department = department;
        LocationId = locationId;
        IsPrimaty = isPrimary;
    }
    public void setPrimary(bool isPrimary)
    {
        IsPrimaty = isPrimary;
    }
}
