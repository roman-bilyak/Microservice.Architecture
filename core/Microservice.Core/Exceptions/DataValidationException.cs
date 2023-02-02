using System.ComponentModel.DataAnnotations;

namespace Microservice.Core;

public class DataValidationException : BaseException
{
    public IReadOnlyList<ValidationResult> Errors { get; protected set; }

    public DataValidationException(string message, List<ValidationResult> errors)
        : base(message)
    {
        Errors = new List<ValidationResult>(errors);
    }
}