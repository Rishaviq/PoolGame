using static System.Net.Mime.MediaTypeNames;

namespace PoolGame.WebAPI.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";

            // Push properties into the LogContext for this request
            using (Serilog.Context.LogContext.PushProperty("ClientIP", ip))
           
            {
                await _next(context);
            }
        }
    }
}
