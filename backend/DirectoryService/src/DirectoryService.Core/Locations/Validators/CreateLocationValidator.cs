using System;
using DirectoryService.Contracts.LocationsDtos;
using FluentValidation;

namespace DirectoryService.Core.Locations.Validators;

public class CreateLocationValidator : AbstractValidator<CreateLocationDto>
{
    public CreateLocationValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Название обязательно").WithErrorCode("location.name.required")
            .MaximumLength(100).WithMessage("Название не должно быть длиннее 100 символов").WithErrorCode("location.name.too_long");

        RuleFor(x => x.Address)
            .NotNull().WithMessage("Адрес обязателен").WithErrorCode("location.address.required");

        RuleFor(x => x.Address.Country)
            .NotEmpty().WithMessage("Страна обязательна").WithErrorCode("location.address.country.required")
            .MaximumLength(50).WithMessage("Страна не должна быть длиннее 50 символов").WithErrorCode("location.address.country.too_long");

        RuleFor(x => x.Address.City)
            .NotEmpty().WithMessage("City is required.")
            .MaximumLength(50).WithMessage("City must not exceed 50 characters.");

        RuleFor(x => x.Address.Street)
            .NotEmpty().WithMessage("Street is required.")
            .MaximumLength(100).WithMessage("Street must not exceed 100 characters.");

        RuleFor(x => x.Address.HouseNumber)
            .NotEmpty().WithMessage("House number is required.")
            .MaximumLength(10).WithMessage("House number must not exceed 10 characters.");

        RuleFor(x => x.Address.ApartmentNumber)
            .MaximumLength(10).WithMessage("Apartment number must not exceed 10 characters.");
    }
}
// todo дописать сообщения и коды ошибок