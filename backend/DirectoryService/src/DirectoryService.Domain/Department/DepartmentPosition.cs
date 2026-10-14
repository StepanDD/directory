using System;

namespace DirectoryService.Domain.Department;

public class DepartmentPosition
{
    public Guid Id { get; }
    public Department Department { get; private set; }
    public Guid PositionId { get; private set; }
    public DepartmentPosition(Guid id, Department department, Guid positionId)
    {
        Id = id;
        Department = department;
        PositionId = positionId;
    }
}
