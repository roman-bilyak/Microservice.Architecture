using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Microservice.AspNetCore;

internal class ExceptionActionFilter : IAsyncExceptionFilter
{
    public Task OnExceptionAsync(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        //TODO: return detailed errors in response
        context.Result = new ObjectResult(context.Exception.Message);

        return Task.CompletedTask;
    }
}