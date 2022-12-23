using System.Net;
using System.Text.Json;
using FluentValidation.Results;
using PruebaAPI.Error;
using PruebaAPI.Exceptions;

namespace PruebaAPI.Middleware;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch(Exception ex)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            switch(ex)
            {
                case ElementNotFoundException:
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    var errorModelNotFound = new ErrorModelBuilder().WithErrorCode("01").WithMessage(ex.Message).Build();
                    await context.Response.WriteAsync(JsonSerializer.Serialize(errorModelNotFound));
                    break;
                //TODO: Making error more flexible to include the validation answers
                case InvalidElementException<List<ValidationFailure>> invalidElementContext:

                    Dictionary<string,string> errors = new Dictionary<string, string>();

                    foreach (var error in invalidElementContext.Data)
                    {
                        var casterror = error  as ValidationFailure;
                        errors.Add(casterror.PropertyName, casterror.ErrorMessage);    
                    }

                    var errorModelBadRequest = new ErrorModelBuilder().WithErrorCode("02").WithMessage(ex.Message + JsonSerializer.Serialize(errors)).Build();
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    await context.Response.WriteAsync(JsonSerializer.Serialize(errorModelBadRequest));

                    break;
                case InconsistentDataException:
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    var errorModelInconsistentData = new ErrorModelBuilder().WithErrorCode("03").WithMessage(ex.Message).Build();
                    await context.Response.WriteAsync(JsonSerializer.Serialize(errorModelInconsistentData));
                    break;
            }
        }
    }
}