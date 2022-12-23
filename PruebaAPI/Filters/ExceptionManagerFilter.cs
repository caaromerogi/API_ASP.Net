
using System.Net;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PruebaAPI.Exceptions;
//Another way to controll exceptions https://stackoverflow.com/questions/38630076/asp-net-core-web-api-exception-handling
namespace PruebaAPI.Filters;
[Obsolete("This is the first implementation of Exception Handler. Now, is used ErrorHandlerMiddleware")]
public class ExceptionManagerFilter : ExceptionFilterAttribute
{

    public override void OnException(ExceptionContext context)
    {
        if(context.Exception is ElementNotFoundException){
            context.Result = new JsonResult(new {Codigo = "0001", Mensaje = context.Exception.Message});
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
        }

        if(context.Exception is InvalidElementException<List<ValidationFailure>>){
            var invalidElementContext = (InvalidElementException<List<ValidationFailure>>)context.Exception;

            Dictionary<string,string> errors = new Dictionary<string, string>();

            foreach (var error in invalidElementContext.Data)
            {
                var casterror = error  as ValidationFailure;
                errors.Add(casterror.PropertyName, casterror.ErrorMessage);    
            }

            context.Result = new JsonResult(new {
                Codigo = "0002",
                Mensaje = invalidElementContext.Message,
                Validaciones = errors
            });
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        }

        if(context.Exception is InconsistentDataException){
            context.Result = new JsonResult(new {
                Codigo = "0003",
                Mensaje = context.Exception.Message,
            });
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        }
    }
}