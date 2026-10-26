using DirectoryService.Contracts.LocationsDtos;
using FluentValidation;

namespace DirectoryService.Core.Locations.Validators;

public class UpdateLocationValidator : AbstractValidator<UpdateLocationDto>
{
    public UpdateLocationValidator()
    {
        RuleFor(x => x.Name)
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

        When(x => x.Address != null, () =>
        {
            RuleFor(x => x.Address!.Country)
                .MaximumLength(50).WithMessage("Country must not exceed 50 characters.");

            RuleFor(x => x.Address!.City)
                .MaximumLength(50).WithMessage("City must not exceed 50 characters.");

            RuleFor(x => x.Address!.Street)
                .MaximumLength(100).WithMessage("Street must not exceed 100 characters.");

            RuleFor(x => x.Address!.HouseNumber)
                .MaximumLength(10).WithMessage("House number must not exceed 10 characters.");

            RuleFor(x => x.Address!.ApartmentNumber)
                .MaximumLength(10).WithMessage("Apartment number must not exceed 10 characters.");
        });
    }
}
