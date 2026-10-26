using DirectoryService.Contracts.AddressesDtos;

namespace DirectoryService.Contracts.LocationsDtos;

public record CreateLocationDto(string Name, CreateAddressDto Address) {}
