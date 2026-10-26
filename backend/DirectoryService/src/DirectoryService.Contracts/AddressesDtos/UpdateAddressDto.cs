namespace DirectoryService.Contracts.AddressesDtos;

public record UpdateAddressDto(string? Country, string? City, string? Street, string? HouseNumber, string? ApartmentNumber) {}
