using System;
using DirectoryService.Domain.ValueObjects;

namespace DirectoryService.Domain;

public class Location
{
    public Guid Id { get; }
    public string Name { get; private set; }
    public Address Address { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public Location(Guid id, string name, Address address)
    {
        Id = id;
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Location name cannot be null or empty.", nameof(name));
        Name = name;
        Address = address;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
    public void ChangeName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("Location name cannot be null or empty.", nameof(newName));
        Name = newName;
        UpdatedAt = DateTime.UtcNow;
    }
    public void ChangeAddress(Address newAddress)
    {
        Address = newAddress;
        UpdatedAt = DateTime.UtcNow;
    }
}
