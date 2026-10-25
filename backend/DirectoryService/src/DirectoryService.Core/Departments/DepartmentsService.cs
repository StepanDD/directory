using System;
using DirectoryService.Contracts.DepartmentsDtos;
using DirectoryService.Core.Locations;
using DirectoryService.Domain.DepartmentSpace;
using FluentValidation;

namespace DirectoryService.Core.Departments;

public class DepartmentsService : IDepartmentsService
{
    private readonly IValidator<CreateDepartmentDto> _validator;
    private readonly IDepartmentsRepository _departmentsRepository;
    private readonly ILocationsRepository _locationsRepository;

    public DepartmentsService(
        IValidator<CreateDepartmentDto> validator,
        IDepartmentsRepository departmentsRepository,
        ILocationsRepository locationsRepository)
    {
        _validator = validator;
        _departmentsRepository = departmentsRepository;
        _locationsRepository = locationsRepository;
    }

    public async Task<Guid> Create(CreateDepartmentDto departmentDto, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(departmentDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        if (!await _departmentsRepository.CheckParentIdAsync(departmentDto.ParentId, cancellationToken))
        {
            throw new InvalidOperationException("Parent department does not exist.");
        }
        if (!await _locationsRepository.CheckLocationsExistenceByIdsAsync(departmentDto.LocationIds, cancellationToken))
        {
            throw new InvalidOperationException("One or more locations do not exist.");
        }
        // todo проверка позиций

        Department? parentDepartment = null;
        if (departmentDto.ParentId is not null)
        {
            parentDepartment = await _departmentsRepository.GetByIdAsync(departmentDto.ParentId.Value, cancellationToken);
        }
        var department = new Department(
            Guid.NewGuid(),
            departmentDto.Name,
            departmentDto.Slug,
            parentDepartment,
            departmentDto.LocationIds,
            departmentDto.PositionIds);

        return await _departmentsRepository.AddAsync(department, cancellationToken);
    }
}
