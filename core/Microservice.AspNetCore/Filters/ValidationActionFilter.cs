using Microservice.Core;
using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;

namespace Microservice.AspNetCore;

internal class ValidationActionFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            List<ValidationResult> errors = new();
            foreach (var state in context.ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    errors.Add(new ValidationResult(error.ErrorMessage, new[] { state.Key }));
                }
            }
            if (errors.Any())
            {
                throw new DataValidationException("ModelState is not valid! See ValidationErrors for details.", errors);
            }
        }

        await next();
    }
}