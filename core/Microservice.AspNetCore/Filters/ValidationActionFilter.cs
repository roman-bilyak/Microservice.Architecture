using Microsoft.AspNetCore.Mvc.Filters;

namespace Microservice.AspNetCore;

internal class ValidationActionFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            //TODO: throw specific exception with error details
            throw new Exception("ModelState is not valid!");
        }

        await next();
    }
}