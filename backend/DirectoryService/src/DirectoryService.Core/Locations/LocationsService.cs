using System;
using DirectoryService.Contracts.LocationsDtos;
using DirectoryService.Domain;
using DirectoryService.Domain.ValueObjects;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace DirectoryService.Core.Locations;

public class LocationsService : ILocationsService
{
    private readonly ILocationsRepository _locationsRepository;
    private readonly ILogger<LocationsService> _logger;
    private readonly IValidator<CreateLocationDto> _validator;

    public LocationsService(
        ILocationsRepository locationsRepository,
        IValidator<CreateLocationDto> validator,
        ILogger<LocationsService> logger)
    {
        _locationsRepository = locationsRepository;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Guid> Create(CreateLocationDto locationDto, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(locationDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        if (await _locationsRepository.CheckNameAsync(locationDto.Name, cancellationToken))
        {
            throw new InvalidOperationException($"Location with name '{locationDto.Name}' already exists.");
        }

        Location location = new(
            Guid.NewGuid(),
            locationDto.Name,
            new Address(
                locationDto.Address.Country,
                locationDto.Address.City,
                locationDto.Address.Street,
                locationDto.Address.HouseNumber,
                locationDto.Address.ApartmentNumber));

        var locationId = await _locationsRepository.CreateAsync(location, cancellationToken);

        _logger.LogInformation("Location with id {LocationId} created successfully", locationId);

        return locationId;
    }
}
