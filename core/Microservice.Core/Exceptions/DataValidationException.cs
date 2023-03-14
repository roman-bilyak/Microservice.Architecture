namespace Microservice.Core;

public class DataValidationException : BaseException
{
    public IReadOnlyDictionary<string, string[]> Errors { get; protected set; }

    public DataValidationException(IDictionary<string, string[]> errors)
        : this("ModelState is not valid! See ValidationErrors for details.", errors)
    {
    }

    public DataValidationException(string message, IDictionary<string, string[]> errors)
        : base(message)
    {
        Errors = new Dictionary<string, string[]>(errors);
    }
}