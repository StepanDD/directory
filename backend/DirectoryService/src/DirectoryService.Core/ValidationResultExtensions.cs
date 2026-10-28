using System;
using DirectoryService.Domain.Common;
using FluentValidation.Results;

namespace DirectoryService.Core;

public static class ValidationResultExtensions
{
    public static Error[] ToErrors(this ValidationResult validationResult)
    {
        return validationResult.Errors
            .Select(failure => Error.Validation(
                failure.ErrorCode,
                failure.ErrorMessage,
                failure.PropertyName))
            .ToArray();
    }
}
