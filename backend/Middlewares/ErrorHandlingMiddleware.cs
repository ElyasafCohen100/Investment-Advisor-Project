using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace StockAdvisorBackend.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);

                // אם הבקשה נגמרה בלי בעיה אבל הסטטוס הוא 404
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
                await HandleExceptionAsync(context, ex);
            }
        }

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
