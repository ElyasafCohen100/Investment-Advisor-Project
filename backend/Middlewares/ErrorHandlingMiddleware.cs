// ╔════════════════════════════════════════════════════════════════════════════╗
// ║                     🚨 ErrorHandlingMiddleware                             
// ║  Intercepts all HTTP requests and handles any runtime errors              
// ║  Returns friendly JSON responses for exceptions and 404 not found cases   
// ╚════════════════════════════════════════════════════════════════════════════╝

using System.Net;
using System.Text.Json;

namespace StockAdvisorBackend.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        // Middleware pipeline delegate
        private readonly RequestDelegate _next;

        // Constructor
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // Main logic: Wrap the request and handle errors
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);

                // Handle 404 Not Found response
                if (context.Response.StatusCode == (int)HttpStatusCode.NotFound)
                {
                    context.Response.ContentType = "application/json";

                    var response = new
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = "The requested resource was not found."
                    };

                    var json = JsonSerializer.Serialize(response);
                    await context.Response.WriteAsync(json);
                }
            }
            catch (Exception ex)
            {
                // Handle any unhandled exception
                await HandleExceptionAsync(context, ex);
            }
        }

        // Format and send JSON error response
        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "An unexpected error occurred.",
                Details = ex.Message
            };

            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }
    }
}
