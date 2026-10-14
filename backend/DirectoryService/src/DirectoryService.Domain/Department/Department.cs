using System;

namespace DirectoryService.Domain.Department;

public class Department
{
    private List<DepartmentLocation>? _locations;
    private List<DepartmentPosition>? _positions;

    public Guid Id { get; }
    public string Name { get; private set; }
    public string Slug { get; private set; }
    public string Path { get; private set; }
    public Guid? ParentId { get; private set; }
    public Department? Parent { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    // Отношения
    public IReadOnlyList<DepartmentLocation>? Locations => _locations;
    public IReadOnlyList<DepartmentPosition>? Positions => _positions;
    public Department(Guid id, string name, string slug,
        Department? parent, IEnumerable<Guid>? locationIds, IEnumerable<Guid>? positionIds)
    {
        Id = id;
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Department name cannot be null or empty.", nameof(name));
        Name = name;
        if (string.IsNullOrWhiteSpace(slug))
            throw new ArgumentException("Department slug cannot be null or empty.", nameof(slug));
        Slug = slug;
        if (parent != null)
        {
            Parent = parent;
            ParentId = parent.Id;
            Path = parent.Path + "/" + slug;
        }
        else
            Path = slug;       
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;

        if (locationIds != null)
        {
            var locations = locationIds
                .Select(locationId => new DepartmentLocation(Guid.NewGuid(), this, locationId, false))
                .ToList();
            _locations = locations;
        }

        if (positionIds != null)
        {
            var positions = positionIds
                .Select(positionId => new DepartmentPosition(Guid.NewGuid(), this, positionId))
                .ToList();
            _positions = positions;
        }
    }
    public void ChangeName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("Department name cannot be null or empty.", nameof(newName));
        Name = newName;
        UpdatedAt = DateTime.UtcNow;
    }
    public void ChangeSlug(string newSlug)
    {
        if (string.IsNullOrWhiteSpace(newSlug))
            throw new ArgumentException("Department slug cannot be null or empty.", nameof(newSlug));
        Slug = newSlug;
        UpdatedAt = DateTime.UtcNow;
    }
    public void ChangeParent(Department? newParent)
    {
        Parent = newParent;
        ParentId = newParent?.Id;
        Path = newParent != null ? newParent.Path + "/" + Slug : Slug;
        UpdatedAt = DateTime.UtcNow;
    }
    public void AddLocation(Guid locationId, bool isPrimary)
    {
        if (_locations == null)
            _locations = new List<DepartmentLocation>();

        var newLocation = new DepartmentLocation(Guid.NewGuid(), this, locationId, isPrimary);
        _locations.Add(newLocation);
        UpdatedAt = DateTime.UtcNow;
    }
    public void AddLocation(IEnumerable<Guid> locationIds)
    {
        if (_locations == null)
            _locations = new List<DepartmentLocation>();

        foreach (var locationId in locationIds)
        {
            var newLocation = new DepartmentLocation(Guid.NewGuid(), this, locationId, false);
            _locations.Add(newLocation);
        }
        UpdatedAt = DateTime.UtcNow;
    }
    // todo: как-то сделать выбор праймовой локации
    public void AddPosition(Guid positionId)
    {
        if (_positions == null)
            _positions = new List<DepartmentPosition>();

        var newPosition = new DepartmentPosition(Guid.NewGuid(), this, positionId);
        _positions.Add(newPosition);
        UpdatedAt = DateTime.UtcNow;
    }
    public void AddPosition(IEnumerable<Guid> positionIds)
    {
        if (_positions == null)
            _positions = new List<DepartmentPosition>();

        foreach (var positionId in positionIds)
        {
            var newPosition = new DepartmentPosition(Guid.NewGuid(), this, positionId);
            _positions.Add(newPosition);
        }
        UpdatedAt = DateTime.UtcNow;
    }
}
