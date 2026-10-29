using System;

namespace DirectoryService.Domain.Common.Exceptions;

public abstract class DomainException : Exception
{
    public Error Error { get; }

    protected DomainException(Error error)
        : base(error.Message)
    {
        Error = error;
    }
}