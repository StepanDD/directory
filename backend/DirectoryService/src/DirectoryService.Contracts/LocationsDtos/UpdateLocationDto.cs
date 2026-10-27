using DirectoryService.Contracts.AddressesDtos;

namespace DirectoryService.Contracts.LocationsDtos;

public record UpdateLocationDto(string? Name, UpdateAddressDto? Address) {}