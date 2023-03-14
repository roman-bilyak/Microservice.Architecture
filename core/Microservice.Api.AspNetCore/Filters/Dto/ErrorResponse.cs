namespace Microservice.AspNetCore;

internal class ErrorResponse
{
    public ErrorInfo Error { get; protected set; }

    public ErrorResponse(string message)
    : this(message, string.Empty)
    {
    }

    public ErrorResponse(string message, string details)
        : this(message, details, Array.Empty<ValidationErrorInfo>())
    {
    }

    public ErrorResponse(string message, ValidationErrorInfo[] validationErrors)
        : this(message, string.Empty, validationErrors)
    {
    }

    public ErrorResponse(string message, string details, ValidationErrorInfo[] validationErrors)
    {
        Error = new ErrorInfo(message, details, validationErrors);
    }
}