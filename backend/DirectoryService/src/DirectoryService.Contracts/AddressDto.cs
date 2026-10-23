namespace DirectoryService.Contracts;

public record AddressDto(string Country, string City, string Street, string HouseNumber, string? ApartmentNumber) {}
