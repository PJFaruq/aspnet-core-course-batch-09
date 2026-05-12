using ECommerce.API.Common.Responses;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

namespace ECommerce.API.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var response = new ErrorResponses
            {
                Success = false,
                Message = "Something went wrong",
                StatusCode = StatusCodes.Status500InternalServerError,
                Errors = new List<string>
                {
                    exception.Message
                }
            };

            httpContext.Response.StatusCode = response.StatusCode;
            httpContext.Response.ContentType = "application/json";
            var jsonResponse=JsonSerializer.Serialize(response);

            await httpContext.Response.WriteAsync(jsonResponse, cancellationToken);

            return true;
        }
    }
}
