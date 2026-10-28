using System;
using DirectoryService.Contracts.LocationsDtos;
using DirectoryService.Domain.Common;
using DirectoryService.Domain.Common.Exceptions;
using DirectoryService.Domain.Locations;
using DirectoryService.Domain.Locations.Exceptions;
using DirectoryService.Domain.ValueObjects;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace DirectoryService.Core.Locations;

public class LocationsService : ILocationsService
{
    private readonly ILocationsRepository _locationsRepository;
    private readonly ILogger<LocationsService> _logger;
    private readonly IValidator<CreateLocationDto> _createLocationValidator;
    private readonly IValidator<UpdateLocationDto> _updateLocationValidator;

    public LocationsService(
        ILocationsRepository locationsRepository,
        IValidator<CreateLocationDto> createLocationValidator,
        IValidator<UpdateLocationDto> updateLocationValidator,
        ILogger<LocationsService> logger)
    {
        _locationsRepository = locationsRepository;
        _createLocationValidator = createLocationValidator;
        _updateLocationValidator = updateLocationValidator;
        _logger = logger;
    }

    public async Task<Guid> Create(CreateLocationDto locationDto, CancellationToken cancellationToken)
    {
        var validationResult = await _createLocationValidator.ValidateAsync(locationDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new DomainValidationException(validationResult.ToErrors());
        }
        if (await _locationsRepository.CheckNameAsync(locationDto.Name, cancellationToken))
        {
            throw new LocationNameDuplicateException(locationDto.Name);
        }

        Location location = new(
            Guid.CreateVersion7(),
            locationDto.Name,
            new Address(
                locationDto.Address.Country,
                locationDto.Address.City,
                locationDto.Address.Street,
                locationDto.Address.HouseNumber,
                locationDto.Address.ApartmentNumber));

        var locationId = await _locationsRepository.AddAsync(location, cancellationToken);

        _logger.LogInformation("Location with id {LocationId} created successfully", locationId);

        return locationId;
    }

    public async Task<Guid> Update(Guid locationId, UpdateLocationDto locationDto, CancellationToken cancellationToken)
    {
        var validationResult = await _updateLocationValidator.ValidateAsync(locationDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new DomainValidationException(validationResult.ToErrors());
        }
        Location? location = await _locationsRepository.GetByIdAsync(locationId, cancellationToken)
            ?? throw new LocationNotFoundException(locationId);

        if (locationDto.Name is not null
                && !string.Equals(locationDto.Name, location.Name, StringComparison.Ordinal)
                && await _locationsRepository.CheckNameAsync(locationDto.Name, cancellationToken))
        {
            throw new LocationNameDuplicateException(locationDto.Name);
        }

        if (!string.IsNullOrWhiteSpace(locationDto.Name))
        {
            location.ChangeName(locationDto.Name);
        }
        if (locationDto.Address is not null)
        {
            var newAddress = location.Address.Update(
                locationDto.Address.Country,
                locationDto.Address.City,
                locationDto.Address.Street,
                locationDto.Address.HouseNumber,
                locationDto.Address.ApartmentNumber);
            location.ChangeAddress(newAddress);
        }
        await _locationsRepository.SaveChangesAsync(cancellationToken);
        return location.Id;
    }
}
