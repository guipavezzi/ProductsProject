using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProductProject.Shared.Exceptions;
using ProductProject.Shared.Responses;

public class ExceptionFIlter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
        {
            var result = context.Exception is ProductProjectException;
            if (result)
                HandleProjectException(context);
            else
                ThrowUnknowError(context);
        }

        private void ThrowUnknowError(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = new ObjectResult(new ResponseJson(context.Exception.Message));
        }

        private void HandleProjectException(ExceptionContext context)
        {
            if (context.Exception is NotFoundException)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Result = new NotFoundObjectResult(new ResponseJson(context.Exception.Message));
            }
        }
}