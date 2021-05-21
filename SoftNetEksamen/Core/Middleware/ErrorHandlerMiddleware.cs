using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SoftNetEksamen.Core.Middleware
{
  public class ErrorHandlerMiddleware
  {
    private readonly RequestDelegate _next;
    public ErrorHandlerMiddleware(RequestDelegate next)
    {
      _next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
      try
      {
        await _next(context);
      }
      catch (Exception error)
      {
        var response = context.Response;
        response.ContentType = "application/json";
        response.StatusCode = StatusCodes.Status500InternalServerError;
        var result = JsonSerializer.Serialize($"An error occured: {error.Message}");
        await response.WriteAsync(result);
      }
    }
  }
}