namespace DirectoryService.Contracts.AddressesDtos;

public record CreateAddressDto(string Country, string City, string Street, string HouseNumber, string? ApartmentNumber) {}
