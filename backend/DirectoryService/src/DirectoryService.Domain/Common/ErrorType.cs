using System.Text.Json.Serialization;

namespace DirectoryService.Domain.Common;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ErrorType
{
    Validation,
    NotFound,
    Conflict,
    Internal,
}
