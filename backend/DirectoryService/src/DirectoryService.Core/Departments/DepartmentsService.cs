using System;
using DirectoryService.Contracts.DepartmentsDtos;
using DirectoryService.Core.Locations;
using DirectoryService.Domain;
using DirectoryService.Domain.Common.Exceptions;
using DirectoryService.Domain.DepartmentSpace;
using DirectoryService.Domain.DepartmentSpace.Exceptions;
using DirectoryService.Domain.Locations.Exceptions;
using FluentValidation;

namespace DirectoryService.Core.Departments;

public class DepartmentsService : IDepartmentsService
{
    private readonly IValidator<CreateDepartmentDto> _createDepartmentValidator;
    private readonly IValidator<UpdateDepartmentDto> _updateDepartmentValidator;
    private readonly IDepartmentsRepository _departmentsRepository;
    private readonly ILocationsRepository _locationsRepository;

    public DepartmentsService(
        IValidator<CreateDepartmentDto> createDepartmentValidator,
        IValidator<UpdateDepartmentDto> updateDepartmentValidator,
        IDepartmentsRepository departmentsRepository,
        ILocationsRepository locationsRepository)
    {
        _createDepartmentValidator = createDepartmentValidator;
        _updateDepartmentValidator = updateDepartmentValidator;
        _departmentsRepository = departmentsRepository;
        _locationsRepository = locationsRepository;
    }

    public async Task BindLocation(Guid departmentId, Guid locationId, bool isPrimary, CancellationToken cancellationToken)
    {
        Department? department = await _departmentsRepository.GetByIdIncludeLocationsAsync(departmentId, cancellationToken)
            ?? throw new DepartmentNotFoundException(departmentId);
        if (!await _locationsRepository.CheckLocationsExistenceByIdsAsync([locationId], cancellationToken))
        {
            throw new LocationNotFoundException(locationId);
        }
        department.AddLocation(locationId, isPrimary);
        await _departmentsRepository.SaveChangesAsync(cancellationToken);
    }
    public async Task UnBindLocation(Guid departmentId, Guid locationId, CancellationToken cancellationToken)
    {
        Department? department = await _departmentsRepository.GetByIdIncludeLocationsAsync(departmentId, cancellationToken)
            ?? throw new DepartmentNotFoundException(departmentId);
        if (!await _locationsRepository.CheckLocationsExistenceByIdsAsync([locationId], cancellationToken))
        {
            throw new LocationNotFoundException(locationId);
        }
        department.DeleteLocation(locationId);
        await _departmentsRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task<Guid> Create(CreateDepartmentDto departmentDto, CancellationToken cancellationToken)
    {
        var validationResult = await _createDepartmentValidator.ValidateAsync(departmentDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new DomainValidationException(validationResult.ToErrors());
        }
        if (!await _departmentsRepository.CheckParentIdAsync(departmentDto.ParentId, cancellationToken))
        {
            throw new DepartmentNotFoundException(departmentDto.ParentId ?? Guid.Empty);
        }
        if (!await _locationsRepository.CheckLocationsExistenceByIdsAsync(departmentDto.LocationIds, cancellationToken))
        {
            throw new LocationNotFoundException();
        }
        // todo проверка позиций

        Department? parentDepartment = null;
        if (departmentDto.ParentId is not null)
        {
            parentDepartment = await _departmentsRepository.GetByIdAsync(departmentDto.ParentId.Value, cancellationToken);
        }
        var department = new Department(
            Guid.CreateVersion7(),
            departmentDto.Name,
            departmentDto.Slug,
            parentDepartment,
            departmentDto.LocationIds,
            departmentDto.PositionIds);

        return await _departmentsRepository.AddAsync(department, cancellationToken);
    }

    public async Task<Guid> Update(Guid departmentId, UpdateDepartmentDto departmentDto, CancellationToken cancellationToken)
    {
        var validationResult = await _updateDepartmentValidator.ValidateAsync(departmentDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new DomainValidationException(validationResult.ToErrors());
        }
        if (!await _departmentsRepository.CheckParentIdAsync(departmentDto.ParentId, cancellationToken))
        {
            throw new DepartmentNotFoundException(departmentDto.ParentId ?? Guid.Empty);
        }

        var department = await _departmentsRepository.GetByIdAsync(departmentId, cancellationToken);
        if (department is null)
        {
            throw new DepartmentNotFoundException(departmentId);
        }
        if (!string.IsNullOrWhiteSpace(departmentDto.Name))
        {
            department.ChangeName(departmentDto.Name);
        }
        if (!string.IsNullOrWhiteSpace(departmentDto.Slug))
        {
            department.ChangeSlug(departmentDto.Slug);
        }
        if (departmentDto.ParentId is not null && departmentDto.ParentId != department.ParentId)
        {
            Department? newParent = await _departmentsRepository.GetByIdAsync(departmentDto.ParentId.Value, cancellationToken);
            department.ChangeParent(newParent);
        }
        await _departmentsRepository.SaveChangesAsync(cancellationToken);
        return department.Id;
    }
}
