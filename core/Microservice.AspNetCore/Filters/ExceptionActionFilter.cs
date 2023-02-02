using Microservice.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Authentication;

namespace Microservice.AspNetCore;

internal class ExceptionActionFilter : IAsyncExceptionFilter
{
    public Task OnExceptionAsync(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)GetStatusCode(context.Exception);
        //TODO: return detailed errors in response
        context.Result = new ObjectResult(context.Exception.Message);

        return Task.CompletedTask;
    }

    #region helper methods

    private static HttpStatusCode GetStatusCode(Exception exception)
    {
        if (exception is AuthenticationException 
            || exception is InvalidCredentialException)
        {
            return HttpStatusCode.Unauthorized;
        }

        if (exception is ValidationException)
        {
            return HttpStatusCode.BadRequest;
        }

        if (exception is EntityNotFoundException)
        {
            return HttpStatusCode.NotFound;
        }

        if (exception is NotImplementedException)
        {
            return HttpStatusCode.NotImplemented;
        }

        return HttpStatusCode.InternalServerError;
    }

    #endregion
}