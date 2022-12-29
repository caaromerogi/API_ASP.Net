using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaAPI.Exceptions;
using PruebaAPI.Middleware;

namespace PruebaAPITests.Systems.Middleware;

public class ErrorHandlerMiddlewareTest
{
    public ErrorHandlerMiddlewareTest()
    {

    }
    [Fact]
    public async Task ElementNotFoundException_ThrowHttpNotFound()
    {
        HttpContext ctx = new DefaultHttpContext();
        RequestDelegate next = (HttpContext hc) => throw new ElementNotFoundException("");
        ErrorHandlerMiddleware mw = new ErrorHandlerMiddleware(next);

        await mw.Invoke(ctx);

        Assert.IsType<int>(ctx.Response.StatusCode);
        Assert.Equal(404, ctx.Response.StatusCode);
    }
    
    [Fact]
    public async Task InvalidElementException_ThrowHttpBadRequest()
    {
        HttpContext ctx = new DefaultHttpContext();
        RequestDelegate next = (HttpContext hc) => 
        throw new InvalidElementException<List<ValidationFailure>>("",new List<ValidationFailure>());
        ErrorHandlerMiddleware mw = new ErrorHandlerMiddleware(next);

        await mw.Invoke(ctx);

        Assert.IsType<int>(ctx.Response.StatusCode);
        Assert.Equal(400, ctx.Response.StatusCode);
    }
}