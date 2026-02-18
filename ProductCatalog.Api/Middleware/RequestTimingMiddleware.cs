using System.Diagnostics;

namespace ProductCatalog.Api.Middleware
{
    /// <summary>
    /// Custom middleware that measures how long each HTTP request takes
    /// </summary>
    public class RequestTimingMiddleware
    {
        private readonly RequestDelegate _next;

        // Middleware is constructed once by DI
        public RequestTimingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // This method is called for every HTTP request
        public async Task InvokeAsync(HttpContext context)
        {
            // Start timing
            var stopwatch = Stopwatch.StartNew();

            // Call the next middleware in the pipeline
            await _next(context);

            // Stop timing after request finishes
            stopwatch.Stop();

            var method = context.Request.Method;
            var path = context.Request.Path;
            var statusCode = context.Response.StatusCode;
            var elapsedMs = stopwatch.ElapsedMilliseconds;

            // Simple logging (no framework helpers)
            Console.WriteLine(
                $"[{method}] {path} → {statusCode} ({elapsedMs} ms)"
            );
        }
    }
}
