using Microservice.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Security.Authentication;

namespace Microservice.AspNetCore;

internal class ExceptionActionFilter : IAsyncExceptionFilter
{
    public Task OnExceptionAsync(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)GetStatusCode(context.Exception);
        context.Result = new ObjectResult(GetErrorResponse(context.Exception));

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

        if (exception is DataValidationException)
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

    private static ErrorResponse GetErrorResponse(Exception exception)
    {
        if (exception is DataValidationException dataValidationException)
        {
            ValidationErrorInfo[] errors = dataValidationException.Errors
                .Select(x => new ValidationErrorInfo(x.ErrorMessage, x.MemberNames.ToArray()))
                .ToArray();
            return new ErrorResponse(exception.Message, errors);
        }
        return new ErrorResponse(exception.Message);
    }

    #endregion
}