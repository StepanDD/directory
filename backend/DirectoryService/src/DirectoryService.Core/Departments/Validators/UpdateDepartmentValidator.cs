using DirectoryService.Contracts.DepartmentsDtos;
using FluentValidation;

namespace DirectoryService.Core.Departments.Validators;

public class UpdateDepartmentValidator : AbstractValidator<UpdateDepartmentDto>
{
    public UpdateDepartmentValidator()
    {
        RuleFor(x => x.Name)
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

        RuleFor(x => x.Slug)
            .MaximumLength(50).WithMessage("Slug must not exceed 50 characters.");
    }
}
