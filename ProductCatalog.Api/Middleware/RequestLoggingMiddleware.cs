namespace ProductCatalog.Api.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(
            RequestDelegate next,
            ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var start = DateTime.UtcNow;

            _logger.LogInformation(
                "Incoming request {Method} {Path}",
                context.Request.Method,
                context.Request.Path
            );

            await _next(context);

            var duration = DateTime.UtcNow - start;

            _logger.LogInformation(
                "Request {Method} {Path} responded {StatusCode} in {ElapsedMs}ms",
                context.Request.Method,
                context.Request.Path,
                context.Response.StatusCode,
                duration.TotalMilliseconds
            );
        }
    }
}
