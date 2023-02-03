using FluentValidation;
using Microservice.Core;
using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;

namespace Microservice.AspNetCore;

internal class ValidationActionFilter : IAsyncActionFilter
{
    private readonly IServiceProvider _serviceProvider;

    public ValidationActionFilter(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        await ValidateArguments(context);

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

    private async Task ValidateArguments(ActionExecutingContext context)
    {
        foreach (var (key, value) in context.ActionArguments)
        {
            if (value is null)
            {
                continue;
            }

            Type genericType = typeof(IValidator<>).MakeGenericType(value.GetType());
            if (_serviceProvider.GetService(genericType) is not IValidator validator)
            {
                continue;
            }

            ValidationContext<object> validationContext = new(value);
            var result = await validator.ValidateAsync(validationContext);
            if (result.IsValid)
            {
                continue;
            }

            foreach (var error in result.Errors)
            {
                context.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
        }
    }
}