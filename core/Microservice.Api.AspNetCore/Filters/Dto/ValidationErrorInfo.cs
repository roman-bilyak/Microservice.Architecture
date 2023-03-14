namespace Microservice.AspNetCore;

internal class ValidationErrorInfo
{
    public string Member { get; protected set; }

    public string[] Messages { get; protected set; }

    public ValidationErrorInfo(string member, string[] messages)
    {
        Member = member;
        Messages = messages;
    }
}