using System;

namespace DirectoryService.Domain.DepartmentSpace;

public class Department
{
    private const string PathSeparator = "/";
    private readonly List<DepartmentLocation> _locations = [];
    private readonly List<DepartmentPosition> _positions = [];

    public Guid Id { get; }
    public string Name { get; private set; } = string.Empty;
    public string Slug { get; private set; } = string.Empty;
    public string Path { get; private set; } = string.Empty;
    public Guid? ParentId { get; private set; }
    public Department? Parent { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    // Отношения
    public IReadOnlyList<DepartmentLocation> Locations => _locations;
    public IReadOnlyList<DepartmentPosition> Positions => _positions;
    private Department() { } // Для EF Core
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
            Path = parent.Path + PathSeparator + slug;
        }
        else
            Path = slug;       
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;

        if (locationIds != null)
        {
            var locationIdsDistinct = locationIds.Distinct().ToList();
            var locations = locationIdsDistinct
                .Select(locationId => new DepartmentLocation(Guid.CreateVersion7(), this, locationId, false))
                .ToList();
            _locations = locations;
        }

        if (positionIds != null)
        {
            var positionIdsDistinct = positionIds.Distinct().ToList();
            var positions = positionIdsDistinct
                .Select(positionId => new DepartmentPosition(Guid.CreateVersion7(), this, positionId))
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
        Path = Parent != null ? Parent.Path + PathSeparator + newSlug : newSlug;
        UpdatedAt = DateTime.UtcNow;
    }
    public void ChangeParent(Department? newParent)
    {
        Parent = newParent;
        ParentId = newParent?.Id;
        Path = newParent != null ? newParent.Path + PathSeparator + Slug : Slug;
        UpdatedAt = DateTime.UtcNow;
    }
    public void AddLocation(Guid locationId, bool isPrimary = false)
    {
        var newLocation = new DepartmentLocation(Guid.CreateVersion7(), this, locationId, isPrimary);
        if (_locations.Exists(l => l.LocationId == locationId))
        {
            throw new InvalidOperationException("Домен: эта локация уже привязана");
        }
        _locations.Add(newLocation);
        UpdatedAt = DateTime.UtcNow;
    }
    public void AddLocation(IEnumerable<Guid> locationIds)
    {
        throw new NotSupportedException();
        foreach (var locationId in locationIds)
        {
            var newLocation = new DepartmentLocation(Guid.CreateVersion7(), this, locationId, false);
            _locations.Add(newLocation);
        }
        UpdatedAt = DateTime.UtcNow;
    }
    public void DeleteLocation(Guid locationId)
    {
        var locationForDelete = _locations.FirstOrDefault(l => l.LocationId == locationId)
            ?? throw new InvalidOperationException("Домен: Такой локации не привязано");
        _locations.Remove(locationForDelete);
        UpdatedAt = DateTime.UtcNow;
    }
    // туду: как-то сделать выбор праймовой локации
    public void AddPosition(Guid positionId)
    {
        var newPosition = new DepartmentPosition(Guid.CreateVersion7(), this, positionId);
        _positions.Add(newPosition);
        UpdatedAt = DateTime.UtcNow;
    }
    public void AddPosition(IEnumerable<Guid> positionIds)
    {
        foreach (var positionId in positionIds)
        {
            var newPosition = new DepartmentPosition(Guid.CreateVersion7(), this, positionId);
            _positions.Add(newPosition);
        }
        UpdatedAt = DateTime.UtcNow;
    }
}
