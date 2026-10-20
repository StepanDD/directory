using System;

namespace DirectoryService.Domain.DepartmentSpace;

public class DepartmentPosition
{
    public Guid Id { get; }
    public Department Department { get; private set; } = null!;
    public Guid PositionId { get; private set; }
    private DepartmentPosition() { } // Для EF Core
    public DepartmentPosition(Guid id, Department department, Guid positionId)
    {
        Id = id;
        Department = department;
        PositionId = positionId;
    }
}
