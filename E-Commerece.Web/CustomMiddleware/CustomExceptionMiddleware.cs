using System.Text.Json;
using Domain.Exceptions;
using Shared.ErrorModels;

namespace E_Commerece.Web.CustomMiddleware
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionMiddleware> _logger;

        public CustomExceptionMiddleware(RequestDelegate next, ILogger<CustomExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
                if(httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    httpContext.Response.ContentType = "application/json";
                    var errorResponse = new ErrorToreturn
                    {
                        StatusCode = httpContext.Response.StatusCode,
                        ErrorMessage = $"End Poin{httpContext.Request.Path}",
                    };
                    var responseToReturn = JsonSerializer.Serialize(errorResponse);
                    await httpContext.Response.WriteAsync(responseToReturn);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");

                httpContext.Response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                 
                    _ => StatusCodes.Status500InternalServerError
                };
                httpContext.Response.ContentType = "application/json";

                var errorResponse = new ErrorToreturn
                {
                    StatusCode = httpContext.Response.StatusCode,
                    ErrorMessage = "Internal Server Error"
                };

                var responseToReturn = JsonSerializer.Serialize(errorResponse);
                await httpContext.Response.WriteAsync(responseToReturn);
            }
        }
    }
}
