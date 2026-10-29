namespace DirectoryService.Domain.Common.Exceptions;

public class DomainValidationException : Exception
{
    public IReadOnlyList<Error> Errors { get; }

    public DomainValidationException(IEnumerable<Error> errors)
        : base("One or more validation errors occurred.")
    {
        Errors = errors.ToArray();
    }
}
