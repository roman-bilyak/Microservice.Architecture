namespace Microservice.AspNetCore;

internal class ErrorInfo
{
    public string Message { get; protected set; }

    public string Details { get; protected set; }

    public ValidationErrorInfo[] ValidationErrors { get; protected set; }

    public ErrorInfo(string message, string details, ValidationErrorInfo[] validationErrors)
    {
        Message = message;
        Details = details;
        ValidationErrors = validationErrors;
    }
}