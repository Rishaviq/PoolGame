using PoolGame.Services.Exceptions;
using Serilog;
using System.Net;
using System.Text.Json;

namespace PoolGame.WebAPI.Middlewares
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public GlobalExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            try
            {
                await _next(context);
            }
            catch (UserValidationException ex) {
                Log.Error(ex,"An error with the user:");
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";

                var errorResponse = new
                {
                    error = "BadRequest",
                    message = ex.Message
                };

                var json = JsonSerializer.Serialize(errorResponse);
                await context.Response.WriteAsync(json);

            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error cought in the middleware:");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }
    }
}
