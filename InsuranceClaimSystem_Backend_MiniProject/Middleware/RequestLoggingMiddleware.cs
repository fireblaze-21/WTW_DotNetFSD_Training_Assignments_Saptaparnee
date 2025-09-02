using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using System.Security.Claims;

namespace InsuranceClaimSystem.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var userName = context.User.Identity.IsAuthenticated ? context.User.Identity.Name : "Anonymous";
            var method = context.Request.Method;
            var path = context.Request.Path;
            var timestamp = DateTime.UtcNow;

            _logger.LogInformation($"[{timestamp}] Request from '{userName}': {method} {path}");

            // Allow unauthenticated access to register/login
            if (path.StartsWithSegments("/api") &&
                !context.User.Identity.IsAuthenticated &&
                !path.StartsWithSegments("/api/auth/login") &&
                !path.StartsWithSegments("/api/auth/register"))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized - Authentication is required.");
                return;
            }

            await _next(context);
        }

    }
}
