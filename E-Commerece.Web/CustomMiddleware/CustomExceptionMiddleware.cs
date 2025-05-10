using System.Text.Json;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
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

                if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    httpContext.Response.ContentType = "application/json";
                    var errorResponse = new ErrorToreturn
                    {
                        ErrorMessage = $"Endpoint {httpContext.Request.Path}"
                    };
                    var responseToReturn = JsonSerializer.Serialize(errorResponse);
                    await httpContext.Response.WriteAsync(responseToReturn);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");

                var errorResponse = new ErrorToreturn
                {
                    ErrorMessage = ex.Message
                };

                httpContext.Response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    UnAuthorizedException => StatusCodes.Status401Unauthorized,
                    BadRequestException badRequestException => GetBadRequestErrors(badRequestException, errorResponse),
                    _ => StatusCodes.Status500InternalServerError
                };

                errorResponse.StatusCode = httpContext.Response.StatusCode;
                httpContext.Response.ContentType = "application/json";

                var responseToReturn = JsonSerializer.Serialize(errorResponse);
                await httpContext.Response.WriteAsync(responseToReturn);
            }
        }

        private static int GetBadRequestErrors(BadRequestException badRequestException, ErrorToreturn response)
        {
            response.Errors = badRequestException.Errors;
            return StatusCodes.Status400BadRequest;
        }
    }
}
