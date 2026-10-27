using System;
using DirectoryService.Contracts.DepartmentsDtos;
using FluentValidation;

namespace DirectoryService.Core.Departments.Validators;

public class CreateDepartmentValidator : AbstractValidator<CreateDepartmentDto>
{
    public CreateDepartmentValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

        RuleFor(x => x.Slug)
            .NotEmpty().WithMessage("Slug is required.")
            .MaximumLength(50).WithMessage("Slug must not exceed 50 characters.");
    }
}
