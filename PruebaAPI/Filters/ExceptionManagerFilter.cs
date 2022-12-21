
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PruebaAPI.Exceptions;

namespace PruebaAPI.Filters;

public class ExceptionManagerFilter: ExceptionFilterAttribute
{

    public override void OnException(ExceptionContext context)
    {
        if(context.Exception is ElementNotFoundException){
            context.Result = new JsonResult(new {Codigo = "0001", Mensaje = context.Exception.Message});
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
        }
    }
}