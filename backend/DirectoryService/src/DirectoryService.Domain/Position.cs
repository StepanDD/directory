using System;

namespace DirectoryService.Domain;

public class Position
{
    public Guid Id { get; }
    public string Name { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public Position(Guid id, string name)
    {
        Id = id;
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Position name cannot be null or empty.", nameof(name));
        Name = name;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
    public void ChangeName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("Position name cannot be null or empty.", nameof(newName));
        Name = newName;
        UpdatedAt = DateTime.UtcNow;
    }
}
