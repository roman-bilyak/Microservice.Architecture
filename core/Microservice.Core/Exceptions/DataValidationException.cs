using System.ComponentModel.DataAnnotations;

namespace Microservice.Core;

public class DataValidationException : BaseException
{
    public IReadOnlyList<ValidationResult> Errors { get; protected set; }

    public DataValidationException(params ValidationResult[] errors)
        : this("ModelState is not valid! See ValidationErrors for details.", errors)
    {
    }
    public DataValidationException(string message, params ValidationResult[] errors)
        : base(message)
    {
        Errors = new List<ValidationResult>(errors);
    }
}