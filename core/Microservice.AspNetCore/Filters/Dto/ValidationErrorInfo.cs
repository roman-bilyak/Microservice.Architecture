namespace Microservice.AspNetCore;

internal class ValidationErrorInfo
{
    public string? Message { get; protected set; }

    public string[] Members { get; protected set; }

    public ValidationErrorInfo(string? message, string[] members)
    {
        Message = message;
        Members = members;
    }
}